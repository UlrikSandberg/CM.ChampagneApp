using System;
using System.Xml.Schema;
namespace CM.ChampagneApp.DTO.Builders
{
    public class UserNotificationsSettingsUpdate
    {
        
		public bool ReceiveCMNotifications { get; }
        public bool NotifyNewFollower { get; }
        public bool NotifyNewComment { get; }
        public bool NotifyActivityInThread { get; }
        public bool NotifyLikeTasting { get; }
        public bool NotifyLikeComment { get; }
        public bool ReceiveNewsLetter { get; }
		public bool NotifyChampagneTasted { get; }
        public bool NotifyBrandNews { get; }      

		private UserNotificationsSettingsUpdate(bool receiveCMNotifications, bool notifyNewFollower, bool notifyNewComment, bool notifyActivityInThread, bool notifyLikeTasting, bool notifyLikeComment, bool receiveNewsLetter, bool notifyChampagneTasted ,bool notifyBrandNews)
		{
			ReceiveCMNotifications = receiveCMNotifications;
			NotifyNewFollower = notifyNewFollower;
			NotifyNewComment = notifyNewComment;
			NotifyActivityInThread = notifyActivityInThread;
			NotifyLikeTasting = notifyLikeTasting;
			NotifyLikeComment = notifyLikeComment;
			ReceiveNewsLetter = receiveNewsLetter;
			NotifyChampagneTasted = notifyChampagneTasted;
			NotifyBrandNews = notifyBrandNews;
		}

		public class UserNotificationsSettingsUpdateBuilder : IBuilder<UserNotificationsSettingsUpdate>
		{
			private bool ReceiveCMNotifications = false;
			private bool NotifyNewFollower = false;
			private bool NotifyNewComment = false;
			private bool NotifyActivityInThread = false;
			private bool NotifyLikeTasting = false;
			private bool NotifyLikeComment = false;
			private bool ReceiveNewsLetter = false;
			private bool NotifyChampagneTasted = false;
			private bool NotifyBrandNews = false;

			public UserNotificationsSettingsUpdateBuilder SetReceiveCMNotifications(bool value)
			{
				ReceiveCMNotifications = value;
				return this;
			}

			public UserNotificationsSettingsUpdateBuilder SetNotifyNewFollower(bool value)
			{
				NotifyNewFollower = value;
				return this;
			}

			public UserNotificationsSettingsUpdateBuilder SetNotifyNewComment(bool value)
            {
				NotifyNewComment = value;
				return this;
            }
            
			public UserNotificationsSettingsUpdateBuilder SetNotifyActivityInThread(bool value)
            {
				NotifyActivityInThread = value;
				return this;
            }

			public UserNotificationsSettingsUpdateBuilder SetNotifyLikeTasting(bool value)
            {
				NotifyLikeTasting = value;
				return this;
            }

			public UserNotificationsSettingsUpdateBuilder SetNotifyLikeComment(bool value)
            {
				NotifyLikeComment = value;
				return this;
            }

			public UserNotificationsSettingsUpdateBuilder SetReceiveNewLetter(bool value)
            {
				ReceiveNewsLetter = value;
				return this;
            }  

			public UserNotificationsSettingsUpdateBuilder SetNotifyChampagneTasted(bool value)
			{
				NotifyChampagneTasted = value;
				return this;
			}

			public UserNotificationsSettingsUpdateBuilder SetNotifyBrandNews(bool value)
			{
				NotifyBrandNews = value;
				return this;
			}

			public UserNotificationsSettingsUpdate Build()
			{
				return new UserNotificationsSettingsUpdate(ReceiveCMNotifications, NotifyNewFollower, NotifyNewComment, NotifyActivityInThread, NotifyLikeTasting, NotifyLikeComment, ReceiveNewsLetter, NotifyChampagneTasted, NotifyBrandNews);
			}
		}
    }
}
