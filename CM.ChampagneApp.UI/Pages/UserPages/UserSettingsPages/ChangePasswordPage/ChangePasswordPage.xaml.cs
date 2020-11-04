using System;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.ChangePasswordPage
{
    public partial class ChangePasswordPage : ContentPage
    {
        public ChangePasswordPage()
        {
			NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        void Handle_OnLeftNavIconClicked(object sender, System.EventArgs e)
        {
            var ViewModel = (ChangePasswordPageModel)this.BindingContext;
            if(ViewModel.NavigateBack.CanExecute(null))
            {
                ViewModel.NavigateBack.Execute(null);
            }
        }

        void Handle_OnRightNavIconClicked(object sender, System.EventArgs e)
        {
            var ViewModel = (ChangePasswordPageModel)this.BindingContext;
            if(ViewModel.EditingDone.CanExecute(null))
            {
                ViewModel.EditingDone.Execute(null);
            }
        }

        void Handle_OnPasswordEntryFieldCompleted(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
        {
            var ViewModel = (ChangePasswordPageModel)this.BindingContext;
            if (ViewModel.PasswordEntered.CanExecute(args.TextEntered))
            {
                ViewModel.PasswordEntered.Execute(args.TextEntered);
            }
        }

        void Handle_OnNewPasswordEntryFieldCompleted(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
        {
            var ViewModel = (ChangePasswordPageModel)this.BindingContext;
            if (ViewModel.NewPasswordEntered.CanExecute(args.TextEntered))
            {
                ViewModel.NewPasswordEntered.Execute(args.TextEntered);
            }
        }

        void Handle_OnConfirmPasswordEntryFieldCompleted(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
        {
            var ViewModel = (ChangePasswordPageModel)this.BindingContext;
            if (ViewModel.ConfirmPasswordEntered.CanExecute(args.TextEntered))
            {
                ViewModel.ConfirmPasswordEntered.Execute(args.TextEntered);
            }
        }


		void Handle_OnDidDisappear(object sender, System.EventArgs e)
		{
			var viewModel = (ChangePasswordPageModel)this.BindingContext;
			if(viewModel.UploadingIndicatorDissappeared.CanExecute(null))
			{
				viewModel.UploadingIndicatorDissappeared.Execute(null);
			}
		}

		void Handle_OnPasswordTextChange(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
		{
			var viewModel = (ChangePasswordPageModel)this.BindingContext;
			if(viewModel.PasswordChanged.CanExecute(args.TextEntered))
			{
				viewModel.PasswordChanged.Execute(args.TextEntered);
			}
		}

		void Handle_OnNewPasswordTextChange(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
        {
			var viewModel = (ChangePasswordPageModel)this.BindingContext;
			if(viewModel.NewPasswordChanged.CanExecute(args.TextEntered))
			{
				viewModel.NewPasswordChanged.Execute(args.TextEntered);
			}
        }

		void Handle_OnConfirmPasswordTextChange(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
        {
			var viewModel = (ChangePasswordPageModel)this.BindingContext;
			if(viewModel.ConfirmPasswordChanged.CanExecute(args.TextEntered))
			{
				viewModel.ConfirmPasswordChanged.Execute(args.TextEntered);
			}
        }
    }
}
