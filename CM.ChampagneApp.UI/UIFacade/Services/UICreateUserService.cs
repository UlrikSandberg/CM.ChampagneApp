using System;
using System.Linq;
using System.Threading.Tasks;
using CM.ChampagneApp.Business.Services;
using CM.ChampagneApp.DTO.Documents;
using EmailValidation;

namespace CM.ChampagneApp.UI.UIFacade.Services
{
    public interface IUICreateUserService
	{
		Task<BaseResponse> CreateUser(string email, string username, string password);
		bool ValidateEmail(string email);
		bool ValidatePassword(string password);
		bool ValidateName(string name);
		bool ValidatePasswordLength(string password);
		Task<BaseResponse> ChangePassword(string currentPassword, string newPassword, string confirmPassword);
		Task<BaseResponse> CheckUsernameAvailability(string username);
		Task<BaseResponse> CheckEmailAvailability(string email);
		Task<BaseResponse> RequestPasswordReset(string email);
		Task<BaseResponse> ChangeEmail(string email, string password);
		Task<BaseResponse> RequestConfirmationEmailResend();
	}

	public class UICreateUserService : IUICreateUserService
    {
		private readonly ICreateUserService createUserService;
		private readonly IUIAuthenticationService uIAuthenticationService;

		private const string forbiddenPasswordChars = " \"";
		private const string forbiddnesNameChars = " !#$%&'()*+,./:;<=>?@[\\]^`{|}~";
		private readonly INotificationRegistrationService notificationRegistrationService;

		public UICreateUserService(ICreateUserService createUserService, IUIAuthenticationService uIAuthenticationService, INotificationRegistrationService notificationRegistrationService)
		{
			this.notificationRegistrationService = notificationRegistrationService;
            this.uIAuthenticationService = uIAuthenticationService;
			this.createUserService = createUserService;
		}

		public async Task<BaseResponse> CreateUser(string email, string username, string password)
		{         
			var cmResult = await createUserService.CreateCMUserProfile(email, username, password);

			if(!cmResult.IsSuccesfull)
			{
				return new BaseResponse(false, cmResult.Message);
			}

			//Save refresh tokens in app
			App.AccountService.SaveCredentialsFromInitToken(email, password, cmResult.Model);
            App.AccountService.SetUserProfileCreated(true);

			//The user has been created and id is available since SaveCredentialsFromInitToken has been called upload installationId
			await notificationRegistrationService.RegisterInstallationId();         

			return new BaseResponse(true);
		}

        public bool ValidatePasswordLength(string password)
		{
			if(password.Length < 6)
			{
				return false;
			}

			return true;
		}

		public bool ValidateEmail(string email)
		{
			return EmailValidator.Validate(email);
		}

		public bool ValidatePassword(string password)
        {
            foreach (char c in password)
            {
                if (forbiddenPasswordChars.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }

		public bool ValidateName(string name)
		{
            foreach(char c in name)
			{
				if(forbiddnesNameChars.Contains(c))
				{
					return false;
				}
			}
			return true;
		}

		public async Task<BaseResponse> ChangePassword(string currentPassword, string newPassword, string confirmPassword)
		{
            //First check if both new password and confirm new password are equal
			if(!newPassword.Equals(confirmPassword))
			{
				return new BaseResponse(false, "New password does not match the confirm password");
			}

			if(!ValidatePassword(newPassword))
			{
				return new BaseResponse(false, "new password must not contain spaces nor any of the following characters \n" + forbiddenPasswordChars);
			}

			return await createUserService.ChangePassword(currentPassword, newPassword);

		}

		public async Task<BaseResponse> CheckUsernameAvailability(string username)
		{
			var response = await createUserService.CheckUsernameAvailability(username);

			if (response == null)
            {
				return new BaseResponse(false, "No connection...");
            }

			return response;
		}

		public async Task<BaseResponse> CheckEmailAvailability(string email)
		{
			var response = await createUserService.CheckEmailAvailability(email);

			if(response == null)
			{
				return new BaseResponse(false, "No connection...");
			}

			return response;
		}

		public async Task<BaseResponse> RequestPasswordReset(string email)
		{
			return await createUserService.RequestPasswordReset(email);
		}

		public async Task<BaseResponse> ChangeEmail(string email, string password)
		{
			return await createUserService.ChangeEmail(email.ToLower(), password);
		}

		public async Task<BaseResponse> RequestConfirmationEmailResend()
		{
			return await createUserService.RequestConfirmationEmailResend();
		}
	}
}
