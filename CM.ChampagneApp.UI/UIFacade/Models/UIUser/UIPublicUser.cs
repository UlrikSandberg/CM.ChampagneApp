using System;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser.Entities;
using CM.ChampagneApp.Business.Configuration;
using System.Dynamic;
using System.Collections.Generic;
using System.ComponentModel;

namespace CM.ChampagneApp.UI.UIFacade.Models.UIUser
{
	public class UIPublicUser : BaseUIModel
    {    
		public Guid Id { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
		public string ProfileName { get; set; }
		public string Biography { get; set; }

		public int BookmarkhedChampagnes { get; set; }

		public int TastedChampagnes { get; set; }

		public int Following { get; set; }

		public int Followers { get; set; }

		public ImageCustomization ImageCustomization { get; set; }

		public List<AbstractProfileCard> Pages { get; set; }
      
		public bool IsRequesterFollowing { get; set; }

        public string ProfileCoverImgUrl
		{
            get
			{
                return ImageCustomization.profileCoverImgId != Guid.Empty
                    ? new Uri(Configuration.BlobUserStorageUrl, $"{Id}/images/{ImageCustomization.profileCoverImgId.ToString()}/original.jpg").ToString()
                    : "ProfileCoverDefault.jpg";
            }
		}

        public string Username
		{
            get
			{
                return ProfileName != null ? ProfileName : Name;
            }
		}

        public string ProfileImgUrl
		{
			get
			{
                return ImageCustomization.profilePictureImgId != Guid.Empty
                    ? new Uri(Configuration.BlobUserStorageUrl, $"{Id}/images/{ImageCustomization.profilePictureImgId.ToString()}/original.jpg").ToString()
                    : "PlaceholderProfileImg.png";
            }
		}      
	}
}
