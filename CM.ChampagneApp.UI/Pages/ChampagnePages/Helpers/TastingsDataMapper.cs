using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CM.ChampagneApp.UI.Pages.Commons.Helpers;
using CM.ChampagneApp.UI.UIFacade.Models.UITastingModels;

namespace CM.ChampagneApp.UI.Pages.ChampagnePages.Helpers
{
    public class TastingsDataMapper : IDataMapper<ObservableCollection<UITasting>, IEnumerable<UITasting>>
    {
        public ObservableCollection<UITasting> Map(IEnumerable<UITasting> data)
        {
            return new ObservableCollection<UITasting>(data);
        }
    }
}
