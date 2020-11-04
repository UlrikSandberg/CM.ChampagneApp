using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CM.ChampagneApp.Acq;
using CM.ChampagneApp.Business.Configuration;
using CM.ChampagneApp.DTO.Models;
using Newtonsoft.Json;
using CM.ChampagneApp.DTO.Models.GETModels.TastingModels;
using System.Collections;
using CM.ChampagneApp.DTO.Builders;
using CM.ChampagneApp.Instrumentation.Http;
using Microsoft.AppCenter.Crashes;
using CM.ChampagneApp.DTO.Models.GETModels.ChampagneModels;

namespace CM.ChampagneApp.Business.Services
{

    public interface IChampagneDataService
    {
        Task<IEnumerable<ChampagneRoot>> GetChampagneRoots(int page, int pageSize);
		Task<Champagne> GetChampagenProfile(Guid id);
		Task<IEnumerable<ChampagneLight>> GetBottlesFromRoot(Guid champagneRootId);
		Task<IEnumerable<ChampagneRoot>> GetBrandChampagneRoots(Guid brandId);
		Task<IEnumerable<ChampagneRoot>> GetBrandRootsShuffled(Guid brandId, bool isShuffled, int amount);
        Task<IEnumerable<ChampagneRoot>> GetChampagneRootsShuffled(bool isShuffled, int amount);
		Task<IEnumerable<ChampagneLight>> GetChampagnesByFilterAsync(FilterSearchQuery filterSearchQuery, int page, int pageSize);
        Task<IEnumerable<ChampagneSearchModel>> SearchChampagnes(string searchText, int page, int pageSize);
    }


	public class ChampagneDataService : BaseDataService , IChampagneDataService
    {
        private const string ChampagneController = "champagnes";

        public ChampagneDataService(IBusinessAccountService authService, IAuthenticationService authenticationService, IAppConfiguration appConfig, IHttpProxy httpProxy) : base(authService, authenticationService, appConfig, httpProxy)
		{
		}

		public async Task<IEnumerable<ChampagneLight>> GetBottlesFromRoot(Guid champagneFolderId)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/champagnes/champagneFolders/{champagneFolderId}/champagnes?{CFCacheTrue}");

			var response = await GetAsync(baseurl);

			return await TryReadAsync<IEnumerable<ChampagneLight>>(response);
		}

		public async Task<IEnumerable<ChampagneRoot>> GetBrandChampagneRoots(Guid brandId)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/brands/{brandId}/champagneFolders?includeEmptyRoots=false&{CFCacheTrue}");

			var response = await GetAsync(baseurl);

			return await TryReadAsync<IEnumerable<ChampagneRoot>>(response);
		}

		public async Task<IEnumerable<ChampagneRoot>> GetBrandRootsShuffled(Guid brandId, bool isShuffled, int amount)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/brands/{brandId}/champagneFolders/shuffled?isShuffled={isShuffled}&amount={amount}&{CFCacheFalse}");

			var response = await GetAsync(baseurl);

			return await TryReadAsync<IEnumerable<ChampagneRoot>>(response);         
		}

		public async Task<Champagne> GetChampagenProfile(Guid id)
		{
			try
			{            
                var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/champagnes/{id}?{CFCacheFalse}");

				var response = await GetAsync(baseurl);
                
				var content = await response.Content.ReadAsStringAsync();

				if (response.IsSuccessStatusCode)
				{
					return JsonConvert.DeserializeObject<Champagne>(content);
				}
				return null;
			}
			catch (Exception ex)
			{
                Crashes.TrackError(ex);
				System.Diagnostics.Debug.WriteLine(ex.Message);
				return null;
			}
		}

		public async Task<IEnumerable<ChampagneRoot>> GetChampagneRoots(int page, int pageSize)
        {
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/champagnes/champagneFolders?page={page}&pageSize={pageSize}&folderType=Editions&{CFCacheTrue}");

			var response = await GetAsync(baseurl);

			return await TryReadAsync<IEnumerable<ChampagneRoot>>(response);
        }

        public async Task<IEnumerable<ChampagneRoot>> GetChampagneRootsShuffled(bool isShuffled, int amount)
        {
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/champagnes/champagneFolders/shuffled?isShuffled={isShuffled}&amount={amount}&{CFCacheFalse}"); //<-- No caching since the purpose of shuffled is for the result to be different

            var response = await GetAsync(baseurl);

			return await TryReadAsync<IEnumerable<ChampagneRoot>>(response);
        }

		public async Task<IEnumerable<ChampagneLight>> GetChampagnesByFilterAsync(FilterSearchQuery filterSearchQuery, int page, int pageSize)
		{
         
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/champagnes/filterPagedAsync?page={page}&pageSize={pageSize}&{CFCacheTrue}");

			var json = JsonConvert.SerializeObject(filterSearchQuery, Formatting.Indented);

			var response = await PutAsync(baseurl, json);

			return await TryReadAsync<IEnumerable<ChampagneLight>>(response);
		}

        public async Task<IEnumerable<ChampagneSearchModel>> SearchChampagnes(string searchText, int page, int pageSize)
        {
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/{APIv1Base}/{ChampagneController}/search?searchText={searchText}&page={page}&pageSize={pageSize}&{CFCacheTrue}");

            var response = await GetAsync(baseurl);

            return await TryReadAsync<IEnumerable<ChampagneSearchModel>>(response);
        }
    }
}
