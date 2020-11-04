using System;
using System.Collections;
using System.Collections.Generic;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.UI.Elements.Cards;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.CustomLayouts
{
    public partial class ChampagneGrid : ContentView
    {

        private int cardWidth;

        //Make ressource file for this!
        private int coloumnSpacing = 10;

        //Make resource file for this!
        private int rowSpacing = 30;

        //Make resource file for this!
        private int paddingFactor = 20;

        public delegate void ItemClicked(object sender, ChampagneClickedEventArgs args);
        public event ItemClicked OnItemClicked;

        public ChampagneGrid()
        {
            InitializeComponent();
            cardWidth = (App.DisplaySettings.Width / 2) - coloumnSpacing - paddingFactor;
        }


        public IEnumerable ItemSource
        {
            get { return (IEnumerable)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        public static BindableProperty ItemSourceProperty =
            BindableProperty.Create(nameof(ItemSource), typeof(IEnumerable), typeof(ChampagneGrid), propertyChanged: ItemSourceChanged);

        private static void ItemSourceChanged(BindableObject bindable, object newValue, object oldValue)
        {

            var ChampagneGrid = (ChampagneGrid)bindable;
            if(ChampagneGrid != null)
            {
                ChampagneGrid.UpdateList();
            }
        }

        private void UpdateList()
        {

            if(ItemSource != null)
            {
                ContentPresenter.Children.Clear();
                foreach(Object Champagne in ItemSource)
                {
                    var NextChampagne = new ChampagneCard();
                    NextChampagne.OnChosenChampagne += new ChampagneCard.ChosenChampagne(OnChosenChampagne);
                    NextChampagne.BindingContext = Champagne;
                    NextChampagne.WidthRequest = cardWidth;
                    ContentPresenter.Children.Add(NextChampagne);
                }
            }
        }

        private void OnChosenChampagne(object sender, ChampagneClickedEventArgs args)
        {
            OnItemClicked(sender, args);
        }

        protected override void OnParentSet()
        {
            //Upon load set the itemSource if such exist
            base.OnParentSet();

            ContentPresenter.ColumnSpacing = coloumnSpacing;
            ContentPresenter.RowSpacing = rowSpacing;
        }
    }
}
