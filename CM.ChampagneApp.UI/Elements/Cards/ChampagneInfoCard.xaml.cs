using System;
using System.Collections.Generic;
using System.Windows.Input;
using CM.ChampagneApp.UI.Elements.Assets;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Cards
{
    public partial class ChampagneInfoCard : ContentView
    {

		public delegate void BottleClicked(object sender, System.EventArgs e);
		public event BottleClicked OnBottleClicked;

        public static BindableProperty BottleClickedCommandProperty =
            BindableProperty.Create(nameof(BottleClickedCommand), typeof(ICommand), typeof(ChampagneInfoCard));

        public ChampagneInfoCard()
        {
            InitializeComponent();
        }

		void Handle_Clicked(object sender, System.EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("Should animate bottle");
			if(OnBottleClicked != null)
			{
				OnBottleClicked(sender, e);
			}

            if(BottleClickedCommand != null)
            {
                if(BottleClickedCommand.CanExecute(null))
                {
                    BottleClickedCommand.Execute(null);
                }
            }

            //var image = BottleImage; this.RaiseChild(image);
            //image.TranslateTo(0, (App.DisplaySettings.Height / 2 - image.Height / 2) - 20, 200, null);
            //image.ScaleTo(1.5, 200, null);image.RotateTo(360, 200);
        }

        public ICommand BottleClickedCommand
        {
            get { return (ICommand)GetValue(BottleClickedCommandProperty); }
            set { SetValue(BottleClickedCommandProperty, value); }
        }

        public string BrandName
        {
            get { return (string)GetValue(BrandNameProperty); }
            set { SetValue(BrandNameProperty, value); }
        }

        public static BindableProperty BrandNameProperty =
            BindableProperty.Create(nameof(BrandName), typeof(string), typeof(ChampagneInfoCard));

        public string BottleName
        {
            get { return (string)GetValue(BottleNameProperty); }
            set { SetValue(BottleNameProperty, value); }
        }

        public static BindableProperty BottleNameProperty =
            BindableProperty.Create(nameof(BottleName), typeof(string), typeof(ChampagneInfoCard));

        public string NumberOfTastings
        {
            get { return (string)GetValue(NumberOfTastingsProperty); }
            set { SetValue(NumberOfTastingsProperty, value); }
        }

        public static BindableProperty NumberOfTastingsProperty =
            BindableProperty.Create(nameof(NumberOfTastings), typeof(string), typeof(ChampagneInfoCard));

        public string AverageRating
        {
            get { return (string)GetValue(AverageRatingProperty); }
            set { SetValue(AverageRatingProperty, value); }
        }

        public static BindableProperty AverageRatingProperty =
            BindableProperty.Create(nameof(AverageRating), typeof(string), typeof(ChampagneInfoCard));

        public string ImageURI
        {
            get { return (string)GetValue(ImageURIProperty); }
            set { SetValue(ImageURIProperty, value); }
        }

        public static BindableProperty ImageURIProperty =
            BindableProperty.Create(nameof(ImageURI), typeof(string), typeof(ChampagneInfoCard));

        public string Vintage
        {
            get { return (string)GetValue(VintageProperty); }
            set { SetValue(VintageProperty, value); }
        }

        public static BindableProperty VintageProperty =
            BindableProperty.Create(nameof(Vintage), typeof(string), typeof(ChampagneInfoCard));

        public string Character
        {
            get { return (string)GetValue(CharacterProperty); }
            set { SetValue(CharacterProperty, value); }
        }

        public static BindableProperty CharacterProperty =
            BindableProperty.Create(nameof(Character), typeof(string), typeof(ChampagneInfoCard));

        public string Dosage
        {
            get { return (string)GetValue(DosageProperty); }
            set { SetValue(DosageProperty, value); }
        }

        public static BindableProperty DosageProperty =
            BindableProperty.Create(nameof(Dosage), typeof(string), typeof(ChampagneInfoCard));

        public string Price
        {
            get { return (string)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }

        public static BindableProperty PriceProperty =
            BindableProperty.Create(nameof(Price), typeof(string), typeof(ChampagneInfoCard));


        public string Alchohol
        {
            get { return (string)GetValue(AlchoholProperty); }
            set { SetValue(AlchoholProperty, value); }
        }

        public static BindableProperty AlchoholProperty =
            BindableProperty.Create(nameof(Alchohol), typeof(string), typeof(ChampagneInfoCard));

        public string BottleSize
        {
            get { return (string)GetValue(BottleSizeProperty); }
            set { SetValue(BottleSizeProperty, value); }
        }

        public static BindableProperty BottleSizeProperty =
            BindableProperty.Create(nameof(BottleSize), typeof(string), typeof(ChampagneInfoCard));

    }
}