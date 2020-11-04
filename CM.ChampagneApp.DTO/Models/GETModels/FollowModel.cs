using System;
namespace CM.ChampagneApp.DTO.Models
{
    public class FollowerModel
    {

        public string Id
        {
            get;
            set;
        }

        public string ProfileName
        {
            get;
            set;
        }

        public string ProfileImgId
        {
            get;
            set;
        }

        public bool UserFollows
        {
            get;
            set;
        }
    }
}
