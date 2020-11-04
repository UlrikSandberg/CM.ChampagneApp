using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.ChangeEmailPage
{
    public partial class ChangeEmailPage : ContentPage
    {
        public ChangeEmailPage()
        {
			InitializeComponent();
        }

		void Handle_OnNewEmailFieldCompleted(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
		{
			var viewModel = (ChangeEmailPageModel)this.BindingContext;
			if(viewModel.NewEmailEntered.CanExecute(args.TextEntered))
			{
				viewModel.NewEmailEntered.Execute(args.TextEntered);
			}
		}

		void Handle_OnPasswordFieldCompleted(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
		{
			var viewModel = (ChangeEmailPageModel)this.BindingContext;
			if(viewModel.PasswordEntered.CanExecute(args.TextEntered))
			{
				viewModel.PasswordEntered.Execute(args.TextEntered);
			}
		}
    }
}
