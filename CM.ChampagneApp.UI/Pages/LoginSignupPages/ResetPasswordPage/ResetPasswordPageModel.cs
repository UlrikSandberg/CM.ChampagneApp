using System;
using System.Windows.Input;
using FreshMvvm;
using Xamarin.Forms;
using CM.ChampagneApp.UI.UIFacade.Services;
using CM.ChampagneApp.Instrumentation;
using System.Threading.Tasks;

namespace CM.ChampagneApp.UI.Pages.LoginSignupPages.ResetPasswordPage
{
    public class ResetPasswordPageModel : FreshBasePageModel
    {
        public ICommand NavigateBack { get; set; }
        public ICommand ResetPassword { get; set; }
        public ICommand EmailEntered { get; set; }

		private string enteredEmail = "";

		private readonly IUICreateUserService createUserService;
        private readonly IEventCollector eventCollector;

        public ResetPasswordPageModel(IUICreateUserService createUserService, IEventCollector eventCollector)
        {
			this.createUserService = createUserService;
            this.eventCollector = eventCollector;
            NavigateBack = new Command(async () => await OnNavigateBack());
            ResetPassword = new Command(async () => await OnResetPassword());
            EmailEntered = new Command<string>(OnEmailEntered);

        }

		public bool IsResetPasswordInProgress { get; set; }
        private async Task OnResetPassword()
        {
            eventCollector.TrackIntention("ResetPassword");
			if(string.IsNullOrEmpty(enteredEmail))
			{
				await CoreMethods.DisplayAlert("Request Password Recovery", "You must enter an email address to request a password reset", "OK");
				return;
			}

			if(!createUserService.ValidateEmail(enteredEmail))
			{
				await CoreMethods.DisplayAlert("Request Password Recovery", "The entered email: " + "\n" + enteredEmail + "\nIs not a valid email address. Please double check and try again", "OK");
				return;
			}         

			//Ask if they are really sure they want to reset on the entered email:
			var result = await CoreMethods.DisplayAlert("Request Password Recovery", "You are about to request a password recovery procedure for the email:" + "\n" + enteredEmail + "\n", "Request", "Cancel");

			if(result)
			{
				IsResetPasswordInProgress = true;

				var response = await createUserService.RequestPasswordReset(enteredEmail);            

				if(response.IsSuccesfull)
				{
					IsResetPasswordInProgress = false;
					await CoreMethods.DisplayAlert("Successfull", "A password reset request has been send to the email:" + "\n" + enteredEmail + "\nPlease check your inbox", "OK");
					await CoreMethods.PopPageModel();
				}
				else
				{
					IsResetPasswordInProgress = false;
					await CoreMethods.DisplayAlert("Password Recovery Failure", "Something went wrong... This may be caused if you are out of service. Try in a few minutes..." + "\nError: " + response.Message, "OK");
				}
			}         
        }

        private void OnEmailEntered(string email)
        {
			enteredEmail = email;
        }

        private async Task OnNavigateBack()
        {
            eventCollector.TrackIntention("ResetPassword.GoBack");
            await CoreMethods.PopPageModel();
        }
    }
}
