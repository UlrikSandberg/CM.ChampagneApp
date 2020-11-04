using System;
using System.Windows.Input;
using FreshMvvm;
using CM.ChampagneApp.UI.UIFacade.Models;

namespace CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.Helpers
{
    public class SettingsListModel : BaseUIModel
    {

        public string Title { get; set; }

        public bool IsUrl { get; set; }

        public string Url { get; set; }

        public ICommand Command { get; set; }

        public bool IsBadgeVisible { get; set; }

        public string BadgeValue { get; set; }
    }
}
