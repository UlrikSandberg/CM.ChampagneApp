using System;
namespace CM.ChampagneApp.UI.Pages.UserPages.CellarPage.Helpers
{
    public class CellarPageModelInitData
    {
        public Guid UserId { get; }
        public string Username { get; }
        public bool IsCurrentUser { get; }

        public CellarPageModelInitData(Guid userId, string username = null, bool isCurrentUser = false)
        {
            UserId = userId;
            Username = username;
            IsCurrentUser = isCurrentUser;
        }
    }
}
