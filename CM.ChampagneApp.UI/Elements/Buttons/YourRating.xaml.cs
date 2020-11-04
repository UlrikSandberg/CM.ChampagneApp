using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Buttons
{
    public partial class YourRating : ContentView
    {

        public delegate void YourRatingClicked(object sender, System.EventArgs e);
        public event YourRatingClicked OnYourRatingClicked;

        public YourRating()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            OnYourRatingClicked(sender , e);
        }

        public string Rating
		{
			get { return (string)GetValue(RatingProperty); }
			set { SetValue(RatingProperty, value); }
		}

		public static BindableProperty RatingProperty =
			BindableProperty.Create(nameof(Rating), typeof(string), typeof(YourRating));
    }
}
