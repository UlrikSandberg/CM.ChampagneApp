using System;
using System.Collections.Generic;
using System.Windows.Input;
using CM.ChampagneApp.UI.CustomEventArgs;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Buttons
{
    public partial class RatingStars : ContentView
    {

        public delegate void ChosenRating(object sender, SelectedRatingEventArgs args);
        public event ChosenRating OnChosenRating;

        public double minimumX { get; private set; }
        public double maxX { get; private set; }

        public double selectedX { get; private set; }

        public double StarsCurrentlyDisplayed { get; set; }
        public IList<Image> RatingStarsImg { get; set; }

        public ICommand GetNativePanGestureArgs { get; set; }
        public ICommand TapGestureRecognized { get; set; }
        public ICommand NativePanGestureEnded { get; set; }

        public RatingStars()
        {
            InitializeComponent();
            GetNativePanGestureArgs = new Command<NativePanGestureEventArgs>(Handle_NativePanGestures);
            NativePanGestureEnded = new Command<NativePanGestureEventArgs>(Handle_NativePanGestureEnded);
            TapGestureRecognized = new Command<NativePanGestureEventArgs>(Handle_NativeTapGesture);
            RatingStarsImg = new List<Image>();
            RatingStarsImg.Add(Star1);
            RatingStarsImg.Add(Star2);
            RatingStarsImg.Add(Star3);
            RatingStarsImg.Add(Star4);
            RatingStarsImg.Add(Star5);


        }

        private void Handle_NativePanGestures(NativePanGestureEventArgs args)
        {
            minimumX = (App.DisplaySettings.Width / 2 - StarsContainer.Width / 2) - 20;
            maxX = (App.DisplaySettings.Width / 2 + StarsContainer.Width / 2) - 20;

            if (args.PointOfTouchInViewX - minimumX >= 0 && args.PointOfTouchInViewX < maxX)
            {
                var startX = args.PointOfTouchInViewX - minimumX;
                var currentX = (startX / StarsContainer.Width) * 10;
                var roundedX = (Math.Round(currentX, 0)) / 2;
                selectedX = args.PointOfTouchInViewX - minimumX;
                SetStars(roundedX);
            }
        }

        private void Handle_NativePanGestureEnded(NativePanGestureEventArgs args)
        {
            var currentX = (selectedX / StarsContainer.Width) * 10;
            var roundedX = (Math.Round(currentX, 0)) / 2;
            var SelectedRatingArgs = new SelectedRatingEventArgs(roundedX);
            OnChosenRating(this, SelectedRatingArgs);

        }

        private void Handle_NativeTapGesture(NativePanGestureEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine(args.PointOfTouchInViewX);
            System.Diagnostics.Debug.WriteLine(args.PointOfTouchInViewY);

            minimumX = (App.DisplaySettings.Width / 2 - StarsContainer.Width / 2) - 20;
            maxX = (App.DisplaySettings.Width / 2 + StarsContainer.Width / 2) - 20;

            if (args.PointOfTouchInViewX - minimumX >= 0 && args.PointOfTouchInViewX < maxX)
            {
                var startX = args.PointOfTouchInViewX - minimumX;
                var currentX = (startX / StarsContainer.Width) * 10;
                var roundedX = (Math.Round(currentX, 0)) / 2;
                selectedX = args.PointOfTouchInViewX - minimumX;
                SetStars(roundedX);
                if(roundedX > 0.0 && roundedX <= 5.0)
                {
                    var SelectedRatingArgs = new SelectedRatingEventArgs(roundedX);
                    OnChosenRating(this, SelectedRatingArgs);
                }
            }
        }

        private void SetStars(double value)
        {
            if (StarsCurrentlyDisplayed != value)
            {

                StarsCurrentlyDisplayed = value;
                System.Diagnostics.Debug.WriteLine("Current value: " + StarsCurrentlyDisplayed);
                var IndexNumber = 0;
                System.Diagnostics.Debug.WriteLine(StarsCurrentlyDisplayed - IndexNumber);
                foreach (Image star in RatingStarsImg)
                {

                    if (StarsCurrentlyDisplayed - IndexNumber == 0.5)
                    {
                        star.Source = "HalfStar";
                    }
                    else if (IndexNumber < value)
                    {
                        star.Source = "StarGold";
                    }
                    else
                    {
                        star.Source = "Star";
                    }

                    IndexNumber += 1;
                }
            }
        }

        public double StartValue
        {
            get { return (double)GetValue(StartValueProperty); }
            set { SetValue(StartValueProperty, value); }
        }

        public static BindableProperty StartValueProperty =
            BindableProperty.Create(nameof(StartValue), typeof(double), typeof(RatingStars), 0.0, propertyChanged: StartValueChanged);

        private static void StartValueChanged(BindableObject bindable, object newValue, object oldValue)
        {
            var RatingStars = (RatingStars)bindable;

            if(RatingStars != null)
            {
                RatingStars.SetInitialValue();
            }
        }

        private void SetInitialValue()
        {
            SetStars(StartValue);
        }
    }
}
