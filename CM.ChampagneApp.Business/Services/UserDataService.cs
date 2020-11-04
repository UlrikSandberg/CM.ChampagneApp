using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CM.ChampagneApp.Acq;
using CM.ChampagneApp.Business.Configuration;
using CM.ChampagneApp.DTO.Models;
using CM.ChampagneApp.DTO.Models.UserModels;
using Newtonsoft.Json;
using CM.ChampagneApp.DTO.Models.GETModels.FollowModels;
using CM.ChampagneApp.DTO.Documents;
using CM.ChampagneApp.DTO.Builders;
using CM.ChampagneApp.DTO.Models.POSTModels.FollowLikeModels;
using CM.ChampagneApp.DTO.Models.GETModels.UserModels;
using CM.ChampagneApp.Instrumentation.Http;
using Microsoft.AppCenter.Crashes;

namespace CM.ChampagneApp.Business.Services
{
    public interface IUserDataService
    {
        Task<PublicUserModel> GetUser(Guid id);
        Task<UserModel> GetCurrentUser();
		Task<IEnumerable<UserCellarChampagneModel>> GetCurrentUserCellarPagedAsync(int page, int pageSize);
		Task<IEnumerable<ChampagneLight>> GetCurrentUserCellarSavedPagedAsync(int page, int pageSize);
		Task<IEnumerable<UserCellarChampagneModel>> GetUserCellarPagedAsync(Guid userId, int page, int pageSize);
		Task<IEnumerable<ChampagneLight>> GetUserCellarSavedPagedAsync(int page, int pageSize, Guid userId);
        Task<IEnumerable<FollowersModel>> GetUserFollowers(Guid userId, int page, int pageSize);
        Task<IEnumerable<FollowingModel>> GetUserFollowing(Guid userId, int page, int pageSize);
		Task<IEnumerable<FollowingModel>> GetUserBrandFollowing(Guid userId, int page, int pageSize);
		Task<BaseResponse> BookmarkChampagne(Guid champagneId);
		Task<BaseResponse> UnbookmarkChampagne(Guid champagneId);
		Task<UserInfo> GetUserInfo(Guid userId);
		Task<UserSettings> GetCurrentUserSettings();
		Task<BaseResponse> UpdateUserSettings(UserSettingsUpdate update);
		Task<IEnumerable<FollowingModel>> SearchUserByUsernamePagedAsync(string username, int page, int pageSize);
		Task<IEnumerable<FollowingModel>> SearchUsersPagedAsync(int page, int pageSize);
		Task<IEnumerable<FollowingModel>> SearchUserByProfilenamePagedAsync(string profilename, int page, int pageSize);
		Task<BaseResponse> SubmitBugAndFeedback(FeedbackAndBugSubmission feedbackAndBugSubmission);
        Task<BaseResponse> UpdateUserNotificationSettings(UserNotificationsSettingsUpdate update);
        Task<IEnumerable<UserSearchModel>> SearchUsersByProfilenameAndUsernamePagedAsync(string searchText, int page, int pageSize);
        Task<IEnumerable<UserSearchModel>> SearchUsersPagedAsyncLight(int page, int pageSize);
             
    }
    
    public class UserDataService : BaseDataService, IUserDataService
    {
        private readonly ICreateUserService createUserService;

        private const string UserController = "users";
        private Uri UserControllerBaseUrl;

        public UserDataService(ICreateUserService createUserService, IBusinessAccountService authService, IAuthenticationService authenticationService, IAppConfiguration appConfig, IHttpProxy httpProxy) : base(authService, authenticationService, appConfig, httpProxy)
        {
            UserControllerBaseUrl = new Uri($"{AppConfig.ApiBaseUrl}{APIv1Base}/{UserController}");
            this.createUserService = createUserService;
        }

		public async Task<BaseResponse> BookmarkChampagne(Guid champagneId)
		{

			var bookmarkChampagneRequestModel = new BookmarkChampagneRequestModel
			{
				champagneId = champagneId
			};
			
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/users/currentUser/bookmarkedChampagnes");

			var json = JsonConvert.SerializeObject(bookmarkChampagneRequestModel, Formatting.Indented);

			var response = await PostAsync(baseurl, json, true);
			
			if (!response.IsSuccessStatusCode)
			{
				return new BaseResponse(false, response.ReasonPhrase);
			}
			
			return new BaseResponse(true);

		}

		/// <summary>
		/// Gets the current user. Not implemented yet
		/// </summary>
		/// <returns>The current user.</returns>
		public async Task<UserModel> GetCurrentUser()
        {
            try
            {
                var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/users/currentUser");
                
                var response = await GetAsync(baseurl);

                if(response.StatusCode.Equals(System.Net.HttpStatusCode.NotFound))
                {
                    //No user found -> But since the return was not unauthorized this means the access tokens are valid -> Suggest that something went wrong during creation of current user
                    //Request the api to try and create the current user once again.

                    //Maybe before masterminding this section we should first test if there are actually any users who encounter this.
                    //For now maybe just show a empty screen that emphasize that the user should contact customer service cause something went wrong
                    //
                    /*
                    var createResponse = await createUserService.CreateCMUserProfile(authService.GetUsername(), "Name");
                    if(!createResponse.IsSuccesfull)
                    {
                        return null;
                    }

                    return await GetCurrentUser();
                    */
                    System.Diagnostics.Debug.WriteLine("*********** ERROR **********");
                    System.Diagnostics.Debug.WriteLine("CM.ChampagneApp.Business.Services.UserDataService");
                    System.Diagnostics.Debug.WriteLine("No user found -> But since the error is not unauthorized this means the access tokens are valid which suggest that something went wrong during creation of current user");
                    System.Diagnostics.Debug.WriteLine("Request the api to try and create the current user once again");
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<UserModel>(content);
 
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }
        }

		public async Task<IEnumerable<UserCellarChampagneModel>> GetCurrentUserCellarPagedAsync(int page, int pageSize)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/users/currentUser/cellar?page=" + page + "&pageSize=" + pageSize + "&getTasted=" + true);

			var response = await GetAsync(baseurl);

			return await TryReadAsync<IEnumerable<UserCellarChampagneModel>>(response);
		}

		public async Task<IEnumerable<ChampagneLight>> GetCurrentUserCellarSavedPagedAsync(int page, int pageSize)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/users/currentUser/cellar?page=" + page + "&pageSize=" + pageSize + "&getTasted=" + false);

			var response = await GetAsync(baseurl);

			return await TryReadAsync<IEnumerable<ChampagneLight>>(response);
		}

		public async Task<UserSettings> GetCurrentUserSettings()
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/users/currentUser/settings");
            
			var response = await GetAsync(baseurl);

			return await TryReadAsync<UserSettings>(response);
		}

		public async Task<PublicUserModel> GetUser(Guid id)
        {
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/users/{id}?{CFCacheFalse}");

            var response = await GetAsync(baseurl);

			return await TryReadAsync<PublicUserModel>(response);         
        }

		public async Task<IEnumerable<FollowingModel>> GetUserBrandFollowing(Guid userId, int page, int pageSize)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/users/" + userId.ToString() + "/following?page=" + page + "&pageSize=" + pageSize + "&getUserFollowing=" + false);
         
			var response = await GetAsync(baseurl);

			return await TryReadAsync<IEnumerable<FollowingModel>>(response);         
		}

		public async Task<IEnumerable<UserCellarChampagneModel>> GetUserCellarPagedAsync(Guid userId, int page, int pageSize)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/users/{userId}/cellar?page={page}&pageSize={pageSize}&getTasted=true");

			var response = await GetAsync(baseurl);

			return await TryReadAsync<IEnumerable<UserCellarChampagneModel>>(response);
		}

		public async Task<IEnumerable<ChampagneLight>> GetUserCellarSavedPagedAsync(int page, int pageSize, Guid userId)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/users/{userId}/cellar?page={page}&pageSize={pageSize}&getTasted=false");
         
			var response = await GetAsync(baseurl);

			return await TryReadAsync<IEnumerable<ChampagneLight>>(response);         
		}

		public async Task<IEnumerable<FollowersModel>> GetUserFollowers(Guid userId, int page, int pageSize)
        {
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/users/" + userId.ToString()+ "/followers?page=" + page + "&pageSize=" + pageSize);

			var response = await GetAsync(baseurl);

			return await TryReadAsync<IEnumerable<FollowersModel>>(response);
		}

        public async Task<IEnumerable<FollowingModel>> GetUserFollowing(Guid userId, int page, int pageSize)
        {
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/users/"+userId.ToString() + "/following?page="+page+"&pageSize="+pageSize+"&getUserFollowing=" + true);

            var response = await GetAsync(baseurl);

			return await TryReadAsync<IEnumerable<FollowingModel>>(response);
        }

		public async Task<UserInfo> GetUserInfo(Guid userId)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/users/{userId}/userInfo");

			var response = await GetAsync(baseurl);

			return await TryReadAsync<UserInfo>(response);
		}

		public async Task<IEnumerable<FollowingModel>> SearchUsersPagedAsync(int page, int pageSize)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/users/userSearch/paged?page={page}&={pageSize}&{CFCacheTrue}");

			var response = await GetAsync(baseurl);

			return await TryReadAsync<IEnumerable<FollowingModel>>(response);
		}

		public async Task<IEnumerable<FollowingModel>> SearchUserByUsernamePagedAsync(string username, int page, int pageSize)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/users/userSearch/username?username={username}&page={page}&pageSize={pageSize}&{CFCacheFalse}");

			var response = await GetAsync(baseurl);

			return await TryReadAsync<IEnumerable<FollowingModel>>(response);
		}

		public async Task<BaseResponse> UnbookmarkChampagne(Guid champagneId)
		{
			//http://localhost:49991/api/v1/users/bookmarkedChampagnes?champagneId=00000000-0000-0000-0000-000000000000

            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/users/currentUser/bookmarkedChampagnes?champagneId=" + champagneId.ToString());

			var response = await DeleteAsync(baseurl);

			if (!response.IsSuccessStatusCode)
			{
				return new BaseResponse(false, response.ReasonPhrase);
			}
			
			return new BaseResponse(true);
		}

		public async Task<BaseResponse> UpdateUserSettings(UserSettingsUpdate update)
		{

            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/users/currentUser/updateSettingsFromBuilder");

			var json = JsonConvert.SerializeObject(update, Formatting.Indented);

			var response = await PutAsync(baseurl, json);

			if(!response.IsSuccessStatusCode)
			{
				if(response.Content != null)
				{
					var errorContent = await response.Content.ReadAsStringAsync();
					return new BaseResponse(false, errorContent);
				}
				return new BaseResponse(false, response.ReasonPhrase);
			}
            
			return new BaseResponse(true);         
		}

		public async Task<BaseResponse> UpdateUserNotificationSettings(UserNotificationsSettingsUpdate update)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/users/currentUser/updateUserNotificationSettingsFromBuilder");

			var json = JsonConvert.SerializeObject(update, Formatting.Indented);

			var response = await PutAsync(baseurl, json);

			if(!response.IsSuccessStatusCode)
            {
                if(response.Content != null)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new BaseResponse(false, errorContent);
                }
                return new BaseResponse(false, response.ReasonPhrase);
            }
            
            return new BaseResponse(true); 
		}

		public async Task<IEnumerable<FollowingModel>> SearchUserByProfilenamePagedAsync(string profilename, int page, int pageSize)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/users/userSearch/profilename?profilename={profilename}&page={page}&pageSize={pageSize}&{CFCacheFalse}");

			var response = await GetAsync(baseurl);

			return await TryReadAsync<IEnumerable<FollowingModel>>(response);
		}

		public async Task<BaseResponse> SubmitBugAndFeedback(FeedbackAndBugSubmission feedbackAndBugSubmission)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/api/v1/users/currentUser/submission");

			var json = JsonConvert.SerializeObject(feedbackAndBugSubmission, Formatting.Indented);

			var response = await PostAsync(baseurl, json, true);

			if(!response.IsSuccessStatusCode)
			{
				return new BaseResponse(false, response.ReasonPhrase);
			}

			return new BaseResponse(true);
		}

        public async Task<IEnumerable<UserSearchModel>> SearchUsersByProfilenameAndUsernamePagedAsync(string searchText, int page, int pageSize)
        {
            var baseurl = new Uri(AppConfig.ApiBaseUrl, $"/{APIv1Base}/{UserController}/search?searchText={searchText}&page={page}&pageSize={pageSize}&{CFCacheTrue}");

            var response = await GetAsync(baseurl);

            return await TryReadAsync<IEnumerable<UserSearchModel>>(response);
        }

        public async Task<IEnumerable<UserSearchModel>> SearchUsersPagedAsyncLight(int page, int pageSize)
        {
            var baseurl = $"{UserControllerBaseUrl}/searchPaged?page={page}&pageSize={pageSize}&{CFCacheTrue}";

            var response = await GetAsync(new Uri(baseurl));

            return await TryReadAsync<IEnumerable<UserSearchModel>>(response);
        }
    }
}
