using System;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.UI.Elements.Cards;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Collections;

namespace CM.ChampagneApp.UI.Elements.Lists
{
    public partial class HorizontalChampagneList : ContentView
    {

        public delegate void ChampagneClicked(object sender, ChampagneClickedEventArgs args);
        public event ChampagneClicked OnChampagneClicked;

        //The height equal the container
        private int cardWidth;

        //The padding factor on bot an top
        private int paddingFactor = 20;

        public HorizontalChampagneList()
        {
            InitializeComponent();

            cardWidth = (App.DisplaySettings.Width / 2);

            UpdateList();

        }


        public IEnumerable ItemSource
        {
            get { return (IEnumerable)GetValue(ItemSourceProperty);}
            set { SetValue(ItemSourceProperty, value); }
        }

        public static BindableProperty ItemSourceProperty =
            BindableProperty.Create(nameof(ItemSource), typeof(IEnumerable), typeof(HorizontalChampagneList), propertyChanged: ItemSourceChanged);


        private static void ItemSourceChanged(BindableObject bindable, object newValue, object oldValue)
        {
            var horizontalChampagneList = (HorizontalChampagneList)bindable;

            if(horizontalChampagneList != null)
            {
                horizontalChampagneList.UpdateList();
            }
        }


        private void UpdateList()
        {

            System.Diagnostics.Debug.WriteLine("Should update list");
            if(ItemSource != null)
            {
                foreach(Object champagne in ItemSource)
                {

                    var Champagne = new ChampagneCard();
                    Champagne.WidthRequest = cardWidth;
                    Champagne.BindingContext = champagne;
                    Champagne.OnChosenChampagne += new ChampagneCard.ChosenChampagne(Handle_OnChampagneClicked);
                    ContentPresenter.Children.Add(Champagne);

                }
            }
        }

        private void Handle_OnChampagneClicked(object sender, ChampagneClickedEventArgs args)
        {
            OnChampagneClicked(sender, args);
        }

        public void ClearItemSource()
        {
            ContentPresenter.Children.Clear();
        }
    }
}
