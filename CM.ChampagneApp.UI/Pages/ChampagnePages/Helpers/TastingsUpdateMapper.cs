using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CM.ChampagneApp.UI.Pages.Commons.Helpers;
using CM.ChampagneApp.UI.UIFacade.Models.UITastingModels;

namespace CM.ChampagneApp.UI.Pages.ChampagnePages.Helpers
{
    public class TastingsUpdateMapper : IUpdateMapper<ObservableCollection<UITasting>, IEnumerable<UITasting>>
    {
        public void UpdateData(ObservableCollection<UITasting> data, IEnumerable<UITasting> update)
        {
            var list = new List<UITasting>(update);

            for(int i = 0; i < list.Count; i++)
            {
                data[i] = list[i];
            }
        }
    }
}
