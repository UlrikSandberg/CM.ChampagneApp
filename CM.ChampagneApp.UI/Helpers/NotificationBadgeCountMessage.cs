using System;
namespace CM.ChampagneApp.UI.Helpers
{
    public class NotificationBadgeCountMessage
    {
		public string BadgeCount { get; set; }

        public NotificationBadgeCountMessage(string badgeCount)
        {
			BadgeCount = badgeCount;
        }
    }
}
