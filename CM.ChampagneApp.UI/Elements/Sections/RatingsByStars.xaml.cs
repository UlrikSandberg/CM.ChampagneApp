using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Sections
{
    public partial class RatingsByStars : ContentView
    {

        public List<Label> RatingLabels { get; set; }

        public RatingsByStars()
        {
            InitializeComponent();
            RatingLabels = new List<Label>();
            RatingLabels.Add(Rating1);
            RatingLabels.Add(Rating2);
            RatingLabels.Add(Rating3);
            RatingLabels.Add(Rating4);
            RatingLabels.Add(Rating5);
        }

        public IEnumerable ItemSource
        {
            get { return (IEnumerable)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        public static BindableProperty ItemSourceProperty =
            BindableProperty.Create(nameof(ItemSource), typeof(IEnumerable), typeof(RatingsByStars), propertyChanged: ItemSourceChanged);

        private static void ItemSourceChanged(BindableObject bindable, object newValue, object oldValue)
        {

            var Control = (RatingsByStars)bindable;

            if (Control != null)
            {
                Control.UpdateStars();
            }

        }

        private void UpdateStars()
        {
            System.Diagnostics.Debug.WriteLine("Should update stars");

            if (ItemSource != null)
            {
                
                var ItemSourceList = ((List<int>)ItemSource);

                if (ItemSourceList.Count == 5)
                {
                    var index = 0;
                    foreach (Label rating in RatingLabels)
                    {
                        rating.Text = "" + ItemSourceList[index];
                        index += 1;
                    }
                }

            }
        }
    }
}

