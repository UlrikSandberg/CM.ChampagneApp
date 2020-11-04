using System;
using CM.ChampagneApp.UI.Pages.Commons.Helpers;
using CM.ChampagneApp.UI.UIFacade.Mapper.CustomMapperConfigurations.UserMapperConfiguration;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser;

namespace CM.ChampagneApp.UI.Pages.UserPages.ProfilePage.Helper
{
    public class PublicUserToUIUserDataMapper : IDataMapper<UIUser, UIPublicUser>
    {
        public UIUser Map(UIPublicUser data)
        {
            //Map
            return MapToUIUser.MapFromUIPublicUser(data);
        }
    }
}
