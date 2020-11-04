using System;
using CM.ChampagneApp.Business.Configuration;
using System.Collections.Generic;
using CM.ChampagneApp.DTO.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using CM.ChampagneApp.Acq;
using CM.ChampagneApp.DTO.Models.GETModels.FollowModels;
using CM.ChampagneApp.DTO.Models.GETModels.BrandModels;
using System.Runtime.InteropServices;
using CM.ChampagneApp.Instrumentation.Http;

namespace CM.ChampagneApp.Business.Services
{
    public interface IBrandDataService
    {
		Task<IEnumerable<BrandLight>> GetBrandsPaged(int page, int pageSize, bool includeUnpublished, bool sortAscending);
		Task<BrandProfile> GetBrandProfile(Guid brandId);
		Task<BrandCellarModel> GetBrandCellar(Guid brandId);
		Task<IEnumerable<FollowersModel>> GetBrandFollowers(Guid brandId, int page, int pageSize);
		Task<BrandInfoPageModel> GetBrandInfoPageAsync(Guid brandId, Guid infoPageId);
        Task<IEnumerable<BrandSearchModel>> SearchBrands(string searchText, int page, int pageSize);  
    }

	public class BrandDataService : BaseDataService, IBrandDataService
    {
        private const string BrandController = "brands";
        public BrandDataService(IBusinessAccountService authService, IAuthenticationService authenticationService, IAppConfiguration appConfig, IHttpProxy httpProxy) : base(authService, authenticationService, appConfig, httpProxy)
		{
		}      

		/// <summary>
		/// Gets the brands paged. Get all the brands in a light version, usefull when fetching a list of lightBrand.
		/// </summary>
		/// <returns>The brands paged.</returns>
		/// <param name="page">Page.</param>
		/// <param name="pageSize">Page size.</param>
		/// <param name="includeUnpublished">If set to <c>true</c> include unpublished.</param>
		public async Task<IEnumerable<BrandLight>> GetBrandsPaged(int page, int pageSize, bool includeUnpublished, bool sortAscending)
        {
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/brands?page={page}&pageSize={pageSize}&includeUnpublished={includeUnpublished}&sortAscending={sortAscending}&{CFCacheTrue}");

			var response = await GetAsync(baseurl);

			return await TryReadAsync<IEnumerable<BrandLight>>(response);
		}

		public async Task<BrandProfile> GetBrandProfile(Guid brandId)
        {         
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/brands/{brandId}/profile?{CFCacheFalse}");

			var response = await GetAsync(baseurl);

			return await TryReadAsync<BrandProfile>(response);       
        }

		public async Task<BrandCellarModel> GetBrandCellar(Guid brandId)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/brands/{brandId}/cellar?{CFCacheTrue}");
			
			var response = await GetAsync(baseurl);

			return await TryReadAsync<BrandCellarModel>(response);         
		}

		public async Task<IEnumerable<FollowersModel>> GetBrandFollowers(Guid brandId, int page, int pageSize)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/brands/{brandId}/followers?page={page}&pageSize={pageSize}&{CFCacheFalse}");
			
			var response = await GetAsync(baseurl);

			return await TryReadAsync<IEnumerable<FollowersModel>>(response);
		}

		public async Task<BrandInfoPageModel> GetBrandInfoPageAsync(Guid brandId, Guid infoPageId)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/brands/{brandId}/pages/{infoPageId}?{CFCacheTrue}");

			var response = await GetAsync(baseurl);

			return await TryReadAsync<BrandInfoPageModel>(response);
		}

        public async Task<IEnumerable<BrandSearchModel>> SearchBrands(string searchText, int page, int pageSize)
        {
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/{APIv1Base}/{BrandController}/search?searchText={searchText}&page={page}&pageSize={pageSize}&{CFCacheTrue}");

            var response = await GetAsync(baseurl);

            return await TryReadAsync<IEnumerable<BrandSearchModel>>(response);
        }
    }
}
