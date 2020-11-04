using System;
using System.Linq;
using CM.ChampagneApp.UI.Pages.Commons.Helpers;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser;

namespace CM.ChampagneApp.UI.Pages.UserPages.ProfilePage.Helper
{
    public class UserUpdateMapper : IUpdateMapper<UIUser, UIUser>
    {
        public void UpdateData(UIUser data, UIUser update)
        {
            //Update nav bar
            data.tastedChampagnes = update.tastedChampagnes;
            data.followers = update.followers;
            data.following = update.following;

            //Update info
            data.profileName = update.profileName;
            data.name = update.name;

            //Update images
            data.ImageCustomization.profilePictureImgId = update.ImageCustomization.profilePictureImgId;
            data.ImageCustomization.profileCoverImgId = update.ImageCustomization.profileCoverImgId;

            //Update pages
            data.pages = update.pages;
        }
    }

    public class PublicUserUpdateMapper : IUpdateMapper<UIUser, UIPublicUser>
    {
        public void UpdateData(UIUser data, UIPublicUser update)
        {
            //Update nav bar --> Not champagnes deliberately.
            data.followers = update.Followers;
            data.following = update.Following;

            //Update info
            data.profileName = update.ProfileName;
            data.name = update.Name;

            //Update images
            data.ImageCustomization.profilePictureImgId = update.ImageCustomization.profilePictureImgId;
            data.ImageCustomization.profileCoverImgId = update.ImageCustomization.profileCoverImgId;

            data.IsRequesterFollowing = update.IsRequesterFollowing;
        }
    }
}
