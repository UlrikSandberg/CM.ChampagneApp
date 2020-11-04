using System;
using System.Net.Http;
using System.Threading.Tasks;
using CM.ChampagneApp.Acq;
using CM.ChampagneApp.Business.Configuration;
using System.Linq.Expressions;
using Newtonsoft.Json;
using System.Text;
using CM.ChampagneApp.DTO.Documents;
using Microsoft.IdentityModel.Tokens;
using IdentityModel.Client;
using CM.ChampagneApp.Instrumentation.Http;
using System.Net.Http.Headers;
using Microsoft.AppCenter.Crashes;

namespace CM.ChampagneApp.Business.Services
{
    public abstract class BaseDataService
    {
        //App-wide authService -> Secure store
		protected readonly IBusinessAccountService AuthService;

        protected const string CFCacheTrue = "cf-cache=true";
        protected const string CFCacheFalse = "cf-cache=false";
        protected const string APIv1Base = "api/v1";
        //Identity connection client
        private readonly IAuthenticationService authenticationService;

		private int reauthenticationAttempts = 0;

        //App configurations 
		protected readonly IAppConfiguration AppConfig;

        protected HttpClient HttpClient { get; }

        protected BaseDataService(IBusinessAccountService authService, IAuthenticationService authenticationService, IAppConfiguration appConfig, IHttpProxy httpProxy)
        {
            this.authenticationService = authenticationService;
			this.AppConfig = appConfig;

            HttpClient = httpProxy.HttpClient;
            SetupHttpClient();

            this.AuthService = authService;
		}

	    private void SetBearerToken()
		{
            HttpClient.SetBearerToken(AuthService.GetAccesToken());
		}

        private void SetupHttpClient()
        {
            if(!HttpClient.DefaultRequestHeaders.Contains("X-Client-Id"))
                HttpClient.DefaultRequestHeaders.Add("X-Client-Id", AppConfig.ClientId);

            if (!HttpClient.DefaultRequestHeaders.Contains("X-Client-Secret"))
                HttpClient.DefaultRequestHeaders.Add("X-Client-Secret", AppConfig.ClientSecret);

            if (!HttpClient.DefaultRequestHeaders.Contains("X-Client-BuildId"))
                HttpClient.DefaultRequestHeaders.Add("X-Client-BuildId", AppConfig.BuildId);

            HttpClient.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            HttpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
        }

        /// <summary>
        /// Deletes the async. Not implemented yet.
        /// </summary>
        /// <returns>The async.</returns>
        /// <param name="destinationUri">Desitnation URI.</param>
        /// <param name="IsAuth">If set to <c>true</c> is auth.</param>
        protected async Task<HttpResponseMessage> DeleteAsync(Uri destinationUri, bool IsAuth = true)
		{
            if (IsAuth)
                SetBearerToken();

			var response = new HttpResponseMessage();

			try
			{
                response = await HttpClient.DeleteAsync(destinationUri);
				if(response.StatusCode.Equals(System.Net.HttpStatusCode.Unauthorized))
				{    
					var reauthenticationResponse = await ReauthenticateUser();

					if(!reauthenticationResponse.IsSuccesfull)
					{
						return response;
					}

					if (reauthenticationAttempts > 5)
                    {
                        System.Diagnostics.Debug.WriteLine("Reauthenticating a fith time, something is wrong");
                        return null;
                    }

                    reauthenticationAttempts++;

					return await DeleteAsync(destinationUri, IsAuth);
				}
			}
			catch (Exception ex)
			{
                Crashes.TrackError(ex);
				System.Diagnostics.Debug.WriteLine(ex.Message);
				response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
				response.ReasonPhrase = ex.Message;
			}

			return response;
		}

		protected async Task<HttpResponseMessage> PutAsync(Uri destinationUri, string contentJson = null, bool IsAuth = true)
        {
            if (IsAuth)
                SetBearerToken();
         
			StringContent httpContent = null;
            if (contentJson != null)
            {
                httpContent = new StringContent(contentJson, System.Text.Encoding.UTF8, "application/json");
            }

			var response = new HttpResponseMessage();

            try
			{
                response = await HttpClient.PutAsync(destinationUri, httpContent);
				if(response.StatusCode.Equals(System.Net.HttpStatusCode.Unauthorized))
				{

                    if (reauthenticationAttempts > 5)
                    {
                        System.Diagnostics.Debug.WriteLine("Reauthenticating a fith time, something is wrong");
                        return null;
                    }

                    reauthenticationAttempts++;

					var reauthenticationResponse = await ReauthenticateUser();

					if(!reauthenticationResponse.IsSuccesfull)
					{
						return response;
					}

					return await PutAsync(destinationUri, contentJson, IsAuth);
				}
			}
			catch(Exception ex)
			{
                Crashes.TrackError(ex);
				System.Diagnostics.Debug.WriteLine(ex.Message);
				//TODO : Find a better generic way to do this ;)
				response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.ReasonPhrase = ex.Message;
			}

			return response;
		}

		protected async Task<HttpResponseMessage> PostMultipartFormDataAsync(Uri destinationUri, MultipartFormDataContent dataContent, bool IsAuth = true)
        {
            if (IsAuth)
                SetBearerToken();

            var response = new HttpResponseMessage();

            try
			{
                response = await HttpClient.PostAsync(destinationUri, dataContent);
				if(response.StatusCode.Equals(System.Net.HttpStatusCode.Unauthorized))
				{

                    if (reauthenticationAttempts > 5)
                    {
                        System.Diagnostics.Debug.WriteLine("Reauthenticating a fith time, something is wrong");
                        return null;
                    }

                    reauthenticationAttempts++;

					var reauthenticationResponse = await ReauthenticateUser();

					if(!reauthenticationResponse.IsSuccesfull)
					{
						return response;
					}

					return await PostMultipartFormDataAsync(destinationUri, dataContent, IsAuth);
				}
			}
			catch (Exception ex)
			{
                Crashes.TrackError(ex);
				System.Diagnostics.Debug.WriteLine(ex.Message);
                //Remember 
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.ReasonPhrase = ex.Message;

                return response;
			}
			return response;
		}

		protected async Task<HttpResponseMessage> PostAsync(Uri destinationUri, string contentJson, bool IsAuth = true)
        {
            if (IsAuth)
                SetBearerToken();

			StringContent httpContent = null;
			if (contentJson != null)
			{
				httpContent = new StringContent(contentJson, System.Text.Encoding.UTF8, "application/json");
			}

			var response = new HttpResponseMessage();
			try
			{
				response = await HttpClient.PostAsync(destinationUri, httpContent);
				if(response.StatusCode.Equals(System.Net.HttpStatusCode.Unauthorized))
				{

                    if (reauthenticationAttempts > 5)
                    {
                        System.Diagnostics.Debug.WriteLine("Reauthenticating a fith time, something is wrong");
                        return null;
                    }

                    reauthenticationAttempts++;

					var reauthenticationResponse = await ReauthenticateUser();

					if(!reauthenticationResponse.IsSuccesfull)
					{
						return response;
					}

					return await PostAsync(destinationUri, contentJson, IsAuth);
				}
			}
			catch (Exception ex)
			{
                Crashes.TrackError(ex);
				System.Diagnostics.Debug.WriteLine(ex.Message);
                //Remember 
				response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
				response.ReasonPhrase = ex.Message; 

				return response;
			}

			return response;
		}

		protected async Task<HttpResponseMessage> PostAsync(Uri destinationUri, string contentJson)
		{
            try
			{
				StringContent httpContent = null;
				if (contentJson != null)
                {
                    httpContent = new StringContent(contentJson, System.Text.Encoding.UTF8, "application/json");
                }

				return await HttpClient.PostAsync(destinationUri, httpContent);
			}
			catch (Exception ex)
			{
                Crashes.TrackError(ex);
				System.Diagnostics.Debug.WriteLine(ex.Message);
				return null;
			}
		}

        protected async Task<HttpResponseMessage> GetAsync(Uri baseurl, bool IsAuth = true)
        {
            if (IsAuth)
                SetBearerToken();
         
            try
            {
                var response = await HttpClient.GetAsync(baseurl);

				if (!response.StatusCode.Equals(System.Net.HttpStatusCode.Unauthorized))
                {
					return response;
                }

				System.Diagnostics.Debug.WriteLine("Http request was denied due to httpstatuscode.unathorized");
                //Handle code to reauthenticate a user

                if (reauthenticationAttempts > 5)
                {
                    System.Diagnostics.Debug.WriteLine("Reauthenticating a fith time, something is wrong");
                    return null;
                }

                reauthenticationAttempts++;

                var reauthenticationResponse = await ReauthenticateUser();

                if (!reauthenticationResponse.IsSuccesfull)
                {
					System.Diagnostics.Debug.WriteLine(reauthenticationResponse.Message);
                    return response;
                }

                return await GetAsync(baseurl);
   
			}
			catch (Exception ex)
			{
                Crashes.TrackError(ex);
                //Catch exceptions such as timeout exceptions and stuffsses..
				System.Diagnostics.Debug.WriteLine(ex.Message);
				return null;
			}
		}
        
		protected async static Task<T> TryReadAsync<T>(HttpResponseMessage response) where T : class
		{
			if(response == null || !response.IsSuccessStatusCode)
				return null;

            return await response.Content.ReadAsAsync<T>();
		}

		private string GetClientIdSecretAuthorization()
		{
            var clientId = AppConfig.ClientId;
            var clientSecret = AppConfig.ClientSecret;

			var encoding = Encoding.UTF8;

			var credentials = string.Format("{0}:{1}", clientId, clientSecret);

			return Convert.ToBase64String(encoding.GetBytes(credentials));
		}

		private async Task<ResponseWithModel<TokenResponse>> ReauthenticateUser()
		{
			//API returned http - unauthorized -> Try to reaunthenticate user by refresh token

			System.Diagnostics.Debug.WriteLine("Trying to re-authenticate user by refresh token");
			var refreshResult = await authenticationService.RefreshAccess(AuthService.GetRefreshToken());
            
			if(!refreshResult.IsSuccesfull)
			{
				System.Diagnostics.Debug.WriteLine("Re-authentication by refresh token failed -> Fallback to password reauthentication");
				var passwordResult = await authenticationService.Login(AuthService.GetUsername(), AuthService.GetPassword());

				if(!passwordResult.IsSuccesfull)
				{
					System.Diagnostics.Debug.WriteLine("Re-authentication by password failed");
                    //Check if the reason is caused by supension
					if(passwordResult.Message != null)
					{
						if(passwordResult.Message.Equals("1000"))
						{
							//Account has been suspended ->
							throw new AccessViolationException("User suspended! Handle logic to suspend user from app");
						} 
					}

					return new ResponseWithModel<TokenResponse>(false, null, "RefreshToken error: " + refreshResult.Message + ": PasswordError: " + passwordResult.Message);
				}
				System.Diagnostics.Debug.WriteLine("Re-authentication by password, succeeded");
				//Update tokens
				AuthService.UpdateTokens(passwordResult.Model);

				return new ResponseWithModel<TokenResponse>(true);
               
			}

			System.Diagnostics.Debug.WriteLine("Re-authentication by refresh token, succeded");
			//Update tokens
			AuthService.UpdateTokens(refreshResult.Model);


			return new ResponseWithModel<TokenResponse>(true);
		}
    }
}
