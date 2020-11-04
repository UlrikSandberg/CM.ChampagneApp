using System;
using System.Collections;
using System.Collections.Specialized;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Lists.InfiniteListView
{
    public class InfinityListView : ListView
    {
        public InfinityListView() : base(ListViewCachingStrategy.RecycleElement)
        {
            ItemTemplate = new InfinityListViewTemplateSelector(GetItemTemplate);
        }

        public DataTemplate ListItemTemplate { get; set; }

        private DataTemplate GetItemTemplate(object item, BindableObject bindableObject)
        {
            return ListItemTemplate;
        }
    }
}
