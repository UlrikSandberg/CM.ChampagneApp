using System;
using System.Windows.Input;
using FreshMvvm;
using Xamarin.Forms;
using CM.ChampagneApp.UI.UIFacade.Services;
using CM.ChampagneApp.UI.Elements.Helpers.ElementEnum;
using System.Threading.Tasks;

namespace CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.ChangePasswordPage
{
    public class ChangePasswordPageModel : FreshBasePageModel
    {
        public ICommand NavigateBack { get; set; }
        public ICommand EditingDone { get; set; }
        public ICommand PasswordEntered { get; set; }
        public ICommand NewPasswordEntered { get; set; }
        public ICommand ConfirmPasswordEntered { get; set; }

		public ICommand PasswordChanged { get; set; }
		public ICommand NewPasswordChanged { get; set; }
		public ICommand ConfirmPasswordChanged { get; set; }

		public ICommand UploadingIndicatorDissappeared { get; set; }

		public string CurrentPassword { get; set; }
		public string NewPassword { get; set; }
		public string ConfirmPassword { get; set; }

		public bool IsValidatingPassword { get; set; }
		public SuccesStateEnum ValidationPasswordState { get; set; } = SuccesStateEnum.Default;
		public string PasswordErrorMessage { get; set; }

		public bool IsValidatingNewPassword { get; set; }
		public SuccesStateEnum ValidationNewPasswordState { get; set; } = SuccesStateEnum.Default;
		public string NewPasswordErrorMessage { get; set; }

		public bool IsValidatingConfirmPassword { get; set; }
		public SuccesStateEnum ValidationConfirmPasswordState { get; set; } = SuccesStateEnum.Default;   
		public string ConfirmPasswordErrorMessage { get; set; }

		private readonly IUICreateUserService createUserService;

		public ChangePasswordPageModel(IUICreateUserService createUserService)
        {
            this.createUserService = createUserService;
			NavigateBack = new Command(async () => await OnNavigateBack());
            EditingDone = new Command(async () => await OnEditingDone());
            PasswordEntered = new Command<string>(OnPasswordEntered);
            NewPasswordEntered = new Command<string>(OnNewPasswordEntered);
            ConfirmPasswordEntered = new Command<string>(OnConfirmPasswordEntered);
			UploadingIndicatorDissappeared = new Command(async () => await OnChangingPasswordIndicatorDissappeared());
			PasswordChanged = new Command<string>(OnPasswordChanged);
			NewPasswordChanged = new Command<string>(OnNewPasswordChanged);
			ConfirmPasswordChanged = new Command<string>(OnConfirmPasswordChanged);
        }


        private void OnPasswordEntered(string password)
        {
			CurrentPassword = password;
        }

        private void OnPasswordChanged(string password)
		{
			IsValidatingPassword = true;
			if(string.IsNullOrEmpty(password))
			{
				ValidationPasswordState = SuccesStateEnum.Default;
				IsValidatingPassword = false;
			}

			if(!createUserService.ValidatePassword(password))
			{
				PasswordErrorMessage = "Password must not contain any spaces nor \"";
				ValidationPasswordState = SuccesStateEnum.Error;
				IsValidatingPassword = false;            
			}
			else
			{
				PasswordErrorMessage = null;
				ValidationPasswordState = SuccesStateEnum.Default;
				IsValidatingPassword = false;
			}

		}

        private void OnNewPasswordEntered(string newPassword)
        {         
			NewPassword = newPassword;
        }

        private void OnNewPasswordChanged(string newPassword)
		{
			IsValidatingNewPassword = true;
			if (createUserService.ValidatePassword(newPassword))
            {
                if (createUserService.ValidatePasswordLength(newPassword))
				{
					NewPasswordErrorMessage = null;
					ValidationNewPasswordState = SuccesStateEnum.Default;
					IsValidatingNewPassword = false;
				}
				else
				{
					//Must be attleast this long!
					NewPasswordErrorMessage = "Password must be attleast 6 characters long";
					ValidationNewPasswordState = SuccesStateEnum.Error;
					IsValidatingNewPassword = false;
				}
            }
			else
			{
				//Must not contain!!
				NewPasswordErrorMessage = "Password must not contain any spaces nor \"";
				ValidationNewPasswordState = SuccesStateEnum.Error;
				IsValidatingNewPassword = false;
			}
		}

        private void OnConfirmPasswordEntered(string confirmPassword)
        {
			ConfirmPassword = confirmPassword;
        }

        private void OnConfirmPasswordChanged(string confirmPassword)
		{
			IsValidatingConfirmPassword = true;
			if(string.IsNullOrEmpty(NewPassword))
			{
				ValidationConfirmPasswordState = SuccesStateEnum.Error;
				ConfirmPasswordErrorMessage = "Confirm password must match new password";
				IsValidatingConfirmPassword = false;
			}         
			else
			{
				if(createUserService.ValidatePassword(confirmPassword))
				{
					if(!confirmPassword.Equals(NewPassword))
					{
						ValidationConfirmPasswordState = SuccesStateEnum.Error;
                        ConfirmPasswordErrorMessage = "Confirm password must match new password";
                        IsValidatingConfirmPassword = false;
					}
					else
					{
						if (createUserService.ValidatePasswordLength(confirmPassword))
                        {
                            ConfirmPasswordErrorMessage = null;
							ValidationConfirmPasswordState = SuccesStateEnum.Default;
                            IsValidatingConfirmPassword = false;
                        }
                        else
                        {
                            //Not long enough
                            ConfirmPasswordErrorMessage = "Password must be attleast 6 characters long";
                            ValidationConfirmPasswordState = SuccesStateEnum.Error;
                            IsValidatingConfirmPassword = false;
                        }
					}               
				}
				else
				{
                    //Not special chars allowed
					ConfirmPasswordErrorMessage = "Password must not contain any spaces nor \"";
                    ValidationConfirmPasswordState = SuccesStateEnum.Error;
                    IsValidatingConfirmPassword = false;
				}
			}
		}

        private async Task OnChangingPasswordIndicatorDissappeared()
		{
			if(IsDoneChangingPasswordWithSucces)
			{
				await CoreMethods.PopPageModel();
			}
			IsChangingPassword = false;
			IsChangingPasswordIndicatorVisible = false;
			IsDoneChangingPasswordWithSucces = false;
			IsDoneChangingPasswordWithError = false;
		}

		public bool IsChangingPasswordIndicatorVisible { get; set; } = false;
        public bool IsChangingPassword { get; set; } = false;
        public bool IsDoneChangingPasswordWithSucces { get; set; } = false;
        public bool IsDoneChangingPasswordWithError { get; set; } = false;
        private async Task OnEditingDone()
        {
			if (string.IsNullOrEmpty(CurrentPassword) || string.IsNullOrEmpty(NewPassword) || string.IsNullOrEmpty(ConfirmPassword))
			{
				await CoreMethods.DisplayAlert("Error: ", "All fields are required to change password", "OK");
			}
			else
			{
				IsChangingPassword = true;
				IsChangingPasswordIndicatorVisible = true;
				var response = await createUserService.ChangePassword(CurrentPassword, NewPassword, ConfirmPassword);

				if (!response.IsSuccesfull)
				{
					IsDoneChangingPasswordWithError = true;
					await CoreMethods.DisplayAlert("Error: ", response.Message, "OK");
				}
				else
				{               
					IsDoneChangingPasswordWithSucces = true;
				}
			}

        }      

        private async Task OnNavigateBack()
        {
            await CoreMethods.PopPageModel();
        }
    }
}
