using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CM.ChampagneApp.Acq;
using CM.ChampagneApp.Business.Configuration;
using CM.ChampagneApp.DTO.Models;
using CM.ChampagneApp.DTO.Models.GETModels.Top10ListModels;
using CM.ChampagneApp.DTO.Models.POSTModels;
using CM.ChampagneApp.Instrumentation.Http;

namespace CM.ChampagneApp.Business.Services
{
    public interface ITop10ListDataService
    {
        Task<IEnumerable<StandardTop10ListModel>> GetStandardTop10Lists();
        Task<IEnumerable<ChampagneLight>> GetStandardTop10List(string configurationKey, bool filterByVintage, bool filterByHighestRating);
    }

    public class Top10ListDataService : BaseDataService, ITop10ListDataService
    {
        public Top10ListDataService(IBusinessAccountService authService, IAuthenticationService authenticationService, IAppConfiguration appConfig, IHttpProxy httpProxy) : base(authService, authenticationService, appConfig, httpProxy)
        {
        }

        public async Task<IEnumerable<ChampagneLight>> GetStandardTop10List(string configurationKey, bool filterByVintage, bool filterByHighestRating)
        {
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/champagnes/Top10StandardList?configurationKey={configurationKey}&filterByVintage={filterByVintage}&filterByHighestRating={filterByHighestRating}&{CFCacheTrue}");

            var response = await GetAsync(baseurl);

            return await TryReadAsync<IEnumerable<ChampagneLight>>(response);
        }

        public async Task<IEnumerable<StandardTop10ListModel>> GetStandardTop10Lists()
        {
            var baseurl = new Uri(AppConfig.ApiBaseUrl,$"/api/v1/champagnes/Top10StandardLists?{CFCacheTrue}");

            var response = await GetAsync(baseurl);

            return await TryReadAsync<IEnumerable<StandardTop10ListModel>>(response);
        }
    }
}
