using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Cards
{
    public partial class Top10Card : ContentView
    {
        public static BindableProperty ClickedCommandProperty =
            BindableProperty.Create(nameof(ClickedCommand), typeof(ICommand), typeof(Top10Card));

        public static BindableProperty BrandNameProperty =
             BindableProperty.Create(nameof(BrandName), typeof(string), typeof(Top10Card));

        public static BindableProperty BottleNameProperty =
            BindableProperty.Create(nameof(BottleName), typeof(string), typeof(Top10Card));

        public static BindableProperty AverageRatingProperty =
            BindableProperty.Create(nameof(AverageRating), typeof(string), typeof(Top10Card));

        public static BindableProperty NumberOfTastingsProperty =
            BindableProperty.Create(nameof(NumberOfTastings), typeof(int), typeof(Top10Card));

        public static BindableProperty IsVintageProperty =
            BindableProperty.Create(nameof(IsVintage), typeof(bool), typeof(Top10Card));

        public static BindableProperty VintageYearProperty =
            BindableProperty.Create(nameof(VintageYear), typeof(int), typeof(Top10Card));

        public static BindableProperty ImageUrlProperty =
            BindableProperty.Create(nameof(ImageUrl), typeof(string), typeof(Top10Card));

        public static BindableProperty Top10NumberProperty =
            BindableProperty.Create(nameof(Top10Number), typeof(int), typeof(Top10Card));

        public static BindableProperty IsTop10NumberVisibleProperty =
            BindableProperty.Create(nameof(IsTop10NumberVisible), typeof(bool), typeof(Top10Card));

        public Top10Card()
        {
            InitializeComponent();
        }

        public ICommand ClickedCommand
        {
            get { return (ICommand)GetValue(ClickedCommandProperty); }
            set { SetValue(ClickedCommandProperty, value); }
        }

        public string BrandName
        {
            get { return (string)GetValue(BrandNameProperty); }
            set { SetValue(BrandNameProperty, value); }
        }

        public string BottleName
        {
            get { return (string)GetValue(BottleNameProperty); }
            set { SetValue(BottleNameProperty, value); }
        }

        public string AverageRating
        {
            get { return (string)GetValue(AverageRatingProperty); }
            set { SetValue(AverageRatingProperty, value); }
        }

        public int NumberOfTastings
        {
            get { return (int)GetValue(NumberOfTastingsProperty); }
            set { SetValue(NumberOfTastingsProperty, value); }
        }

        public bool IsVintage
        {
            get { return (bool)GetValue(IsVintageProperty); }
            set { SetValue(IsVintageProperty, value); }
        }

        public int VintageYear
        {
            get { return (int)GetValue(VintageYearProperty); }
            set { SetValue(VintageYearProperty, value); }
        }

        public string ImageUrl
        {
            get { return (string)GetValue(ImageUrlProperty); }
            set { SetValue(ImageUrlProperty, value); }
        }

        public int Top10Number
        {
            get { return (int)GetValue(Top10NumberProperty); }
            set { SetValue(Top10NumberProperty, value); }
        }

        public bool IsTop10NumberVisible
        {
            get { return (bool)GetValue(IsTop10NumberVisibleProperty); }
            set { SetValue(IsTop10NumberVisibleProperty, value); }
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            var bindingModel = this.BindingContext;

            if(ClickedCommand != null)
            {
                if(ClickedCommand.CanExecute(bindingModel))
                {
                    ClickedCommand.Execute(bindingModel);
                }
            }
        }
    }
}
