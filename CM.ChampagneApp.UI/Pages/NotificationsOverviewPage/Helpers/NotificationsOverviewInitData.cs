using System;
namespace CM.ChampagneApp.UI.Pages.NotificationsOverviewPage
{
    public class NotificationsOverviewInitData
    {
		public bool IsSignUpOrLogin { get; set; }

        public NotificationsOverviewInitData(bool isSignUpOrLogin)
        {
			IsSignUpOrLogin = isSignUpOrLogin;
        }
    }
}
