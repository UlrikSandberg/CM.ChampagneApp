using System;
using AutoMapper;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser;

namespace CM.ChampagneApp.UI.UIFacade.Mapper.CustomMapperConfigurations.UserMapperConfiguration
{
    public static class MapToUIUser
    {
        public static UIUser MapFromUIPublicUser(UIPublicUser source)
        {
            var user = new UIUser();
            user.id = source.Id;
            user.name = source.Name;
            user.profileName = source.ProfileName;
            user.biography = source.Biography;
            user.bookmarkedChampagnes = source.BookmarkhedChampagnes;
            user.tastedChampagnes = source.TastedChampagnes;
            user.following = source.Following;
            user.followers = source.Followers;

            user.pages = source.Pages;

            user.ImageCustomization = source.ImageCustomization;

            user.IsRequesterFollowing = source.IsRequesterFollowing;

            return user;
        }
    }
}
