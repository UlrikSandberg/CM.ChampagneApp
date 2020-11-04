using System;
namespace CM.ChampagneApp.DTO.Models.GETModels.UserModels
{
    public class UserSettings
    {
		public Guid Id { get; set; }
        public string Email { get; set; }
		public bool IsEmailVerified { get; set; }
        public string Name { get; set; }
        public string ProfileName { get; set; }
        public string Biography { get; set; }

		public ImageCustomization imageCustomization { get; set; }

		public NotificationSettings notificationSettings { get; set; }

        public class ImageCustomization
		{
			public Guid ProfilePictureImgId { get; set; }
            public Guid ProfileCoverImgId { get; set; }
            public Guid CellarCardImgId { get; set; }
            public Guid CellarHeaderImgId { get; set; }
		}

        public class NotificationSettings
		{
			public bool ReceiveCMNotifications { get; set; }
            public bool NotifyNewFollower { get; set; }
            public bool NotifyNewComment { get; set; }
            public bool NotifyActivityInThread { get; set; }
            public bool NotifyLikeTasting { get; set; }
            public bool NotifyLikeComment { get; set; }
            public bool ReceiveNewsLetter { get; set; }
			public bool NotifyChampagneTasted { get; set; }
            public bool NotifyBrandNews { get; set; }
		}
    }
}
