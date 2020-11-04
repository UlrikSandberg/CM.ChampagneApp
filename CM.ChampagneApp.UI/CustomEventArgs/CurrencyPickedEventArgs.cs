using System;
using CM.ChampagneApp.UI.CustomEventArgs;
using static CM.ChampagneApp.UI.CustomEventArgs.CurrencyLibrary;

namespace CM.ChampagneApp.UI.CustomEventArgs
{
    public class CurrencyPickedEventArgs
    {

        public Currencies ChosenCurrency { get; private set; }

        public CurrencyPickedEventArgs(Currencies currency)
        {
            ChosenCurrency = currency;
        }
    }
}
