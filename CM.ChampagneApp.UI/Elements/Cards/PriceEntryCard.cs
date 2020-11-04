using System;
using System.Collections.Generic;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.UI.Elements.Typography;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Cards
{
    public partial class PriceEntryCard : ContentView
    {

        public delegate void PriceEntered(object sender, PriceEnteredEventArgs args);
        public event PriceEntered OnPriceEntered;

        public PriceEntryCard()
        {
            InitializeComponent();
        }

        void Handle_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            
        }

        void Handle_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            var trimmedText = PriceEntry.Text.Trim();
            if(trimmedText != "")
            {
                var args = new PriceEnteredEventArgs(Double.Parse(trimmedText));
                if(OnPriceEntered != null)
                {
                    OnPriceEntered(sender, args);
                }
            }
        }
    }
}
