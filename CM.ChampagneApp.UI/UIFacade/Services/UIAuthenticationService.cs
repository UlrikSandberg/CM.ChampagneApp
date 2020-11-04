using System;
using System.Threading.Tasks;
using CM.ChampagneApp.Business.Services;
using Xamarin.Auth;
using CM.ChampagneApp.DTO.Documents;

namespace CM.ChampagneApp.UI.UIFacade.Services
{

    public interface IUIAuthenticationService
	{
		Task<BaseResponse> Login(string email, string password);
		Task<BaseResponse> RefreshAccess();
		Task RegisterInstallationId();
		Task<BaseResponse> DeregisterInstallationId();
	}

	public class UIAuthenticationService : IUIAuthenticationService
    {
		private readonly IAuthenticationService authenticationService;
		private readonly INotificationRegistrationService notificationRegistrationService;

		public UIAuthenticationService(IAuthenticationService authenticationService, INotificationRegistrationService notificationRegistrationService)
        {
            this.notificationRegistrationService = notificationRegistrationService;
			this.authenticationService = authenticationService;
		}

		public async Task<BaseResponse> Login(string email, string password)
		{

			var result = await authenticationService.Login(email, password);

			if(!result.IsSuccesfull)
			{
				return new BaseResponse(false, result.Message);
			}

			//The login was succesfull and user has been granted a token save credentials
			App.AccountService.SaveCredentials(email, password, result.Model);

			//Upload installationId
			await notificationRegistrationService.RegisterInstallationId();

			return new BaseResponse(true, "Login succesfull");

		}

		public async Task RegisterInstallationId()
		{
			await notificationRegistrationService.RegisterInstallationId();
        }

        //Must be called before signout completes
		public async Task<BaseResponse> DeregisterInstallationId()
		{
			return await notificationRegistrationService.DeregisterInstallationId();
		}

		public async Task<BaseResponse> RefreshAccess()
		{

			var currentUser = App.AccountService.CurrentUser();
			var refreshToken = currentUser.Properties["RefreshToken"];

			if(refreshToken != null)
			{
				var response = await authenticationService.RefreshAccess(refreshToken);
				if(!response.IsSuccesfull)
				{
					return new BaseResponse(false, response.Message);
				}

				App.AccountService.UpdateTokens(response.Model);

				return new BaseResponse(true, "Succesfull re-authentication");

			}
			else
			{
				return new BaseResponse(false, "No refresh token prompt login");
			}         
		}
	}
}
