using System;
namespace CM.ChampagneApp.UI.Pages.TastingPages.Helpers
{
    public class CreateTastingInitData
    {
        public bool IsTasting { get; }
        public double ChosenRating { get; }
		public Guid ChampagneId { get; }

		public CreateTastingInitData(bool isTasting, Guid champagneId, double chosenRating)
        {
            ChampagneId = champagneId;
			IsTasting = isTasting;
            ChosenRating = chosenRating;
        }
    }
}
