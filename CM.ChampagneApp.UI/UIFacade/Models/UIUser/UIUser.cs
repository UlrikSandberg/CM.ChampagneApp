using System;
using System.Collections.Generic;
using CM.ChampagneApp.Business.Configuration;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser.Entities;
using System.ComponentModel;

namespace CM.ChampagneApp.UI.UIFacade.Models.UIUser
{
    public class UIUser : BaseUIModel
    {
        public Guid id { get; set; }

        public string email { get; set; }
        public string name { get; set; }
        public string profileName { get; set; }
        public string biography { get; set; }
        public int bookmarkedChampagnes { get; set; }
        public int tastedChampagnes { get; set; }
        public int following { get; set; }
        public int followers { get; set; }

        public bool IsRequesterFollowing { get; set; }

        public List<AbstractProfileCard> pages { get; set; } = new List<AbstractProfileCard>();

        public ImageCustomization ImageCustomization { get; set; }
        public Settings settings { get; set; }

        public class Settings
        {
            public string countryCode { get; set; }
            public string language { get; set; }

            public bool isEmailVerified { get; set; }
        }

        public bool SettingsIcon
        {
            get
            {
                if(settings != null)
                {
                    if (settings.isEmailVerified)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }

                }
                return true;
            }
        }

        public string ProfileName
        {
            get
            {
                return profileName == null || profileName.Length == 0 ? name : profileName;
            }
        }

        public string ProfileCoverImgUrl
        {
            get
            {
                if(ImageCustomization != null)
                {
                    return ImageCustomization.profileCoverImgId != Guid.Empty
                    ? new Uri(Configuration.BlobUserStorageUrl, $"{id}/images/{ImageCustomization.profileCoverImgId.ToString()}/small.jpg").ToString()
                    : "ProfileCoverDefault.jpg";
                }
                return "ProfileCoverDefault.jpg";
            }
        }

        public string ProfileImageUrl
        {
            get
            {
                if(ImageCustomization != null)
                {
                    return ImageCustomization.profilePictureImgId != Guid.Empty
                    ? new Uri(Configuration.BlobUserStorageUrl, $"{id}/images/{ImageCustomization.profilePictureImgId.ToString()}/small.jpg").ToString()
                    : "PlaceholderProfileImg.png";
                }
                return "PlaceholderProfileImg.png";
            }
		}
    }
}
