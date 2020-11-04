using CM.ChampagneApp.UI.CustomEventArgs;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.LoginSignupPages.RegisterPage
{
    public partial class RegisterPage : ContentPage
    {
        private RegisterPageModel _viewModel;

        public RegisterPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            _viewModel = this.BindingContext as RegisterPageModel;
        }

        void Handle_OnEmailCompleted(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
        {
            if (_viewModel.EmailEntered.CanExecute(args.TextEntered))
            {
                _viewModel.EmailEntered.Execute(args.TextEntered);
            }
        }

        void Handle_OnUsernameCompleted(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
        {
            if (_viewModel.UsernameEntered.CanExecute(args.TextEntered))
            {
                _viewModel.UsernameEntered.Execute(args.TextEntered);
            }
        }

        void Handle_OnPasswordCompleted(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
        {

        }

        void Handle_OnSignUpClicked(object sender, System.EventArgs e)
        {
            if (_viewModel.SignUp.CanExecute(null))
            {
                _viewModel.SignUp.Execute(null);
            }
        }

        void Handle_OnTermsOfPoliciesClicked(object sender, System.EventArgs e)
        {
            if (_viewModel.TermsOfUseClicked.CanExecute(null))
            {
                _viewModel.TermsOfUseClicked.Execute(null);
            }
        }

        void Handle_OnAlreadyHaveAccountClicked(object sender, System.EventArgs e)
        {
            if (_viewModel.NavigateToLogin.CanExecute(null))
            {
                _viewModel.NavigateToLogin.Execute(null);
            }
        }

        void Handle_OnTextChange(object sender, TextEnteredEventArgs args)
        {
            if (_viewModel.PasswordTextChanged.CanExecute(args.TextEntered))
            {
                _viewModel.PasswordTextChanged.Execute(args.TextEntered);
            }
        }

        void Handle_OnLeftNavIconClicked(object sender, System.EventArgs e)
        {
            if (_viewModel.NavigateBack.CanExecute(null))
            {
                _viewModel.NavigateBack.Execute(null);
            }
        }
    }
}
