using System;
using FreshMvvm;
using CM.ChampagneApp.UI.UIFacade.Models;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using System.Windows.Input;
using Xamarin.Forms;
using System.Transactions;
using CM.ChampagneApp.UI.UIFacade.Models.UISubscription;
using CM.ChampagneApp.UI.PageModelInitData;
using CM.ChampagneApp.Instrumentation;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.Pages.SharedPages.WebViewPage;
using CM.ChampagneApp.UI.Pages.SharedPages.Helpers;

namespace CM.ChampagneApp.UI.Pages.UserPages.ChooseSubscriptionPage
{
	public class ChooseSubscriptionPageModel : BasePageModel
    {
		public ICommand SubscriptionClicked { get; set; }
		public ICommand SegmentChanged { get; set; }

		public ICommand NavigatePrivacyPolicy { get; set; }
		public ICommand NavigateTermsOfUse { get; set; }

		public UISubscriptionModel ChosenSubscription { get; set; }

		private UISubscriptionModel SubscriptionOption1 = null;
		private UISubscriptionModel SubscriptionOption2 = null;
		private UISubscriptionModel SubscriptionOption3 = null;

        public ChooseSubscriptionPageModel(IEventCollector ec) : base(ec)
        {
			SubscriptionClicked = new Command(async () => await OnSubscriptionClicked());
			SegmentChanged = new Command<double>(OnSegmentChanged);
			NavigatePrivacyPolicy = new Command(async () => await OnNavigatePrivacyPolicy());
			NavigateTermsOfUse = new Command(async () => await OnNavigateTermsOfUse());

			SubscriptionOption1 = new UISubscriptionModel
			{
				Id = 0,
				Title = "CHAMPAGNE LOVERS",
				Description = "3 Months access to all app features",
				TotalAmount = "€22.5",
				WeeklyAmount = "€7.5 / Week" 
			};

			SubscriptionOption2 = new UISubscriptionModel
            {
                Id = 1,
                Title = "CHAMPAGNE LOVERS",
				Description = "6 Months access to all app features",
                TotalAmount = "€45",
                WeeklyAmount = "€7.5 / Week"
            };
            
			SubscriptionOption3 = new UISubscriptionModel
            {
                Id = 2,
                Title = "CHAMPAGNE LOVERS",
				Description = "12 Months access to all app features",
                TotalAmount = "€90",
                WeeklyAmount = "€7.5 / Week"
            };

			ChosenSubscription = SubscriptionOption1;
        }

        private async Task OnSubscriptionClicked()
		{
			var response = await CoreMethods.DisplayAlert("Confirm Purchase: CHAMPAGNE LOVERS Member Subscription", "Are you sure you want to buy the " + ChosenSubscription.Title + " member subscription: " + ChosenSubscription.Description + ". For a total of " + ChosenSubscription.TotalAmount + " a " + ChosenSubscription.WeeklyAmount, "Confirm Purchase", "Cancel");

			if(response)
			{
				System.Diagnostics.Debug.WriteLine("User confirmed purchase of the member subscription: " + ChosenSubscription.Title + " for a total of " + ChosenSubscription.TotalAmount);
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("User cancelled the subscription");
			}
		}

        private void OnSegmentChanged(double segmentIndex)
		{
			switch(segmentIndex)
			{
				case 0:
					ChosenSubscription = SubscriptionOption1;
					break;

				case 1:
					ChosenSubscription = SubscriptionOption2;
					break;

				case 2:
					ChosenSubscription = SubscriptionOption3;
					break;               
			}
		}

        private async Task OnNavigateTermsOfUse()
		{
			await CoreMethods.PushPageModel<WebViewPageModel>(new WebViewInitData("https://champagnemoments.eu/terms-of-use/"));
		}

        private async Task OnNavigatePrivacyPolicy()
		{
			await CoreMethods.PushPageModel<WebViewPageModel>(new WebViewInitData("https://champagnemoments.eu/terms-of-use/"));
		}
	}
}
