using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Cards
{
    public partial class BrandCard : ContentView
    {
        public BrandCard()
        {
            InitializeComponent();
        }

        public string BrandName
        {
            get { return (string)GetValue(BrandNameProperty); }
            set { SetValue(BrandNameProperty, value); }
        }

        public static BindableProperty BrandNameProperty =
            BindableProperty.Create(nameof(BrandName), typeof(string), typeof(BrandCard));

        public int UserTastings
        {
            get { return (int)GetValue(UserTastingsProperty); }
			set { SetValue(BrandNameProperty, value); }
        }

        public static BindableProperty UserTastingsProperty =
            BindableProperty.Create(nameof(UserTastings), typeof(int), typeof(BrandCard), 0);
        
        public int NumberOfBottles
        {
            get { return (int)GetValue(NumberOfBottlesProperty); }
			set { SetValue(BrandNameProperty, value); }
        }

        public static BindableProperty NumberOfBottlesProperty =
            BindableProperty.Create(nameof(NumberOfBottles), typeof(int), typeof(BrandCard), 0);
      
    }
}
