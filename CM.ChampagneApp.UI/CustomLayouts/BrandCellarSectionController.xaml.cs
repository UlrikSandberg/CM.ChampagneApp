using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Collections;
using CM.ChampagneApp.UI.Elements.Sections;
using CM.ChampagneApp.UI.CustomEventArgs;

namespace CM.ChampagneApp.UI.CustomLayouts
{
    public partial class BrandCellarSectionController : ContentView
    {

		public delegate void ChampagneClicked(object sender, ChampagneClickedEventArgs args);
        public event ChampagneClicked OnChampagneClicked;
      
        public BrandCellarSectionController()
        {
            InitializeComponent();
        }      

		public IEnumerable ItemSource
		{
			get { return (IEnumerable)GetValue(ItemSourceProperty); }
			set { SetValue(ItemSourceProperty, value); }
		}

		public static BindableProperty ItemSourceProperty =
			BindableProperty.Create(nameof(ItemSource), typeof(IEnumerable), typeof(BrandCellarSectionController), propertyChanged: ItemSourceChanged);

		private static void ItemSourceChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (BrandCellarSectionController)bindable;

			if(control != null)
			{
				control.OnItemSourceChanged();
			}
		}

		private void OnItemSourceChanged()
		{
			if(ItemSource != null)
			{
				ContentPresenter.Children.Clear();
				foreach(var section in ItemSource)
				{
					var brandCellarSection = new BrandCellarSection();
					brandCellarSection.BindingContext = section;
					brandCellarSection.OnChampagneClicked += new BrandCellarSection.ChampagneClicked(BrandCellarSection_OnChampagneClicked);

					ContentPresenter.Children.Add(brandCellarSection);
				}
			}
		}


        public void BrandCellarSection_OnChampagneClicked(object sender, CustomEventArgs.ChampagneClickedEventArgs args)
        {
			if(OnChampagneClicked != null)
			{
				OnChampagneClicked(sender, args);
			}
        }
    }
}
