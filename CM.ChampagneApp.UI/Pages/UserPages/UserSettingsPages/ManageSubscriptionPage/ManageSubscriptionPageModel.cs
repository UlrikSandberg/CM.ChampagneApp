using System;
using FreshMvvm;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using System.Windows.Input;
using Xamarin.Forms;
using CM.ChampagneApp.Instrumentation;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.Pages.UserPages.ChooseSubscriptionPage;

namespace CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.ManageSubscriptionPage
{
	public class ManageSubscriptionPageModel : BasePageModel
    {
		public bool IsSubscriptionActive { get; set; } = true;

		public ICommand RestorePurchase { get; set; }
		public ICommand MemberSubscriptionActive { get; set; }
		public ICommand MemberSubscriptionInactive { get; set; }

        public ManageSubscriptionPageModel(IEventCollector ec) : base(ec)
        {
			RestorePurchase = new Command(async () => await OnRestorePurhcase());
			MemberSubscriptionActive = new Command(async () => await OnMemberSubscriptionActive());
			MemberSubscriptionInactive = new Command(async () => await OnMemberSubscriptionInactive());
        }

        private async Task OnRestorePurhcase()
		{
			var respone = await CoreMethods.DisplayAlert("Restore Purchase", "Restoring purchase will take you to the iTunes AppStore", "Restore purchase", "Cancel");
		}

        private async Task OnMemberSubscriptionActive()
		{
			await CoreMethods.PushPageModel<ChooseSubscriptionPageModel>();
		}

        private async Task OnMemberSubscriptionInactive()
		{
			await CoreMethods.PushPageModel<ChooseSubscriptionPageModel>();
		}
    }
}
