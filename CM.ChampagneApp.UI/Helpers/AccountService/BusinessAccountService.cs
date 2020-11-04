using System;
using CM.ChampagneApp.Acq;
using CM.ChampagneApp.DTO.Models.POSTModels;
using IdentityModel.Client;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AppCenter.Crashes;

namespace CM.ChampagneApp.UI.Helpers.AccountService
{
	public class BussinessAccountService : IBusinessAccountService
    {
		public string GetAccesToken()
		{
			var result = App.AccountService.CurrentUser();

			if(result == null)
			{
				return null;
			}

			var accessToken = result.Properties["AccessToken"];

			if(accessToken != null)
			{
				return accessToken;
			}
			else
			{
				return null;
			}
		}

		public string GetPassword()
		{
			var result = App.AccountService.CurrentUser();

            if (result == null)
            {
                return null;
            }

            var refreshToken = result.Properties["Password"];

            if (refreshToken != null)
            {
                return refreshToken;
            }
            else
            {
                return null;
            }
		}

		public string GetRefreshToken()
		{
			var result = App.AccountService.CurrentUser();

			if(result == null)
			{
				return null;
			}

			var refreshToken = result.Properties["RefreshToken"];

			if(refreshToken != null)
			{
				return refreshToken;
			}
			else
			{
				return null;
			}
		}

		public string GetUsername()
		{
			var result = App.AccountService.CurrentUser();

            if (result == null)
            {
                return null;
            }

			return result.Username;
		}

		public void SuspendUser()
		{
			throw new NotImplementedException();
		}

		public void UpdateTokens(TokenResponse tokens)
		{
			App.AccountService.UpdateTokens(tokens);
		}

		public void SaveCredentialsByCustomToken(string email, string password, TokenResponse token)
		{
            
		}

		public Guid GetId()
		{
			var user = App.AccountService.CurrentUser();

            if(user == null)
			{
				return Guid.Empty;
			}

			var token = user.Properties["AccessToken"];

			if(token == null)
			{
				return Guid.Empty;
			}

			var handler = new JwtSecurityTokenHandler();

			var decodedToken = handler.ReadJwtToken(token) as JwtSecurityToken;

			//try to read subject from decodedToken

			Guid id = Guid.Empty;

			if(decodedToken != null)
			{
                try
				{
					id = Guid.Parse(decodedToken.Subject);
				}
				catch(Exception ex)
				{
                    Crashes.TrackError(ex);
					System.Diagnostics.Debug.WriteLine(ex.Message);
					return Guid.Empty;
				}
			}

			return id;
		}

		public string GetInstallationId()
		{
			return App.AccountService.GetDeviceInstallationId();
		}
	}
}
