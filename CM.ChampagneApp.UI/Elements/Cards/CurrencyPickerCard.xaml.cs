using System;
using System.Collections.Generic;
using CM.ChampagneApp.UI.CustomEventArgs;
using Xamarin.Forms;
using static CM.ChampagneApp.UI.CustomEventArgs.CurrencyLibrary;

namespace CM.ChampagneApp.UI.Elements.Cards
{
    public partial class CurrencyPickerCard : ContentView
    {

        public delegate void CurrencyPicked(object sender, CurrencyPickedEventArgs args);
        public event CurrencyPicked OnCurrencyPicked;


        public Currencies CurrentCurrency { get; set; }
        public List<Currencies> AvailableCurrencies { get; set; }
        public Dictionary<Currencies, string> CurrencyDic { get; set; }



        public CurrencyPickerCard()
        {
            InitializeComponent();
            AvailableCurrencies = new List<Currencies>();
            CurrencyDic = new Dictionary<Currencies, string>();
            AvailableCurrencies.Add(Currencies.Euro);
            CurrencyDic[Currencies.Euro] = "€";
            AvailableCurrencies.Add(Currencies.Dollar);
            CurrencyDic[Currencies.Dollar] = "$";
            AvailableCurrencies.Add(Currencies.Pound);
            CurrencyDic[Currencies.Pound] = "£";
            CurrentCurrency = Currencies.Euro;

        }


        void Handle_Clicked(object sender, System.EventArgs e)
        {
            
            var index = AvailableCurrencies.IndexOf(CurrentCurrency);
            if(index + 1 >= AvailableCurrencies.Count)
            {
                CurrentCurrency = AvailableCurrencies[0];
                Currency.Text = CurrencyDic[CurrentCurrency];
            }
            else
            {
                CurrentCurrency = AvailableCurrencies[index + 1];
                Currency.Text = CurrencyDic[CurrentCurrency];
            }
            var args = new CurrencyPickedEventArgs(CurrentCurrency);
            OnCurrencyPicked(sender, args);

        }
    }
}
