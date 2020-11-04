using System;
namespace CM.ChampagneApp.UI.CustomEventArgs
{
    public class ChampagneEditionCardClickedEventArgs
    {
		public Guid ChampagneId { get; private set; }

		public ChampagneEditionCardClickedEventArgs(Guid champagneId)
        {
            ChampagneId = champagneId;
		}
    }
}
