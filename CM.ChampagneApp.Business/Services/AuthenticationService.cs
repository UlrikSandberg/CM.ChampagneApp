using System;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using CM.ChampagneApp.Business.Configuration;
using CM.ChampagneApp.DTO.Documents;

using IdentityModel.Client;
using CM.ChampagneApp.Acq;

namespace CM.ChampagneApp.Business.Services
{   
    public interface IAuthenticationService
    {
        
		Task<ResponseWithModel<TokenResponse>> Login(string email, string password);
		Task<ResponseWithModel<TokenResponse>> RefreshAccess(string refreshToken);
    }

	public class AuthenticationService : IAuthenticationService
    {
		private readonly IAppConfiguration config;

		public AuthenticationService(IAppConfiguration config) 
		{
			this.config = config;
		}

		public async Task<ResponseWithModel<TokenResponse>> Login(string email, string password)
		{

            var disco = await DiscoveryClient.GetAsync(config.IdentityUrl.ToString());
			         
			if(disco.IsError)
			{
				System.Diagnostics.Debug.WriteLine(disco.Error);
				return new ResponseWithModel<TokenResponse>(false, null, disco.Error, null);
			}

            var tokenClient = new TokenClient(disco.TokenEndpoint, config.ClientId, config.ClientSecret);
			var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(email, password);
          

			if(tokenResponse.IsError)
			{        
				if(!string.IsNullOrEmpty(tokenResponse.ErrorDescription))
				{               
                    if (tokenResponse.ErrorDescription.Equals("1000"))
                    {
                        //ErrorDescription 1000 is suspended account log the user out of the application immediately...
                        
                        return new ResponseWithModel<TokenResponse>(false, null, tokenResponse.ErrorDescription);
                    }
				}    

				System.Diagnostics.Debug.WriteLine(tokenResponse.Json);
				System.Diagnostics.Debug.WriteLine(tokenResponse.ErrorDescription);
				return new ResponseWithModel<TokenResponse>(false, null, tokenResponse.ErrorDescription);
			}

			//The user has been authenticated and granted a token return succesfull AuthResponse         
			return new ResponseWithModel<TokenResponse>(true, tokenResponse, "Succesfull login");

		}

		public async Task<ResponseWithModel<TokenResponse>> RefreshAccess(string refreshToken)
		{
            var disco = await DiscoveryClient.GetAsync(config.IdentityUrl.ToString());

         
			if(disco.IsError)
			{
				System.Diagnostics.Debug.WriteLine(disco.Error);
				return new ResponseWithModel<TokenResponse>(false, null, disco.Error);
			}

            var tokenClient = new TokenClient(disco.TokenEndpoint, config.ClientId, config.ClientSecret);
			var tokenResponse = await tokenClient.RequestRefreshTokenAsync(refreshToken).ConfigureAwait(false);

			if(tokenResponse.IsError)
			{
				System.Diagnostics.Debug.WriteLine(tokenResponse.Json);
				System.Diagnostics.Debug.WriteLine(tokenResponse.ErrorDescription);
				return new ResponseWithModel<TokenResponse>(false, null, tokenResponse.ErrorDescription);
			}

			return new ResponseWithModel<TokenResponse>(true, tokenResponse, "Succesfull refresh");
		}
	}
}
