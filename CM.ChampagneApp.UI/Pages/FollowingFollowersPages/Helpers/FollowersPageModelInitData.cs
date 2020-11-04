using System;
namespace CM.ChampagneApp.UI.Pages.FollowingFollowersPages.Helpers
{
    public class FollowersPageModelInitData
    {
        public Guid FollowersForId { get; }
        public FollowersType FollowersType { get; }
        public bool IsCurrentUser { get; }
        public string Username { get; }

        public FollowersPageModelInitData(Guid followersForId, FollowersType followersType, bool isCurrentUser = false, string username = null)
        {
            IsCurrentUser = isCurrentUser;
            Username = username;
            FollowersType = followersType;
            FollowersForId = followersForId;

            if(Username == null)
            {
                Username = "";
            }
        }
    }
}
