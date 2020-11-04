using System;
using Xamarin.Forms.Platform.iOS;
using System.Runtime.CompilerServices;
using System.Linq;
using Xamarin.Forms;
using CM.ChampagneApp.UI.Helpers;
using UIKit;
using CM.ChampagneApp.UI.Helpers.FreshMvvmHelpers;

[assembly: ExportRenderer(typeof(Xamarin.Forms.TabbedPage), typeof(CM.ChampagneApp.iOS.Renderers.TabbarPageRenderer))]
namespace CM.ChampagneApp.iOS.Renderers
{
	public class TabbarPageRenderer : TabbedRenderer, IDisposable
    {
		public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            TabBar.UnselectedItemTintColor = UIColor.White;

            MessagingCenter.Subscribe<TabBarBadgeService, NotificationBadgeCountMessage>(this, MessagingCenterSubscriptionKeys.SetNotificationBadge, (page, badgeCountMessage) => 
			{
                if (TabBar != null)
                {
                    if (TabBar.Items != null)
                    {
                        var tab = TabBar.Items[3];
						tab.BadgeColor = Color.FromHex("#C77966").ToUIColor();
						tab.BadgeValue = badgeCountMessage.BadgeCount;
                    }
                }
            });
        }

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);

			MessagingCenter.Unsubscribe<TabBarBadgeService, NotificationBadgeCountMessage>(this, MessagingCenterSubscriptionKeys.SetNotificationBadge);
		}      
	}
}
