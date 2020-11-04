using System;
namespace CM.ChampagneApp.UI.Pages.ChampagnePages.Helpers
{
    public class ChampagneProfileInitData
    {
        public Guid ChampagneId { get; private set; }

		public ChampagneProfileInitData(Guid champagneId)
        {
            ChampagneId = champagneId;
		}
    }
}
