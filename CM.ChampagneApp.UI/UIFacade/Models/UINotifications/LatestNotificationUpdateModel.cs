using System;
using System.Collections.Generic;
namespace CM.ChampagneApp.UI.UIFacade.Models.UINotifications
{
    public class UILatestNotificationUpdateModel
    {
        public int NumberOfUnreadNotifications { get; set; }
		public IEnumerable<UINotification> NewNotifications { get; set; }
    }
}
