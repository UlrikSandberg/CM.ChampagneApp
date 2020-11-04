﻿using System;
using System.Collections;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.CustomLayouts.BrandInfoPageLayouts
{
    public partial class ChampagneBrandInfoPageUI : ContentView
    {
		public delegate void NavigateBack(object sender, System.EventArgs e);
		public event NavigateBack OnNavigateBack;

        public ChampagneBrandInfoPageUI()
        {
            InitializeComponent();
        }

		void Handle_OnLeftNavIconClicked(object sender, System.EventArgs e)
		{
			if(OnNavigateBack != null)
			{
				OnNavigateBack(sender, e);
			}
		}
      
		public string NavigationTitle
        {
            get { return (string)GetValue(NavigationTitleProperty); }
            set { SetValue(NavigationTitleProperty, value); }
        }

		public static BindableProperty NavigationTitleProperty =
            BindableProperty.Create(nameof(NavigationTitle), typeof(string), typeof(InfoPage));

        public string NavigationSubTitle
		{
            get { return (string)GetValue(NavigationSubTitleProperty); }
            set { SetValue(NavigationSubTitleProperty, value); }
        }

        public static BindableProperty NavigationSubTitleProperty =
			BindableProperty.Create(nameof(NavigationSubTitle), typeof(string), typeof(InfoPage));

		public string ScrolledNavigationTitle
        {
            get { return (string)GetValue(ScrolledNavigationTitleProperty); }
            set { SetValue(ScrolledNavigationTitleProperty, value); }
        }

        public static BindableProperty ScrolledNavigationTitleProperty =
            BindableProperty.Create(nameof(ScrolledNavigationTitle), typeof(string), typeof(InfoPage));

        public string ScrolledNavigationSubTitle
        {
            get { return (string)GetValue(ScrolledNavigationSubTitleProperty); }
            set { SetValue(ScrolledNavigationSubTitleProperty, value); }
        }

        public static BindableProperty ScrolledNavigationSubTitleProperty =
			BindableProperty.Create(nameof(ScrolledNavigationSubTitle), typeof(string), typeof(InfoPage));

		public string HeaderImg
        {
            get { return (string)GetValue(HeaderImgProperty); }
            set { SetValue(HeaderImgProperty, value); }
        }

        public static BindableProperty HeaderImgProperty =
			BindableProperty.Create(nameof(HeaderImg), typeof(string), typeof(InfoPage));


		public IEnumerable ItemSource
		{
			get { return (IEnumerable)GetValue(ItemSourceProperty); }
			set { SetValue(ItemSourceProperty, value); }
		}

		public static BindableProperty ItemSourceProperty =
			BindableProperty.Create(nameof(ItemSource), typeof(IEnumerable), typeof(ChampagneBrandInfoPageUI), propertyChanged: ItemSourceChanged);

		private static void ItemSourceChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (ChampagneBrandInfoPageUI)bindable;

			if(control != null)
			{
				control.OnItemSourceChanged();
			}
		}

        private void OnItemSourceChanged()
		{
			if(ItemSource != null)
			{

				System.Diagnostics.Debug.WriteLine("Should layout champagneUI sections");

				foreach(var section in ItemSource)
				{
					var uiSection = new ChampagneUISection();
                    uiSection.InjectBindingContext(section);

                    InnerContentStack.Children.Add(uiSection);
				}
			}
		}
    }
}
