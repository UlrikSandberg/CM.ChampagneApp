using System;
using System.Collections.Generic;

namespace CM.ChampagneApp.DTO.Models.GETModels.Notifications
{
    public class Notification
    {         
		public Guid Id { get; set; }

        public Guid InvokerId { get; set; }
        public string InvokerName { get; set; }
        public Guid InvokerProfileImgId { get; set; }

        //The date of invocation and expiryDate
        public DateTime Date { get; set; }
        public DateTime ExpiresOn { get; set; }

        //What internal method caused the notification
        public string InvokedByMethod { get; set; }
        public string InvokedByAction { get; set; }
        //Base-notification, deceding the nature of the notification
        public string Type { get; set; }

        //Specify specific users who should receive this
        public string ContextUrl { get; set; }

        public bool IsRead { get; set; }

        //Notification content
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
