using System;
namespace CM.ChampagneApp.UI.Pages.UserPages.ProfilePage.Helper
{
    public class ProfilePageInitData
    {
        public Guid Id { get; }
        public bool IsCurrentUser { get; }

        public ProfilePageInitData(Guid id, bool isCurrentUser)
        {
            Id = id;
            IsCurrentUser = isCurrentUser;
        }
    }
}
