using System;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.LoginSignupPages.ResetPasswordPage
{
    public partial class ResetPasswordPage : ContentPage
    {
        public ResetPasswordPage()
        {
			NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        void Handle_BackToLoginClicked(object sender, System.EventArgs e)
        {
            var ViewModel = (ResetPasswordPageModel)this.BindingContext;
            if(ViewModel.NavigateBack.CanExecute(null))
            {
                ViewModel.NavigateBack.Execute(null);
            }
        }

        void Handle_ResetClicked(object sender, System.EventArgs e)
        {
            var ViewModel = (ResetPasswordPageModel)this.BindingContext;
            if(ViewModel.ResetPassword.CanExecute(null))
            {
                ViewModel.ResetPassword.Execute(null);
            }
        }


        void Handle_OnEmailFieldCompleted(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
        {
            var ViewModel = (ResetPasswordPageModel)this.BindingContext;
            if(ViewModel.EmailEntered.CanExecute(args.TextEntered))
            {
                ViewModel.EmailEntered.Execute(args.TextEntered);
            }
        }

        void Handle_OnLeftNavIconClicked(object sender, System.EventArgs e)
        {
            var ViewModel = (ResetPasswordPageModel)this.BindingContext;
            {
                if(ViewModel.NavigateBack.CanExecute(null))
                {
                    ViewModel.NavigateBack.Execute(null);
                }
            }
        }
    }
}
