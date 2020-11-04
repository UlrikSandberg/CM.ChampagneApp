using System;
namespace CM.ChampagneApp.UI.Pages.Commons
{
    public interface IPagingService
    {
        bool IsLoadingEntities { get; set; }
        bool AllEntitiesHasBeenDownloaded { get; set; }
    }
}
