using System.Threading.Tasks;
using System.Windows.Input;
using CM.ChampagneApp.Instrumentation;
using CM.ChampagneApp.UI.Elements.Helpers.ElementEnum;
using CM.ChampagneApp.UI.PageModelInitData;
using CM.ChampagneApp.UI.Pages.LoginSignupPages.LoginPage;
using CM.ChampagneApp.UI.Pages.LoginSignupPages.WelcomeScreenPage;
using CM.ChampagneApp.UI.Pages.LoginSignupPages.WelcomeScreenPage.Helpers;
using CM.ChampagneApp.UI.Pages.SharedPages.Helpers;
using CM.ChampagneApp.UI.Pages.SharedPages.WebViewPage;
using CM.ChampagneApp.UI.UIFacade.Services;
using FreshMvvm;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.LoginSignupPages.RegisterPage
{
    public class RegisterPageModel : FreshBasePageModel
    {

        private string email = null;
        private string username = null;
        private string password = null;

        private bool IsEmailValid = false;
        private bool IsUsernameValid = false;

        public string EmailErrorMessage { get; set; } = null;
        public string UsernameErrorMessage { get; set; } = null;
        public string PasswordErrorMessage { get; set; } = null;

        public ICommand EmailEntered { get; set; }
        public ICommand UsernameEntered { get; set; }
        public ICommand PasswordTextChanged { get; set; }
        public ICommand PasswordEntered { get; set; }
        public ICommand SignUp { get; set; }
        public ICommand TermsOfUseClicked { get; set; }
        public ICommand NavigateToLogin { get; set; }
        public ICommand NavigateBack { get; set; }

        private readonly IUICreateUserService createUserService;
        private readonly IEventCollector eventCollector;

        public RegisterPageModel(IUICreateUserService uiCreateUserService, IEventCollector eventCollector)
        {

            createUserService = uiCreateUserService;
            this.eventCollector = eventCollector;
            EmailEntered = new Command<string>(async (x) => await OnEmailEntered(x));
            UsernameEntered = new Command<string>(async (x) => await OnUsernameEntered(x));
            SignUp = new Command(async () => await OnSignUp());
            TermsOfUseClicked = new Command(async () => await OnTermsOfUse());
            NavigateToLogin = new Command(async () => await OnNavigateToLogin());
            PasswordTextChanged = new Command<string>(OnPasswordTextChanged);
            NavigateBack = new Command(async () => await OnNavigateBack());

        }

        private async Task OnNavigateToLogin()
        {
            eventCollector.TrackIntention("Signup.Login");
            await CoreMethods.PushPageModel<LoginPageModel>();
        }

        private async Task OnTermsOfUse()
        {
            eventCollector.TrackIntention("Signup.TermsOfUse");
            var InitData = new WebViewInitData("https://champagnemoments.eu/terms-of-use-app/?utm_source=Champagne%20App&utm_medium=AppLink&utm_campaign=AppLink");
            await CoreMethods.PushPageModel<WebViewPageModel>(InitData);
        }

        private async Task OnNavigateBack()
        {
            await CoreMethods.PopPageModel();
        }

        private async Task OnSignUp()
        {
            eventCollector.TrackIntention("Signup");
            var credentialIsValid = true;

            if ((string.IsNullOrEmpty(this.email)))
            {
                credentialIsValid = false;
                await CoreMethods.DisplayAlert("Invalid email error", "Please provide an email", "OK");
                return;
            }

            if (string.IsNullOrEmpty(this.username))
            {
                credentialIsValid = false;
                await CoreMethods.DisplayAlert("Invalid name error", "Please provide a name", "OK");
                return;
            }

            if (string.IsNullOrEmpty(this.password))
            {
                credentialIsValid = false;
                await CoreMethods.DisplayAlert("Invalid password error", "Please provide a password", "OK");
                return;
            }

            //Validate the different fields
            if (!createUserService.ValidateEmail(this.email))
            {
                credentialIsValid = false;
                await CoreMethods.DisplayAlert("Invalid email", "Please check, and re-type your email", "OK");
                return;
            }

            if (!createUserService.ValidateName(this.username))
            {
                credentialIsValid = false;
                await CoreMethods.DisplayAlert("Invalid name", "The name must not contain any of the following special characters " + "\n" + " !#$%&'()*+,./:;<=>?@[\\]^`{|}~", "OK");
                return;
            }

            if (!createUserService.ValidatePassword(this.password))
            {
                credentialIsValid = false;
                await CoreMethods.DisplayAlert("Invalid password", "The password must not contain any of the following special characters nor spaces " + "\n" + " !#$%&'()*+,-./:;<=>?@[\\]^_`{|}~", "OK");
                return;
            }

            if (!IsEmailValid || !IsUsernameValid)
            {
                credentialIsValid = false;
                await CoreMethods.DisplayAlert("Username & Email is already used", "The provided email or username is already being used by an account", "OK");
            }

            if (credentialIsValid)
            {
                /*
                var result = await createUserService.CreateUser(this.email.ToLower(), this.username, this.password);
                if(!result.IsSuccesfull)
                {
                    await CoreMethods.DisplayAlert("User creation error", result.Message, "OK");
                }
                else
                {
                    var containerId = App.NavigationContainerManager.CreateMainContainer();
                    CoreMethods.PopToRoot(false);
                    CoreMethods.SwitchOutRootNavigation(containerId);
                }
                */
                await CoreMethods.PushPageModel<WelcomeScreenPageModel>(new WelcomeScreenInitData(this.email, this.username, this.password));
            }
        }

        public bool IsValidatingEmail { get; set; } = false;
        public SuccesStateEnum EmailValidationState { get; set; } = SuccesStateEnum.Default;
        private async Task OnEmailEntered(string email)
        {
            eventCollector.TrackIntention("Signup.EmailEntered");
            //trim each end from whitespace and validate email
            this.email = email.Trim();

            //validate email!
            IsEmailValid = false;
            EmailValidationState = SuccesStateEnum.Default;
            EmailErrorMessage = null;
            IsValidatingEmail = true;
            if (!email.Equals(""))
            {
                if (createUserService.ValidateEmail(email))
                {
                    var response = await createUserService.CheckEmailAvailability(email);
                    if (!response.IsSuccesfull)
                    {
                        IsValidatingEmail = false;
                        EmailValidationState = SuccesStateEnum.Error;
                        EmailErrorMessage = response.Message;
                        return;
                    }

                    IsValidatingEmail = false;
                    IsEmailValid = true;
                    EmailValidationState = SuccesStateEnum.Succes;
                    return;
                }
            }

            IsValidatingEmail = false;
            EmailErrorMessage = "Email is not valid according to conventions";
            EmailValidationState = SuccesStateEnum.Error;
            //Show message!         
        }

        public bool IsValidatingUsername { get; set; } = false;
        public SuccesStateEnum UsernameValidationState { get; set; } = SuccesStateEnum.Default;
        private async Task OnUsernameEntered(string username)
        {
            eventCollector.TrackIntention("Signup.UsernameEntered");
            this.username = username.Trim();

            //Validate username
            UsernameValidationState = SuccesStateEnum.Default;
            IsUsernameValid = false;
            UsernameErrorMessage = null;
            IsValidatingUsername = true;
            if (!username.Equals(""))
            {
                if (createUserService.ValidateName(username))
                {
                    var response = await createUserService.CheckUsernameAvailability(username);
                    if (!response.IsSuccesfull)
                    {
                        IsValidatingUsername = false;
                        UsernameValidationState = SuccesStateEnum.Error;
                        UsernameErrorMessage = response.Message;
                        return;
                    }
                    IsValidatingUsername = false;
                    IsUsernameValid = true;
                    UsernameValidationState = SuccesStateEnum.Succes;
                    return;
                }
            }

            IsValidatingUsername = false;
            UsernameValidationState = SuccesStateEnum.Error;
            UsernameErrorMessage = "May not contain special characters or spaces";
        }

        public bool IsValidatingPassword { get; set; } = false;
        public SuccesStateEnum PasswordValidationState { get; set; } = SuccesStateEnum.Default;

        private void OnPasswordTextChanged(string password)
        {
            eventCollector.TrackIntention("Signup.PasswordEntered");
            PasswordValidationState = SuccesStateEnum.Default;
            PasswordErrorMessage = null;
            IsValidatingPassword = true;
            this.password = password;
            if (!password.Equals(""))
            {
                if (!createUserService.ValidatePassword(password))
                {
                    IsValidatingPassword = false;
                    PasswordErrorMessage = "May not contain any special characters";
                    PasswordValidationState = SuccesStateEnum.Error;
                }
                else
                {
                    if (createUserService.ValidatePasswordLength(password))
                    {
                        IsValidatingPassword = false;
                        PasswordValidationState = SuccesStateEnum.Succes;
                        return;
                    }
                    else
                    {
                        IsValidatingPassword = false;
                        PasswordErrorMessage = "Password should be at least 6 characters long";
                        PasswordValidationState = SuccesStateEnum.Error;
                        return;
                    }
                }
            }
            IsValidatingPassword = false;
            PasswordErrorMessage = "Password should be at least 6 characters long";
            PasswordValidationState = SuccesStateEnum.Error;
        }
    }
}
