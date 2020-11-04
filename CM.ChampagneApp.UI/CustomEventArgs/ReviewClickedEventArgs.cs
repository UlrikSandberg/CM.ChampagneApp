using System;
using CM.ChampagneApp.UI.UIFacade.Models.UITastingModels;
namespace CM.ChampagneApp.UI.CustomEventArgs
{
    public class ReviewClickedEventArgs
    {

		public UITasting TastingId { get; private set; }

		public ReviewClickedEventArgs(UITasting tastingId)
        {
			TastingId = tastingId;
        }
    }
}
