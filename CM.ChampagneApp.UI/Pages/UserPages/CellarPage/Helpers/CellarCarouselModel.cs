using System;
using CM.ChampagneApp.UI.Pages.Commons;
using CM.ChampagneApp.UI.Pages.Commons.Helpers;
using CM.ChampagneApp.UI.UIFacade.Models;
using CM.ChampagneApp.UI.UIFacade.Models.Helpers;

namespace CM.ChampagneApp.UI.Pages.UserPages.CellarPage.Helpers
{
    public class CellarCarouselModel<T> : ParallaxModel
    {
        public PagingService<T> PagingService { get; set; }
        public EmptyStateModel EmptyStateModel { get; set; }
        public bool IsFlipEnabled { get; set; }
    }
}
