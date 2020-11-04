using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Assets
{
    public partial class TastingCardStars : ContentView
    {
		public IList<Image> RatingStarsImg { get; private set; }

        public TastingCardStars()
        {
            InitializeComponent();
			RatingStarsImg = new List<Image>();
            RatingStarsImg.Add(Star1);
            RatingStarsImg.Add(Star2);
            RatingStarsImg.Add(Star3);
            RatingStarsImg.Add(Star4);
            RatingStarsImg.Add(Star5);
        }


		public double RatingValue
        {
            get { return (double)GetValue(RatingValueProperty); }
            set { SetValue(RatingValueProperty, value); }
        }

        public static BindableProperty RatingValueProperty =
            BindableProperty.Create(nameof(RatingValue), typeof(double), typeof(ChampagneCardStars), 0.0, propertyChanged: RatingValueChanged);

        private static void RatingValueChanged(BindableObject bindable, object newValue, object oldValue)
        {
			var control = (TastingCardStars)bindable;

            if(control != null)
            {
                control.SetStars();
            }
        }

        private void SetStars()
        {
            //Round the value to ensure it only hold 1 decimal
            var value = RatingValue * 2;
            var roundedValue = (Math.Round(value, 0)) / 2;
            var starsCurrentlyDisplayed = roundedValue;
            var indexNumber = 0;
            foreach(Image star in RatingStarsImg)
            {
                if(starsCurrentlyDisplayed - indexNumber == 0.5)
                {
                    star.Source = "HalfStar";
                }
                else if(indexNumber < roundedValue)
                {
                    star.Source = "StarGold";
                }
                else
                {
                    star.Source = "Star";
                }

                indexNumber += 1;
            }
            
        }
    }
}
