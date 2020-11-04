using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Lottie.Forms;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace CM.ChampagneApp.UI.Pages.LoginSignupPages.WelcomeScreenPage
{
    public partial class WelcomeScreenPage : ContentPage
    {
        public WelcomeScreenPage()
        {
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
         
            InitializeComponent();



        }

		protected override void OnAppearing()
		{
			base.OnAppearing();

            FadeContent();
		}

        private async void FadeContent()
		{         
			await MainContent.FadeTo(1, 300);
            StartAnimation();
             
		}

		private async void StartAnimation()
		{
			var currentAnimation = "OnboardingAnimation.json";
			var binding = (WelcomeScreenPageModel)this.BindingContext;
			animationView.Play();
			await Task.Delay(7000);

			while (binding.IsCreatingUser)
			{
				animationView.Pause();
				if(currentAnimation.Equals("OnboardingAnimation.json"))
				{
					currentAnimation = "OnboardingAnimation2.json";
					animationView.Animation = "OnboardingAnimation2.json";
				}
				else if(currentAnimation.Equals("OnboardingAnimation2.json"))
				{
					currentAnimation = "OnboardingAnimation.json";
					animationView.Animation = "OnboardingAnimation.json";
				}

				animationView.Play();
				await Task.Delay(7000);
			}
            
			animationView.Pause();
			animationView.Animation = "SuccesCheckAnimation.json";
			animationContainer.Padding = new Thickness(0, 40, 0, 40);
			animationView.Speed = (float)0.8;
			animationView.Play();
			      
		}

		void Handle_OnFinish(object sender, System.EventArgs e)
		{
			var viewModel = (WelcomeScreenPageModel)this.BindingContext;
			if(viewModel.AnimationDone.CanExecute(null))
			{
				viewModel.AnimationDone.Execute(null);
			}
		}
	}
}
