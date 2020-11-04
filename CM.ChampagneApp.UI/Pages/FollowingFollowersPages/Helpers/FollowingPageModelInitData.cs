using System;
namespace CM.ChampagneApp.UI.Pages.FollowingFollowersPages.Helpers
{
    public class FollowingPageModelInitData
    {
        public Guid UserId { get; }
        public string Username { get; }
        public bool IsCurrentUser { get; }

        public FollowingPageModelInitData(Guid userId, string username = null, bool isCurrentUser = false)
        {
            UserId = userId;
            Username = username;
            IsCurrentUser = isCurrentUser;
        }
    }
}
