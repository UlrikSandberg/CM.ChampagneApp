using System;
using System.Collections;
using System.ComponentModel;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.UI.Pages.Commons;

namespace CM.ChampagneApp.UI.Elements.Lists.InfiniteListView
{
    public class GlobalSearchCarouselModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GlobalSearchCarouselTemplate DataTemplate { get; set; }
        public IPagingService PagingService { get; set; }
        public string EmptyStateText { get; set; }

        public object TopVisibleItem { get; set; }
        public bool ShouldScrollToEntry { get; set; } = true;
    }

    public enum GlobalSearchCarouselTemplate
    {
        ChampagneListTemplate,
        BrandListTemplate,
        UserListTemplate
    }
}
