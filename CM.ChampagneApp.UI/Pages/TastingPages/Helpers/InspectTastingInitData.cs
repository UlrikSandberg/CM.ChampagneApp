using System;
namespace CM.ChampagneApp.UI.Pages.TastingPages.Helpers
{
    public class InspectTastingInitData
    {
        public Guid TastingId { get; private set; }
		public Guid ChampagneId { get; private set; }

		public InspectTastingInitData(Guid tastingId, Guid champagneId)
        {
            ChampagneId = champagneId;
			TastingId = tastingId;
		}
    }
}
