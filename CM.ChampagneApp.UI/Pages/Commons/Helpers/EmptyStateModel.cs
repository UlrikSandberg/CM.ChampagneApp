using System;
using CM.ChampagneApp.UI.UIFacade.Models.Helpers;

namespace CM.ChampagneApp.UI.Pages.Commons.Helpers
{
    public class EmptyStateModel
    {
        public VisibleProperty<string> TitleProperty { get; set; } = new VisibleProperty<string>();
        public VisibleProperty<string> BodyProperty { get; set; } = new VisibleProperty<string>();
        public VisibleProperty<string> IconProperty { get; set; } = new VisibleProperty<string>();
        public VisibleProperty<string> ButtonTextProperty { get; set; } = new VisibleProperty<string>();
    }
}
