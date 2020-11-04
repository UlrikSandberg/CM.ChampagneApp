using System;
namespace CM.ChampagneApp.UI.Pages.TastingPages.Helpers
{
    public class ChampagneTastingsInitData
    {
        public Guid ChampagneId { get; }

		public ChampagneTastingsInitData(Guid champagneId)
        {
            ChampagneId = champagneId;
		}
    }
}
