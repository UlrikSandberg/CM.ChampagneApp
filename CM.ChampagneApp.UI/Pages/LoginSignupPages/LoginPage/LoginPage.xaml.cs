using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.LoginSignupPages.LoginPage
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
			NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();


        }

        void Handle_ForgotPasswordClicked(object sender, System.EventArgs e)
        {
            var ViewModel = (LoginPageModel)this.BindingContext;
            if (ViewModel.ForgotPassword.CanExecute(null))
            {
                ViewModel.ForgotPassword.Execute(null);
            }
        }

        void Handle_NoAccountSignUpClicked(object sender, System.EventArgs e)
        {
            var ViewModel = (LoginPageModel)this.BindingContext;
            if(ViewModel.NavigateToSignUp.CanExecute(null))
            {
                ViewModel.NavigateToSignUp.Execute(null);
            }
        }

        void Handle_LoginClicked(object sender, System.EventArgs e)
        {
            var ViewModel = (LoginPageModel)this.BindingContext;
            if (ViewModel.Login.CanExecute(null))
            {
                ViewModel.Login.Execute(null);
            }
        }

        void Handle_OnPasswordCompleted(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
        {
            var ViewModel = (LoginPageModel)this.BindingContext;
            if(ViewModel.PasswordEntered.CanExecute(args.TextEntered))
            {
                ViewModel.PasswordEntered.Execute(args.TextEntered);
            }
        }

        void Handle_OnEmailCompleted(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
        {
            var ViewModel = (LoginPageModel)this.BindingContext;
            if (ViewModel.EmailEntered.CanExecute(args.TextEntered))
            {
                ViewModel.EmailEntered.Execute(args.TextEntered);
            }
        }

		void Handle_OnLeftNavIconClicked(object sender, System.EventArgs e)
		{
			var viewModel = (LoginPageModel)this.BindingContext;
			if(viewModel.NavigateBack.CanExecute(null))
			{
				viewModel.NavigateBack.Execute(null);
			}
		}

		void Handle_OnDidDisappear(object sender, System.EventArgs e)
		{
			var viewModel = (LoginPageModel)this.BindingContext;
			if(viewModel.DidDissappear.CanExecute(null))
			{
				viewModel.DidDissappear.Execute(null);
			}
		}
    }
}
