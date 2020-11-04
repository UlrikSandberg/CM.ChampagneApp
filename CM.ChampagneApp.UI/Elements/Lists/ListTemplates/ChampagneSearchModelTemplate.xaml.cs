using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Lists.ListTemplates
{
    public partial class ChampagneSearchModelTemplate : ContentView
    {
        public static BindableProperty BottleNameProperty =
            BindableProperty.Create(nameof(BottleName), typeof(string), typeof(ChampagneSearchModelTemplate));

        public static BindableProperty BrandNameProperty =
            BindableProperty.Create(nameof(BrandName), typeof(string), typeof(ChampagneSearchModelTemplate));

        public static BindableProperty VintageProperty =
            BindableProperty.Create(nameof(Vintage), typeof(string), typeof(ChampagneSearchModelTemplate));

        public static BindableProperty BottleImageUriProperty =
            BindableProperty.Create(nameof(BottleImageUri), typeof(string), typeof(ChampagneSearchModelTemplate));

        public static BindableProperty AverageRatingProperty =
            BindableProperty.Create(nameof(AverageRating), typeof(double), typeof(ChampagneSearchModelTemplate));

        public static BindableProperty TastingsProperty =
            BindableProperty.Create(nameof(Tastings), typeof(int), typeof(ChampagneSearchModelTemplate));

        public static BindableProperty AverageRatingStringProperty =
            BindableProperty.Create(nameof(AverageRatingString), typeof(string), typeof(ChampagneSearchModelTemplate));

        public ChampagneSearchModelTemplate()
        {
            InitializeComponent();
        }

        public string BottleName
        {
            get { return (string)GetValue(BottleNameProperty); }
            set { SetValue(BottleNameProperty, value); }
        }

        public string BrandName
        {
            get { return (string)GetValue(BrandNameProperty); }
            set { SetValue(BrandNameProperty, value); }
        }

        public string Vintage
        {
            get { return (string)GetValue(VintageProperty); }
            set { SetValue(VintageProperty, value); }
        }

        public string BottleImageUri
        {
            get { return (string)GetValue(BottleImageUriProperty); }
            set { SetValue(BottleImageUriProperty, value); }
        }

        public string AverageRatingString
        {
            get { return (string)GetValue(AverageRatingStringProperty); }
            set { SetValue(AverageRatingStringProperty, value); }
        }

        public double AverageRating
        {
            get { return (double)GetValue(AverageRatingProperty); }
            set { SetValue(AverageRatingProperty, value); }
        }

        public int Tastings
        {
            get { return (int)GetValue(TastingsProperty); }
            set { SetValue(TastingsProperty, value); }
        }
    }
}
