using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Assets
{
    public partial class ChampagneRating : ContentView
    {
        public ChampagneRating()
        {
            InitializeComponent();
        }

        public string AverageRating
        {
            get { return (string)GetValue(AverageRatingProperty); }
            set { SetValue(AverageRatingProperty, value); }
        }


        public static BindableProperty AverageRatingProperty =
            BindableProperty.Create(nameof(AverageRating), typeof(string), typeof(ChampagneRating));

    }
}
