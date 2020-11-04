using System;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.UI.Elements.Cards;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Collections;
using System.Collections.Specialized;

namespace CM.ChampagneApp.UI.Elements.Lists
{
    public partial class HorizontalChampagneEditionList : ContentView
    {
        public delegate void ChampagneClicked(object sender, ChampagneEditionCardClickedEventArgs args);
        public event ChampagneClicked OnChampagneClicked;

        //The height equal the container
        private int cardWidth;

        //The padding factor on bot an top
        private int paddingFactor = 20;

        public HorizontalChampagneEditionList()
        {
            InitializeComponent();

            cardWidth = (App.DisplaySettings.Width / 2);

            UpdateList();

        }


        public IEnumerable ItemSource
        {
            get { return (IEnumerable)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        public static BindableProperty ItemSourceProperty =
            BindableProperty.Create(nameof(ItemSource), typeof(IEnumerable), typeof(HorizontalChampagneList), propertyChanged: ItemSourceChanged);


        private static void ItemSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var horizontalChampagneEditionList = (HorizontalChampagneEditionList)bindable;

            if (horizontalChampagneEditionList != null)
            {
				horizontalChampagneEditionList.OnItemSourceChanged((IEnumerable)oldValue, (IEnumerable)newValue);
                horizontalChampagneEditionList.UpdateList();
            }
        }

		private void OnItemSourceChanged(IEnumerable oldValue, IEnumerable newValue)
		{
			var oldValueINotifyCollectionChanged = oldValue as INotifyCollectionChanged;
			if(oldValueINotifyCollectionChanged != null)
			{
				oldValueINotifyCollectionChanged.CollectionChanged -= new NotifyCollectionChangedEventHandler(NewValueINotifyCollectionChanged);
			}

			var newValueINotifyCollectionChanged = newValue as INotifyCollectionChanged;
			if(newValueINotifyCollectionChanged != null)
			{
				newValueINotifyCollectionChanged.CollectionChanged += new NotifyCollectionChangedEventHandler(NewValueINotifyCollectionChanged);
			}
		}


		private void NewValueINotifyCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
		{

			var oldValue = (IEnumerable)args.OldItems;
			var newValue = (IEnumerable)args.NewItems;

			if(ItemSource != null)
			{
				foreach(Object champagne in newValue)
				{
					var Champagne = new ChampagneEditionCard();
					Champagne.WidthRequest = cardWidth;
					Champagne.BindingContext = champagne;
					Champagne.OnChosenChampagne += new ChampagneEditionCard.ChosenChampagne(Handle_OnChampagneClicked);
					ContentPresenter.Children.Add(Champagne);
				}
			}
		}      

        private void UpdateList()
        {

            System.Diagnostics.Debug.WriteLine("Should update list");
            if (ItemSource != null)
            {
                foreach (Object champagne in ItemSource)
                {

                    var Champagne = new ChampagneEditionCard();
                    Champagne.WidthRequest = cardWidth;
                    Champagne.BindingContext = champagne;
                    Champagne.OnChosenChampagne += new ChampagneEditionCard.ChosenChampagne(Handle_OnChampagneClicked);
					ContentPresenter.Children.Add(Champagne);

                }
            }
        }

        private void Handle_OnChampagneClicked(object sender, ChampagneEditionCardClickedEventArgs args)
        {
            OnChampagneClicked(sender, args);
        }

        public void ClearItemSource()
        {
			ContentPresenter.Children.Clear();
        }
    }
}

