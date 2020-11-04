using System;
using System.Collections.Generic;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.UI.UIFacade.Models;
using CM.ChampagneApp.UI.UIFacade.Models.UIChampagneModels;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Cards
{
    public partial class ChampagneEditionCard : ContentView
    {
		public delegate void ChosenChampagne(object sender, ChampagneEditionCardClickedEventArgs args);
        public event ChosenChampagne OnChosenChampagne;

        public ChampagneEditionCard()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
			var champagne = (UIChampagneLight)this.BindingContext;
			ChampagneEditionCardClickedEventArgs args = new ChampagneEditionCardClickedEventArgs(champagne.Id);
            System.Diagnostics.Debug.WriteLine("Clicked");
            if (OnChosenChampagne != null)
                OnChosenChampagne(this, args);
        }
    }
}
