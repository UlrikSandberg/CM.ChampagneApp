using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Collections;

namespace CM.ChampagneApp.UI.CustomLayouts.BrandInfoPageLayouts
{
    public partial class InfoPage : ContentView
    {

		public delegate void NavigateBack(object sender, System.EventArgs e);
        public event NavigateBack OnNavigateBack;

        public InfoPage()
        {
            InitializeComponent();
        }

		void Handle_OnNavigateBack(object sender, System.EventArgs e)
		{
			if(OnNavigateBack != null)
			{
				OnNavigateBack(sender, e);
			}
		}

        public bool IsVintageUI
		{
			get { return (bool)GetValue(IsVintageUIProperty); }
			set { SetValue(IsVintageUIProperty, value); }
		}

		public static BindableProperty IsVintageUIProperty =
			BindableProperty.Create(nameof(IsVintageUI), typeof(bool), typeof(InfoPage), false, propertyChanged: IsVintageUIChanged);
        
		public bool IsVintageUIInverse
        {
            get { return (bool)GetValue(IsVintageUIInverseProperty); }
            set { SetValue(IsVintageUIInverseProperty, value); }
        }

        public static BindableProperty IsVintageUIInverseProperty =
            BindableProperty.Create(nameof(IsVintageUIInverse), typeof(bool), typeof(InfoPage), true, propertyChanged: IsVintageUIChanged);

		private static void IsVintageUIChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (InfoPage)bindable;

			if(control != null)
			{

				if((bool)newValue)
				{
					control.ChampagneUI.IsVisible = false;
					control.VintageUI.IsVisible = true;
				}
				else
				{
					control.ChampagneUI.IsVisible = true;
                    control.VintageUI.IsVisible = false;
				}            
			}
		}

        public string HeaderImg
		{
			get { return (string)GetValue(HeaderImgProperty); }
			set { SetValue(HeaderImgProperty, value); }
		}

		public static BindableProperty HeaderImgProperty =
			BindableProperty.Create(nameof(HeaderImg), typeof(string), typeof(InfoPage));
      
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


		public IEnumerable ItemSource
		{
			get { return (IEnumerable)GetValue(ItemSourceProperty); }
			set { SetValue(ItemSourceProperty, value); }
		}

		public static BindableProperty ItemSourceProperty =
			BindableProperty.Create(nameof(ItemSource), typeof(IEnumerable), typeof(InfoPage));

    }
}
