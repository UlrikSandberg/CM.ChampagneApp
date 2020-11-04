using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CM.ChampagneApp.UI.Pages.Commons.Helpers;
using CM.ChampagneApp.UI.UIFacade.Models.UITop10;

namespace CM.ChampagneApp.UI.Pages.Top10Pages.Top10ListPage.Helpers
{
    public class StandardTop10ListsDataMapper : IDataMapper<ObservableCollection<UITop10ListCardModel>, IEnumerable<UITop10ListCardModel>>
    {
        public ObservableCollection<UITop10ListCardModel> Map(IEnumerable<UITop10ListCardModel> data)
        {
            return new ObservableCollection<UITop10ListCardModel>(data);
        }
    }
}
