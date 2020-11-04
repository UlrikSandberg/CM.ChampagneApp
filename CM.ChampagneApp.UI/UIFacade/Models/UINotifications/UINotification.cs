using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.UIFacade.Models.UINotifications
{
	public class UINotification : BaseUIModel
	{      
		public Guid Id { get; set; }

        public Guid InvokerId { get; set; }
        public string InvokerName { get; set; }
        public Guid InvokerProfileImgId { get; set; }

        //The date of invocation and expiryDate
        public DateTime Date { get; set; }
        public DateTime ExpiresOn { get; set; }

        //What internal method caused the notification
        public NotificationMethod InvokedByMethod { get; set; }
        public NotificationAction InvokedByAction { get; set; }
        //Base-notification, deceding the nature of the notification
        public string Type { get; set; }

        //Specify specific users who should receive this
        public string ContextUrl { get; set; }

        public bool IsRead { get; set; }

        //Notification content
        public string Title { get; set; }
        public string Message { get; set; }

        public string DateInFormat
		{
            get
			{
				var now = DateTime.Now;
				var utc = DateTime.UtcNow;

				var timeZoneBuffer = utc.Ticks - now.Ticks;

				var timeSince = now.Ticks - Date.Ticks;

				var timeSinceRespectiveToZone = timeSince + timeZoneBuffer;

				var timeSpan = new TimeSpan(timeSinceRespectiveToZone);
            
				var timeSpanInTotalMinutes = timeSpan.TotalMinutes;

				if(timeSpanInTotalMinutes < 1)
				{
					return "Less than a minute ago...";
				}

				if(timeSpanInTotalMinutes < 60)
				{
					return (int)timeSpanInTotalMinutes + " minutes ago.";
				}

				if(timeSpanInTotalMinutes < 60 * 24)
				{
					return ((int)timeSpanInTotalMinutes / 60) + " hours ago.";
				}

				if(timeSpanInTotalMinutes < 60 * 24 * 7)
				{
					return ((int)timeSpanInTotalMinutes / (60 * 24)) + " days ago.";
				}

				return ((int)timeSpanInTotalMinutes / (60 * 24 * 7)) + " weeks ago.";
                
			}
            set
			{
				DateInFormat = value;
			}
		}

        public void UpdateTimeFormat()
		{
			var now = DateTime.Now;
            var utc = DateTime.UtcNow;

            var timeZoneBuffer = utc.Ticks - now.Ticks;

            var timeSince = now.Ticks - Date.Ticks;

            var timeSinceRespectiveToZone = timeSince + timeZoneBuffer;

            var timeSpan = new TimeSpan(timeSinceRespectiveToZone);

            var timeSpanInTotalMinutes = timeSpan.TotalMinutes;

            if (timeSpanInTotalMinutes < 1)
            {
				DateInFormat = "Less than a minute ago...";
            }

            if (timeSpanInTotalMinutes < 60)
            {
				DateInFormat = (int)timeSpanInTotalMinutes + " minutes ago.";
            }

            if (timeSpanInTotalMinutes < 60 * 24)
            {
				DateInFormat = ((int)timeSpanInTotalMinutes / 60) + " hours ago.";
            }

            if (timeSpanInTotalMinutes < 60 * 24 * 7)
            {
				DateInFormat = ((int)timeSpanInTotalMinutes / (60 * 24)) + " days ago.";
            }

			DateInFormat = ((int)timeSpanInTotalMinutes / (60 * 24 * 7)) + " weeks ago.";
		}

		public string ImageUrl
		{
			get
			{
				if(InvokedByAction.Equals(NotificationAction.CommunityUpdate))
				{
					return "SquareLogo.png";
				}
            
				if (InvokerProfileImgId.Equals(Guid.Empty))
				{
					return "PlaceholderProfileImg.png";
				}
				else
				{
					if(InvokedByAction.Equals(NotificationAction.UserAction))
					{
                        return new Uri(Configuration.BlobUserStorageUrl, $"{InvokerId}/images/{InvokerProfileImgId}/small.jpg").ToString();
					}
					if(InvokedByAction.Equals(NotificationAction.BrandNews))
					{
                        return new Uri(Configuration.BlobBrandStorageUrl, $"{InvokerId}/images/{InvokerProfileImgId}/small.jpg").ToString();
					}               
					return null;
				}
			}
		}
	}
}

