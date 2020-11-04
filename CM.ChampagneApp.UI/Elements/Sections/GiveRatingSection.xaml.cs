using System;
using System.Collections.Generic;
using System.Windows.Input;
using CM.ChampagneApp.UI.CustomEventArgs;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Sections
{
    public partial class GiveRatingSection : ContentView
    {

        public delegate void ChosenRating(object sender, SelectedRatingEventArgs args);
        public event ChosenRating OnChosenRating;

        public delegate void YourRatingClicked(object sender, System.EventArgs e);
        public event YourRatingClicked OnYourRatingClicked;

        public static BindableProperty StartTastingCommandProperty =
            BindableProperty.Create(nameof(StartTastingCommand), typeof(ICommand), typeof(GiveRatingSection));

        public GiveRatingSection()
        {
            InitializeComponent();
            GiveRating.IsVisible = true;
            YourRating.IsVisible = false;
        }

        void Handle_OnYourRatingClicked(object sender, System.EventArgs e)
        {
            if(OnYourRatingClicked != null)
            {
                OnYourRatingClicked(sender, e);
            }

            if(StartTastingCommand != null)
            {
                if(StartTastingCommand.CanExecute(null))
                {
                    StartTastingCommand.Execute(null);
                }
            }
        }

        void Handle_OnChosenRating(object sender, SelectedRatingEventArgs args)
        {
            OnChosenRating(sender, args);
        }

        public ICommand StartTastingCommand
        {
            get { return (ICommand)GetValue(StartTastingCommandProperty); }
            set { SetValue(StartTastingCommandProperty, value); }
        }

        public string Rating
		{
			get { return (string)GetValue(RatingProperty); }
			set { SetValue(RatingProperty, value); }
		}

		public static BindableProperty RatingProperty =
			BindableProperty.Create(nameof(Rating), typeof(string), typeof(GiveRatingSection));

        public bool IsGiveRatingVisible
        {
            get { return (bool)GetValue(IsGiveRatingVisibleProperty); }
            set { SetValue(IsGiveRatingVisibleProperty, value); }
        }

        public static BindableProperty IsGiveRatingVisibleProperty =
            BindableProperty.Create(nameof(IsGiveRatingVisible), typeof(bool), typeof(GiveRatingSection), false, propertyChanged: IsGiveRatingVisibleChanged);

        private static void IsGiveRatingVisibleChanged(BindableObject bindable, object newValue, object oldValue)
        {
            
            var Control = (GiveRatingSection)bindable;

            if(Control != null)
            {
                Control.SetVisibility();
            }
        }

        private void SetVisibility()
        {

            if(IsGiveRatingVisible)
            {
                GiveRating.IsVisible = false;
                YourRating.IsVisible = true;
            }
            else
            {
                GiveRating.IsVisible = true;
                YourRating.IsVisible = false;
            }
        }
    }
}
