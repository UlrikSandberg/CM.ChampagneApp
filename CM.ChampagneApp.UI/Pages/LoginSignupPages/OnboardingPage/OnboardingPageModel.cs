using System;
using FreshMvvm;
using System.Collections.ObjectModel;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;
using CM.ChampagneApp.UI.Pages;
using CM.ChampagneApp.UI.UIReadModels;
using System.Windows.Input;
using Xamarin.Forms;
using CM.ChampagneApp.Instrumentation;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.Pages.LoginSignupPages.LoginPage;
using CM.ChampagneApp.UI.Pages.LoginSignupPages.RegisterPage;

namespace CM.ChampagneApp.UI.Pages.LoginSignupPages.OnboardingPage
{
	public class OnboardingPageModel : FreshBasePageModel
    {
        private readonly IEventCollector eventCollector;

        public ObservableCollection<OnboardingCardModel> OnboardingPages { get; set; } = null;

		public string trial { get; set; } = "Test tekst";

		public ICommand SignUp { get; set; }
		public ICommand Login { get; set; }

        public OnboardingPageModel(IEventCollector eventCollector)
        {
            eventCollector.TrackPageView("Onboarding");
			var onboardingPage1 = new OnboardingCardModel();
			var onboardingPage2 = new OnboardingCardModel();
			var onboardingPage3 = new OnboardingCardModel();

			onboardingPage1.ImageUrl = "OnboardingImage1.png";
			onboardingPage2.ImageUrl = "OnboardingImage2.png";
			onboardingPage3.ImageUrl = "OnboardingImage3.png";

			onboardingPage1.Body = "Discover the world of champagne with Champagne Moments and enter a community for champagne lovers";
			onboardingPage2.Body = "Explore the craftsmanship that makes every single champagne unique";
			onboardingPage3.Body = "We have collected the most famous and luxurious brands, and the hidden gems of the region";

			if(App.DisplaySettings.Width > 370)
			{
				onboardingPage1.Title = "Welcome";
				onboardingPage2.Title = "Behind";
				onboardingPage3.Title = "Experience";
			}

            
			var list = new ObservableCollection<OnboardingCardModel>();
			list.Add(onboardingPage1);
			list.Add(onboardingPage2);
			list.Add(onboardingPage3);

			OnboardingPages = list;

			SignUp = new Command(async () => await OnSignup());
			Login = new Command(async () => await OnLogin());
            this.eventCollector = eventCollector;
        }


        private async Task OnSignup()
		{
            eventCollector.TrackIntention("Onboarding.Signup");
			await CoreMethods.PushPageModel<RegisterPageModel>();
		}

        private async Task OnLogin()
		{
            eventCollector.TrackIntention("Onboarding.Login");
			await CoreMethods.PushPageModel<LoginPageModel>();
		}
    }
}
