using System;
using System.Collections;
using System.Collections.Generic;
using CM.ChampagneApp.UI.Elements.Cards;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Lists
{
    public partial class TechnicalList : ContentView
    {
        public TechnicalList()
        {
            InitializeComponent();
        }

        public IEnumerable ItemSource
        {

            get { return (IEnumerable)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }

        }

        public static BindableProperty ItemSourceProperty =
            BindableProperty.Create(nameof(ItemSource), typeof(IEnumerable), typeof(TechnicalList), propertyChanged: ItemSourceChanged);


        private static void ItemSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var TechnicalList = (TechnicalList)bindable;

            if(TechnicalList != null)
            {
                TechnicalList.UpdateContent();
            }
        }


        private void UpdateContent()
        {
			ContentPresenter.Children.Clear();
            if(ItemSource != null)
            {
                foreach (Object Identity in ItemSource)
                {
                    ContentPresenter.Children.Add(new TechnicalIdentityCard
                    {
                        BindingContext = Identity,

                    });
                }
            }
        }
    }
}
