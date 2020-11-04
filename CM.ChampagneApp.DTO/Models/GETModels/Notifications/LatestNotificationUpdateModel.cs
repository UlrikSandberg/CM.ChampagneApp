using System;
using System.Collections.Generic;

namespace CM.ChampagneApp.DTO.Models.GETModels.Notifications
{
    public class LatestNotificationUpdateModel
    {      
        public int NumberOfUnreadNotifications { get; set; }
		public IEnumerable<Notification> NewNotifications { get; set; }
    }
}
