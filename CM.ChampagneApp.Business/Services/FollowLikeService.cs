using System;
using System.Threading.Tasks;
using CM.ChampagneApp.Acq;
using CM.ChampagneApp.Business.Configuration;
using CM.ChampagneApp.DTO.Documents;
using CM.ChampagneApp.DTO.Models.POSTModels.FollowLikeModels;
using Newtonsoft.Json;
using CM.ChampagneApp.DTO.Models.Helpers.EnumOptions;
using CM.ChampagneApp.Instrumentation.Http;

namespace CM.ChampagneApp.Business.Services
{
    public interface IFollowLikeService 
    {
        Task<BaseResponse> FollowBrand(Guid brandId);
        Task<BaseResponse> UnfollowBrand(Guid brandId);
		Task<BaseResponse> FollowUser(Guid userId);
		Task<BaseResponse> UnfollowUser(Guid userId);
		Task<BaseResponse> LikeEntity(Guid contextId, LikeContextTypes.contextTypes contextType);
		Task<BaseResponse> UnlikeEntity(Guid contextId);
    }
    
    public class FollowLikeService : BaseDataService, IFollowLikeService
    {
        public FollowLikeService(IBusinessAccountService authService, IAuthenticationService authenticationService, IAppConfiguration appConfig, IHttpProxy httpProxy) : base(authService, authenticationService, appConfig, httpProxy)
        {
        }

        public async Task<BaseResponse> FollowBrand(Guid brandId)
        {
            var followBrandRequestModel = new FollowBrandRequestModel
            {
                BrandId = brandId
            };

            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/users/currentUser/followingBrands");
            
            var json = JsonConvert.SerializeObject(followBrandRequestModel, Formatting.Indented);

            var response = await PostAsync(baseurl, json, true);

            if (!response.IsSuccessStatusCode)
            {
                return new BaseResponse(false, response.ReasonPhrase);
            }

            return new BaseResponse(true);
        }

		public async Task<BaseResponse> FollowUser(Guid userId)
		{
			var followUserRequestModel = new FollowUserRequestModel
			{
				FollowToUserId = userId
			};

            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/users/currentUser/following");

			var json = JsonConvert.SerializeObject(followUserRequestModel, Formatting.Indented);

			var response = await PostAsync(baseurl, json, true);

			if (!response.IsSuccessStatusCode)
			{
				return new BaseResponse(false, response.ReasonPhrase);
			}
			
			return new BaseResponse(true);

		}

		public async Task<BaseResponse> LikeEntity(Guid contextId, LikeContextTypes.contextTypes contextType)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/users/currentUser/likedEntities/" + contextId.ToString() + "?contextType=" + contextType.ToString());

			var response = await PostAsync(baseurl, null, true);

			if(!response.IsSuccessStatusCode)
			{
				return new BaseResponse(false, response.ReasonPhrase);
			}

			return new BaseResponse(true);
		}

		public async Task<BaseResponse> UnfollowBrand(Guid brandId)
        {
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/users/currentUser/followingBrands/" + brandId.ToString());

			var response = await DeleteAsync(baseurl);

			if(!response.IsSuccessStatusCode)
			{
				return new BaseResponse(false, response.ReasonPhrase);
			}

			return new BaseResponse(true);

        }

		public async Task<BaseResponse> UnfollowUser(Guid userId)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/users/currentUser/following/" + userId.ToString());

			var response = await DeleteAsync(baseurl);

			if (!response.IsSuccessStatusCode)
			{
				return new BaseResponse(false, response.ReasonPhrase);
			}
			
			return new BaseResponse(true);
		}

		public async Task<BaseResponse> UnlikeEntity(Guid contextId)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/users/currentUser/likedEntities/" + contextId.ToString());

			var response = await DeleteAsync(baseurl);

			if(!response.IsSuccessStatusCode)
			{
				return new BaseResponse(false, response.ReasonPhrase);
			}

			return new BaseResponse(true);
		}
	}
}