using System;
using System.Collections.Generic;
using CM.ChampagneApp.UI.UIFacade.Models;

namespace CM.ChampagneApp.UI.Pages.UserPages.ProfilePage.Helper
{
    public class IMergedUserModel
    {
        public string ProfileName { get; }
        public string Biography { get; }
        public string ProfileImageUrl { get; }
        public string ProfileCoverImgUrl { get; }

        public int TastedChampagnes { get; }
        public int Followers { get; }
        public int Following { get; }

        public List<AbstractProfileCard> Pages { get; }
    }
}
