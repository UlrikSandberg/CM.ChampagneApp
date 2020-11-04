using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using CM.ChampagneApp.Acq;
using CM.ChampagneApp.Business.Configuration;
using CM.ChampagneApp.DTO.Models.POSTModels;
using CM.ChampagneApp.DTO.Documents;
using Newtonsoft.Json;
using System.Threading;
using CM.ChampagneApp.Instrumentation.Http;

namespace CM.ChampagneApp.Business.Services
{
    public interface ICreateUserService
	{
		Task<ResponseWithModel<CreateUserResponseModel>> CreateCMUserProfile(string email, string name, string password);
		Task<BaseResponse> ChangePassword(string currentPassword, string password);
		Task<BaseResponse> CheckUsernameAvailability(string username);
		Task<BaseResponse> CheckEmailAvailability(string email);
		Task<BaseResponse> RequestPasswordReset(string email);
		Task<BaseResponse> ChangeEmail(string email, string password);
		Task<BaseResponse> RequestConfirmationEmailResend();
	}

	public class CreateUserService : BaseDataService, ICreateUserService
	{
		private readonly IAuthenticationService authenticationService;

        public CreateUserService(IBusinessAccountService authService, IAuthenticationService authenticationService, IAppConfiguration appConfig, IHttpProxy httpProxy) : base(authService, authenticationService, appConfig, httpProxy)
		{
			this.authenticationService = authenticationService;
		}

		public async Task<BaseResponse> ChangeEmail(string email, string password)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/users/currentUser/updateEmail?email=" + email.ToLower() + "&password=" + password);

			var response = await PutAsync(baseurl, null, true);

			if (!response.IsSuccessStatusCode)
            {
                if (response.Content != null)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new BaseResponse(false, errorContent);
                }
                return new BaseResponse(false, response.ReasonPhrase);
            }

            return new BaseResponse(true);
		}

		public async Task<BaseResponse> ChangePassword(string currentPassword, string password)
		{
         
            var baseurl = new Uri(AppConfig.IdentityUrl, "identity-api/v1/users/currentUser/updatePassword?currentPassword=" + currentPassword + "&newPassword=" + password);

			var response = await PutAsync(baseurl, null, true);

            if(response == null)
            {
                return new BaseResponse(false);
            }

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
        
		public async Task<BaseResponse> CheckEmailAvailability(string email)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/users/emailavailabillity?email="+email);
            
			var response = await GetAsync(baseurl);

			if(response == null)
			{
				return null;
			}

			if(!response.IsSuccessStatusCode)
			{
				if (response.Content != null)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
					return new BaseResponse(false, message: errorContent);
                }
				return new BaseResponse(false, message: response.ReasonPhrase);
			}

			return new BaseResponse(true);
		}

		public async Task<BaseResponse> CheckUsernameAvailability(string username)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/users/usernameavailabillity?username="+username);
                     
			var response = await GetAsync(baseurl);

			if (response == null)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
            {
                if (response.Content != null)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new BaseResponse(false, message: errorContent);
                }
				return new BaseResponse(false, message: response.ReasonPhrase);

            }

            return new BaseResponse(true);
		}

		public async Task<ResponseWithModel<CreateUserResponseModel>> CreateCMUserProfile(string email, string name, string password)
		{
			var cmUserModel = new CreateCMUserModel
			{
				Email = email,
				Name = name,
				Password = password,
				ProfileName = null,
				Biography = null,
				UTCOffSet = NotificationRegistrationService.GetUTCOffset()
			};

            var createCMUserUrl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/users");

			var json = JsonConvert.SerializeObject(cmUserModel, Formatting.Indented);

			var response = await PostAsync(createCMUserUrl, json);

			if(response == null)
			{
				return new ResponseWithModel<CreateUserResponseModel>(false, null, "Internal server error, contact support");
			}

			if(!response.IsSuccessStatusCode)
			{
				if(response.Content != null)
				{
					var errorMessage = await response.Content.ReadAsStringAsync();
					return new ResponseWithModel<CreateUserResponseModel>(false, null, errorMessage);
				}
				return new ResponseWithModel<CreateUserResponseModel>(false, null, response.ReasonPhrase);
			}

			var content = await response.Content.ReadAsStringAsync();
            var tokenResult = JsonConvert.DeserializeObject<CreateUserResponseModel>(content);

			return new ResponseWithModel<CreateUserResponseModel>(true, tokenResult);
		}

		public async Task<BaseResponse> RequestConfirmationEmailResend()
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/confirmemail/resendconfirmationemail");

			var response = await PostAsync(baseurl, null, true);

			if(!response.IsSuccessStatusCode)
			{
				if(response.Content != null)
				{
					var errorMsg = await response.Content.ReadAsStringAsync();
					return new BaseResponse(false, errorMsg);
				}
				return new BaseResponse(false, response.ReasonPhrase);
			}
			return new BaseResponse(true);         
		}

		public async Task<BaseResponse> RequestPasswordReset(string email)
		{
            var baseurl = new Uri(AppConfig.IdentityUrl, "identity-api/resetpassword/requestpasswordreset?email=" + email);

            var response = await PostAsync(baseurl, null, true);

			if(response == null)
			{
				return new BaseResponse(false, "No connection to server... Contact support or wait.");
			}

			if(!response.IsSuccessStatusCode)
			{
				if (response.Content != null)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
					if(errorMessage.Contains("100"))
					{
						//This means the specified email was not in the system. But by definition this information should not be available... To prevent scrapping with rainbow tables. //TODO : 
						return new BaseResponse(true);
					}
					return new BaseResponse(false, errorMessage);
                }
				return new BaseResponse(false, response.ReasonPhrase);
			}         
			return new BaseResponse(true);         
		}
	}
}
