using System;
using CM.ChampagneApp.UI.Dependency;
using CM.ChampagneApp.UI.Helpers;
using Xamarin.Forms;
using Plugin.Badge;

[assembly: Dependency(typeof(CM.ChampagneApp.UI.Helpers.FreshMvvmHelpers.TabBarBadgeService))]
namespace CM.ChampagneApp.UI.Helpers.FreshMvvmHelpers
{
	public class TabBarBadgeService : ITabbarBadgeService
    {      
		private int currentBadgeValue = 0;
		private string CurrentStringBadgeVale = "0";
              
		public void IncrementBadgeValue()
		{
			currentBadgeValue++;
			SendMessage(new NotificationBadgeCountMessage(currentBadgeValue.ToString()));
		}

		public void IncrementBadgeValue(int incrementBy)
		{
			currentBadgeValue += incrementBy;
			if (currentBadgeValue > 0)
			{
				SendMessage(new NotificationBadgeCountMessage(currentBadgeValue.ToString()));
				CrossBadge.Current.SetBadge(currentBadgeValue);
			}
		}      

		public void SetBadge(int badgeValue)
		{
			if (badgeValue > 0)
			{            
				currentBadgeValue = badgeValue;
				SendMessage(new NotificationBadgeCountMessage(currentBadgeValue.ToString()));
				CrossBadge.Current.SetBadge(currentBadgeValue);
			}
		}

		private void SendMessage(NotificationBadgeCountMessage badgeCountMessage)
		{
			MessagingCenter.Send<TabBarBadgeService, NotificationBadgeCountMessage>(this, MessagingCenterSubscriptionKeys.SetNotificationBadge, badgeCountMessage);   
		}

        public void ClearBadge()
		{
			currentBadgeValue = 0;
			SendMessage(new NotificationBadgeCountMessage(null));
			CrossBadge.Current.ClearBadge();
		}

        public int CurrentBadgeValue()
		{
			return currentBadgeValue;
		}
	}
}
