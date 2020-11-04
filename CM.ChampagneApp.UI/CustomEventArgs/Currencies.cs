using System;
using System.Collections.Generic;

namespace CM.ChampagneApp.UI.CustomEventArgs
{
    public class CurrencyLibrary
    {

        public Dictionary<Currencies, string> CurrencyDic { get; set; }

        public enum Currencies
        {
            Euro,
            Dollar,
            Pound
        }

        public CurrencyLibrary()
        {

            CurrencyDic = new Dictionary<Currencies, string>();
            CurrencyDic[Currencies.Euro] = "€";
            CurrencyDic[Currencies.Dollar] = "$";
            CurrencyDic[Currencies.Pound] = "£";

        }

    }
}
