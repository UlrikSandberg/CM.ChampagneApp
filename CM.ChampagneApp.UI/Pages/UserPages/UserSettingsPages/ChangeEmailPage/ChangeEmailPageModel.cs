using System;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using System.Windows.Input;
using Xamarin.Forms;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser;
using CM.ChampagneApp.UI.UIFacade.Services;
using CM.ChampagneApp.UI.Elements.Helpers.ElementEnum;
using CM.ChampagneApp.Instrumentation;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.Helpers;

namespace CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.ChangeEmailPage
{
	public class ChangeEmailPageModel : BasePageModel
	{
		public UIUserSettings UserSettings { get; set; }
		public Color EmailVerifiedColor { get; set; }
		public SuccesStateEnum SuccesState { get; set; }
		public string EmailVerifiedText { get; set; }
      
		public ICommand DoneEditing { get; set; }
		public ICommand NewEmailEntered { get; set; }
		public ICommand PasswordEntered { get; set; }
		public ICommand DoneUploading { get; set; }

		public ICommand ResendConfirmation { get; set; }

		public string newEmailEntered { get; set; }
		public string passwordEntered { get; set; }

		private readonly IUIUserDataService userDataService;
		private readonly IUICreateUserService createUserService;

		public ChangeEmailPageModel(IUIUserDataService userDataService, IUICreateUserService createUserService, IEventCollector ec) : base(ec)
        {
			this.createUserService = createUserService;
			this.userDataService = userDataService;
			DoneEditing = new Command(async () => await Handle_DoneEditing());
			NewEmailEntered = new Command<string>(Handle_EmailEntered);
			PasswordEntered = new Command<string>(Handle_PasswordEntered);
			DoneUploading = new Command(async () => await OnChangingPasswordIndicatorDissappeared());
			ResendConfirmation = new Command(async () => await Handle_ResendConfirmationEmail());
        }      

		public override void Init(object initData)
		{
			base.Init(initData);
            DownloadCurrentUserSettings().RunForget();
		}

		protected override async Task OnReconnect()
		{
			IsOutOfService = false;
			await DownloadCurrentUserSettings();
		}
        
        private void Handle_EmailEntered(string email)
		{
			newEmailEntered = email;
		}
        
        private void Handle_PasswordEntered(string password)
		{
			passwordEntered = password;
		}

		public bool IsDownloadingCurrentUserSettings { get; set; }
		private async Task DownloadCurrentUserSettings()
		{
			IsDownloadingCurrentUserSettings = true;         
			UserSettings = await userDataService.GetCurrentUserSetting();

			if(UserSettings == null)
			{
				IsOutOfService = true;
				IsDownloadingCurrentUserSettings = false;
				return;
			}
			else
			{
				IsDownloadingCurrentUserSettings = false;

				if(!UserSettings.IsEmailVerified)
				{
					EmailVerifiedColor = Color.FromHex("#C77966");
					EmailVerifiedText = "Email not confirmed yet...";
					SuccesState = SuccesStateEnum.Error;
				}
				else
				{
					EmailVerifiedColor = Color.FromHex("#A68F68");
                    EmailVerifiedText = "Email confirmed";
					SuccesState = SuccesStateEnum.Succes;
				}

				System.Diagnostics.Debug.WriteLine(UserSettings);
			}         
		}

		private bool IsInformingUser = false;
		private async Task OnChangingPasswordIndicatorDissappeared()
        {
			IsUploadingIndicatorVisible = false;
			IsUploadingEmail = false;
			if(IsDoneChangingEmailWithSucces)
			{
				IsDoneChangingEmailWithSucces = false;
				await CoreMethods.DisplayAlert("Confirm your new email", "Head over to your inbox and confirm your new email address", "OK");
				await CoreMethods.PopPageModel();
			}
			IsDoneChangingEmailWithSucces = false;
			IsDoneChangingEmailWithError = false;
        }
      
		public bool IsUploadingIndicatorVisible { get; set; }
		public bool IsUploadingEmail { get; set; }
		public bool IsDoneChangingEmailWithSucces { get; set; }
		public bool IsDoneChangingEmailWithError { get; set; }
		private async Task Handle_DoneEditing()
		{
			if(IsUploadingEmail)
			{
				return;
			}

			if(string.IsNullOrEmpty(newEmailEntered))
			{
				await CoreMethods.DisplayAlert("Input Error:", "When changing email, a new email must be provided", "OK");
				return;
			}

			if(string.IsNullOrEmpty(passwordEntered))
			{
				await CoreMethods.DisplayAlert("Input Error: ", "Please provided your password, in order to change email", "OK");
				return;
			}

			if(!createUserService.ValidateEmail(newEmailEntered))
			{
				await CoreMethods.DisplayAlert("Invalid Email", "The provided email does not meet email conventions, please double check and try again", "OK");
				return;
			}

			var decision = await CoreMethods.DisplayAlert("Warning, You are about to change your email!", "You are about to change your email to: " + "\n" + newEmailEntered, "Proceed", "Cancel");

			if(decision)
			{
				//All criteria has been matched change email
				IsUploadingEmail = true;
				IsUploadingIndicatorVisible = true;
                var result = await createUserService.ChangeEmail(newEmailEntered, passwordEntered);

				if(result.IsSuccesfull)
				{
					App.AccountService.UpdateEmail(newEmailEntered);
					IsDoneChangingEmailWithSucces = true;
					UserSettings.Email = newEmailEntered;
					SuccesState = SuccesStateEnum.Error;
					UserSettings.IsEmailVerified = false;
					SetCoral();
					newEmailEntered = "";
					passwordEntered = "";

				}
				else
				{
					IsDoneChangingEmailWithError = true;
					await CoreMethods.DisplayAlert("Error: ", result.Message, "OK");
				}
			}         
		}

        private void SetCoral()
		{
			EmailVerifiedColor = Color.FromHex("#C77966");
		}

        private void SetDarkGold()
		{
			EmailVerifiedColor = Color.FromHex("#A68F68");
		}

		public bool IsResendingConfirmationEmail { get; set; }
		private async Task Handle_ResendConfirmationEmail()
		{
			if (!IsResendingConfirmationEmail)
			{
				IsResendingConfirmationEmail = true;
				var choice = await CoreMethods.DisplayAlert("Resend Confirmation Email", "You are about to re-send a confirmation email to the email: " + "\n" + UserSettings.Email, "Proceed", "Cancel");

				if (choice)
				{
					var respond = await createUserService.RequestConfirmationEmailResend();

					if (!respond.IsSuccesfull)
					{
						await CoreMethods.DisplayAlert("Error: ", respond.Message, "OK");
					}
					else
					{
						await CoreMethods.DisplayAlert("Succesfull", "A new confirmation email has been send to: " + "\n" + UserSettings.Email + "\n" + "It might take a few moments.", "OK");
					}

				}
				IsResendingConfirmationEmail = false;
			}
			else
			{
				await CoreMethods.DisplayAlert("Working...", "Please wait, we are sending you your new confirmation email, but the web is abit crowded", "OK");
			}
		}
    }
}
