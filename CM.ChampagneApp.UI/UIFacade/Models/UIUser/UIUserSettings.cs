using System;
//using CM.ChampagneApp.Business.Models.UserModels;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser.Entities;
using CM.ChampagneApp.Business.Configuration;
namespace CM.ChampagneApp.UI.UIFacade.Models.UIUser
{
	public class UIUserSettings : BaseUIModel
    {      
		public Guid Id { get; set; }

        public string Email { get; set; }
		public bool IsEmailVerified { get; set; }
        public string Name { get; set; }
        public string ProfileName { get; set; }
        public string Biography { get; set; }

		public Entities.ImageCustomization ImageSettings { get; set; }

		public Notifications NotificationSettings { get; set; }

        public string ProfileCellarCardImageUrl
		{
            get
			{
                return ImageSettings == null
                    ? "CellarCardDefault.jpg"
                    : ImageSettings.cellarCardImgId.Equals(Guid.Empty)
                        ? "CellarCardDefault.jpg"
                        : new Uri(Configuration.BlobUserStorageUrl, $"{Id}/images/{ImageSettings.cellarCardImgId}/small.jpg").ToString();
            }
		}

        public string ProfileImageUrl
		{
			get
			{
                return ImageSettings == null
                    ? "PlaceholderProfileImg.png"
                    : ImageSettings.profilePictureImgId.Equals(Guid.Empty)
                        ? "PlaceholderProfileImg.png"
                        : new Uri(Configuration.BlobUserStorageUrl, $"{Id}/images/{ImageSettings.profilePictureImgId}/small.jpg").ToString();
            }
		}

        public string ProfileCoverImageUrl
		{
            get
			{
                return ImageSettings == null
                    ? "ProfileCoverDefault.jpg"
                    : ImageSettings.profileCoverImgId.Equals(Guid.Empty)
                        ? "ProfileCoverDefault.jpg"
                        : new Uri(Configuration.BlobUserStorageUrl, $"{Id}/images/{ImageSettings.profileCoverImgId}/small.jpg").ToString();
            }
		}
    }
}
