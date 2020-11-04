using System;
namespace CM.ChampagneApp.UI.Helpers
{
    public class NotificationReceivedMessage
    {      
		public string Message { get; }
		public string ContextUrl { get; }
		public bool IsRunning { get; }
		public string NotificationId { get; }

		public NotificationReceivedMessage(string message, string contextUrl, string notificationId, bool isRunning)
        {
			Message = message;
			ContextUrl = contextUrl;
			IsRunning = isRunning;
			NotificationId = notificationId;
		}
    }
}
