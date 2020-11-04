using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Backgrounds
{
    public partial class GrapeBackground : ContentView
    {
        public GrapeBackground()
        {
            InitializeComponent();

            if(App.IsOnIphoneX)
            {
                Image.Margin = new Thickness(0, 0, -20, -100);
            }
        }
    }
}
