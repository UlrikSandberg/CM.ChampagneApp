using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Cards
{
    public partial class GiveRatingHeaderCard : ContentView
    {
        public GiveRatingHeaderCard()
        {
            InitializeComponent();
        }

        public string ChampagneImage
		{
			get { return (string)GetValue(ChampagneImageProperty); }
			set { SetValue(ChampagneImageProperty, value); }
		}

		public static BindableProperty ChampagneImageProperty =
			BindableProperty.Create(nameof(ChampagneImage), typeof(string), typeof(GiveRatingHeaderCard));


        public string BottleName
		{
			get { return (string)GetValue(BottleNameProperty); }
			set { SetValue(BottleNameProperty, value); }
		}

		public static BindableProperty BottleNameProperty =
			BindableProperty.Create(nameof(BottleName), typeof(string), typeof(GiveRatingHeaderCard));

        
        public string BrandName
		{
			get { return (string)GetValue(BrandNameProperty); }
			set { SetValue(BrandNameProperty, value); }
		}

		public static BindableProperty BrandNameProperty =
			BindableProperty.Create(nameof(BrandName), typeof(string), typeof(GiveRatingHeaderCard));

    }
}
