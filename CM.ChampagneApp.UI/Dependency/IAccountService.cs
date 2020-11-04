using System;
using Xamarin.Auth;
using CM.ChampagneApp.DTO.Models.POSTModels;
using IdentityModel.Client;

namespace CM.ChampagneApp.UI.Dependency
{
    public interface IAccountService
    {
		void SaveCredentials(string email, string password , TokenResponse token);
		void SaveCredentialsFromInitToken(string email, string password, CreateUserResponseModel token);
		void RegisterDeviceInstallationId(string installationId);
		string GetDeviceInstallationId();
		void UpdateTokens(TokenResponse token);
		void SetUserProfileCreated(bool IsCreated);
		void SignOut();
		Account CurrentUser();
		void DeleteCurrentUser();
		string Username();
		void UpdateEmail(string email);
    }
}
