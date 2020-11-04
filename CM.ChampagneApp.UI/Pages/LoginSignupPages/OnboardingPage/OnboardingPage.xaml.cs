using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace CM.ChampagneApp.UI.Pages.LoginSignupPages.OnboardingPage
{
    public partial class OnboardingPage : ContentPage
    {
        public OnboardingPage()
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
		    InitializeComponent();

            if(App.IsOnIphoneX)
            {
                Container.Margin = new Thickness(0, 40, 0, 40);
            }
        }

		void Handle_SignUpPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if(e.PropertyName.Equals("Width") || e.PropertyName.Equals("Right"))
			{
				//SignUpBtn.CornerRadius = (int)SignUpBtn.Height / 2;
			}
		}

		void Handle_LoginPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Width") || e.PropertyName.Equals("Right"))
            {
				//LoginBtn.CornerRadius = (int)SignUpBtn.Height / 2;
            }
        }

		void Handle_LoginClicked(object sender, System.EventArgs e)
		{
			var viewModel = (OnboardingPageModel)this.BindingContext;
			if(viewModel.Login.CanExecute(null))
			{
				viewModel.Login.Execute(null);
			}
		}

		void Handle_SignUpClicked(object sender, System.EventArgs e)
        {
			var viewModel = (OnboardingPageModel)this.BindingContext;

			if(viewModel.SignUp.CanExecute(null))
			{
				viewModel.SignUp.Execute(null);
			}

        }
    }
}
