using System;
namespace CM.ChampagneApp.UI.Pages.Commons.Helpers
{
    public class CarouselListModel<T>
    {
        public PagingService<T> PagingService { get; set; }
        public EmptyStateModel EmptyStateModel { get; set; }
    }
}
