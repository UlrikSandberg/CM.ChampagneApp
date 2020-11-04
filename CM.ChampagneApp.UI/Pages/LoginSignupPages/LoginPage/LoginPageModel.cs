using System;
using System.Windows.Input;
using CM.ChampagneApp.UI.UIFacade.Services;
using FreshMvvm;
using Xamarin.Forms;
using CM.ChampagneApp.UI.Helpers;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.Pages.LoginSignupPages.ResetPasswordPage;
using CM.ChampagneApp.UI.Pages.LoginSignupPages.RegisterPage;

namespace CM.ChampagneApp.UI.Pages.LoginSignupPages.LoginPage
{
	public class LoginPageModel : FreshBasePageModel
	{

		public string Email { get; set; }
		private System.Security.SecureString Password;

		public ICommand NavigateBack { get; set; }
        public ICommand NavigateToSignUp { get; set; }
        public ICommand Login { get; set; }
        public ICommand ForgotPassword { get; set; }

        public ICommand EmailEntered { get; set; }
        public ICommand PasswordEntered { get; set; }

		public ICommand DidDissappear { get; set; }

		private readonly IUIAuthenticationService uIAuthenticationService;

		public LoginPageModel(IUIAuthenticationService uIAuthenticationService )
        {
			Email = App.AccountService.Username();

            this.uIAuthenticationService = uIAuthenticationService;

			NavigateToSignUp = new Command(async () => await OnNavigateToSignUp());
            Login = new Command(async () => await OnLogin());
            ForgotPassword = new Command(async () => await OnForgotPassword());
            EmailEntered = new Command<string>(OnEmailEntered);
            PasswordEntered = new Command<string>(OnPasswordEntered);
			NavigateBack = new Command(async () => await OnNavigateBack());
			DidDissappear = new Command(async () => await OnDidDissappear());

        }

        private async Task OnNavigateBack()
		{
			await CoreMethods.PopPageModel();
		}

        private void OnEmailEntered(string email)
        {
			Email = email;
			System.Diagnostics.Debug.WriteLine("Email entered: " + email);
        }

        private void OnPasswordEntered(string password)
        {
			Password = new System.Security.SecureString();
			foreach(char c in password.Trim())
			{
				Password.AppendChar(c);
			}

			System.Diagnostics.Debug.WriteLine("Secure password toString(): "+ Password);
			System.Diagnostics.Debug.WriteLine(new System.Net.NetworkCredential(string.Empty, Password).Password);
        }

        private async Task OnNavigateToSignUp()
        {
            await CoreMethods.PushPageModel<RegisterPageModel>();
        }

        private async Task OnForgotPassword()
        {
            await CoreMethods.PushPageModel<ResetPasswordPageModel>();
        }

        private async Task OnDidDissappear()
		{

			if(IsDoneLoadingWithSucces)
			{
				IsLoading = false;
                IsLoadingIndicatorVisible = false;
                IsDoneLoadingWithError = false;
                IsDoneLoadingWithSucces = false;

				var containerId = App.NavigationContainerManager.CreateMainContainer(true);
                await CoreMethods.PopToRoot(false);
                CoreMethods.SwitchOutRootNavigation(containerId);        
			}
			else
			{
				IsLoading = false;
                IsLoadingIndicatorVisible = false;
                IsDoneLoadingWithError = false;
                IsDoneLoadingWithSucces = false;
			}         
		}


		public bool IsLoading { get; set; } = false;
		public bool IsLoadingIndicatorVisible { get; set; } = false;
		public bool IsDoneLoadingWithSucces { get; set; } = false;
		public bool IsDoneLoadingWithError { get; set; } = false;
        private async Task OnLogin()
        {
            if(Email == null || Password == null)
            {
                await CoreMethods.DisplayAlert("Error:", "Neither email nor password field must be empty", "OK");
            }

			if(Email.Trim().Length < 1 || Email == null || Password.Length < 1 || Password == null)
			{
				await CoreMethods.DisplayAlert("Error: ", "Neither email nor password field must be empty", "OK");
			}

			//Login the user
			IsLoading = true;
			IsLoadingIndicatorVisible = true;
            DTO.Documents.BaseResponse result = await uIAuthenticationService.Login(Email, new System.Net.NetworkCredential(string.Empty, Password).Password);

			if (!result.IsSuccesfull)
			{            
				await CoreMethods.DisplayAlert("Login error", result.Message, "OK");

				//IsDoneLoadingWithError = true;
				IsLoadingIndicatorVisible = false;
			}
			else
			{
				IsDoneLoadingWithSucces = true;
			}
        }
    }
}
