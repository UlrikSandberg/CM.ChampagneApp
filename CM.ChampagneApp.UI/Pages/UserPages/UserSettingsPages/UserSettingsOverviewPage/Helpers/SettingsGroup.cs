using System;
using System.Collections.ObjectModel;

namespace CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.Helpers
{
    public class SettingsGroup : ObservableCollection<SettingsListModel>
    {
        public string name { get; private set; }
        public bool IsVisible { get; private set; }

        public SettingsGroup(string groupName, bool isVisible)
        {
            name = groupName;
            IsVisible = isVisible;
        }
    }
}
