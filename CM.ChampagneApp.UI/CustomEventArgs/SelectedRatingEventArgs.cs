using System;
namespace CM.ChampagneApp.UI.CustomEventArgs
{
    public class SelectedRatingEventArgs
    {

        public double SelectedRating { get; private set; }

        public SelectedRatingEventArgs(double ChosenRating)
        {
            SelectedRating = ChosenRating;
        }
    }
}
