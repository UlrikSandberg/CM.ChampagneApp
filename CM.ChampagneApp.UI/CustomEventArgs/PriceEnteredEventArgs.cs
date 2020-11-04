using System;
namespace CM.ChampagneApp.UI.CustomEventArgs
{
    public class PriceEnteredEventArgs
    {
        public double EnteredPrice { get; private set; }

        public PriceEnteredEventArgs(double price)
        {
            EnteredPrice = price;
        }
    }
}
