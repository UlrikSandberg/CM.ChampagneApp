using System;
using System.Collections.Generic;
using CM.ChampagneApp.UI.Resources;

using Xamarin.Forms;

namespace CM.ChampagneApp.UI.CustomLayouts
{
    public partial class ContentBackground : ContentView
    {
        public delegate void LeftIconClicked(object sender, System.EventArgs e);
        public event LeftIconClicked OnLeftNavIconClicked;

        public delegate void RightIconClicked(object sender, System.EventArgs e);
        public event RightIconClicked OnRightNavIconClicked;

        public delegate void TitleClicked(object sender, System.EventArgs e);
        public event TitleClicked OnNavTitleClicked;

        public delegate void Handle_OnScrolled(object sender, ScrolledEventArgs e);
        public event Handle_OnScrolled OnHandle_OnScrolled;

        public ContentBackground()
        {
            InitializeComponent();

            if(App.IsOnIphoneX)
            {
                ChampagneProfileHeader.Margin = new Thickness(0, -30, 0, 0);
                ChampagneprofileHeaderOverlay.Margin = new Thickness(0, -30, 0, 0);
            }
        }

        void Handle_Scrolled(object sender, Xamarin.Forms.ScrolledEventArgs e)
        {
            
            if(e.ScrollY > (App.DisplaySettings.Width * 0.6) - (App.DisplaySettings.Width / 6) - 20)
            {
                NavBar.BackgroundColor = Color.FromHex("#0B0E1D");
                NavBar.Title = ScrolledTitle;
                NavBar.SubTitle = ScrolledSubTitle;
            }
            else
            {            
                NavBar.BackgroundColor = Color.Transparent;
                NavBar.Title = NavTitle;
                NavBar.SubTitle = NavSubTitle;
            }

            if(OnHandle_OnScrolled != null)
            {
                OnHandle_OnScrolled(sender, e);
            }

            Handle_StretcyHeader(e.ScrollY);
        }

        private void Handle_StretcyHeader(double yOffset)
        {
            if (yOffset < 0) //We are stretching the view downwards
            {
                //Make the scaling follow a linear function divided by 100 this 
                //way the function when plot follow the factor of the scaling proportionally
                ChampagneProfileHeader.Scale = 1.0 + (-1 * yOffset) / 100;
                ChampagneprofileHeaderOverlay.Scale = 1.0 + (-1 * yOffset) / 100;
            }
        }

        public double CoverImageOpacity
		{
			get { return (double)GetValue(CoverImageOpacityProperty); }
			set { SetValue(CoverImageOpacityProperty, value); }
		}

        public bool IsBadgeVisible
        {
            get { return (bool)GetValue(IsBadgeVisibleProperty); }
            set { SetValue(IsBadgeVisibleProperty, value); }
        }

        public static BindableProperty IsBadgeVisibleProperty =
            BindableProperty.Create(nameof(IsBadgeVisible), typeof(bool), typeof(ContentBackground), false);

        public static BindableProperty CoverImageOpacityProperty =
			BindableProperty.Create(nameof(CoverImageOpacity), typeof(double), typeof(ContentBackground), 0.3);

        public View AddContent
        {
            get { return (View)GetValue(AddContentProperty); }
            set { SetValue(AddContentProperty, value); }
        }

        public static BindableProperty AddContentProperty =
            BindableProperty.Create(nameof(AddContent), typeof(View), typeof(ContentBackground), propertyChanged: AddContentChanged);


        private static void AddContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var Content = (ContentBackground)bindable;

            if(Content != null)
            {
                Content.contentPresenter.Content = (View)newValue;
            }
        }

        void Handle_OnLeftIconClicked(object sender, System.EventArgs e)
        {
            if (OnLeftNavIconClicked != null)
            {
                OnLeftNavIconClicked(sender, e);
            }
        }

        void Handle_OnRightIconClicked(object sender, System.EventArgs e)
        {
			System.Diagnostics.Debug.WriteLine("Content Background right pressed");
            if (OnRightNavIconClicked != null)
            {
                OnRightNavIconClicked(sender, e);
            }
        }

        void Handle_OnTitleClicked(object sender, System.EventArgs e)
        {
            if (OnNavTitleClicked != null)
            {
                OnNavTitleClicked(sender, e);
            }
        }

        public string ScrolledTitle
        {
            get { return (string)GetValue(ScrolledTitleProperty); }
            set { SetValue(ScrolledTitleProperty, value); }
        }

        public static BindableProperty ScrolledTitleProperty =
            BindableProperty.Create(nameof(ScrolledTitle), typeof(string), typeof(ContentBackground));

        public string ScrolledSubTitle
        {
            get { return (string)GetValue(ScrolledSubTitleProperty); }
            set { SetValue(ScrolledSubTitleProperty, value); }
        }

        public static BindableProperty ScrolledSubTitleProperty =
            BindableProperty.Create(nameof(ScrolledSubTitle), typeof(string), typeof(ContentBackground));

        public string NavTitle
        {
            get { return (string)GetValue(NavTitleProperty); }
            set { SetValue(NavTitleProperty, value); }
        }

        public static BindableProperty NavTitleProperty =
            BindableProperty.Create(nameof(NavTitle), typeof(string), typeof(ContentBackground));

        public string NavSubTitle
        {
            get { return (string)GetValue(NavSubTitleProperty); }
            set { SetValue(NavTitleProperty, value); }
        }

        public static BindableProperty NavSubTitleProperty =
            BindableProperty.Create(nameof(NavSubTitle), typeof(string), typeof(ContentBackground));

        public string NavLeftIcon
        {
            get { return (string)GetValue(NavLeftIconProperty); }
            set { SetValue(NavLeftIconProperty, value); }
        }

        public static BindableProperty NavLeftIconProperty =
            BindableProperty.Create(nameof(NavLeftIcon), typeof(string), typeof(ContentBackground));

        public string NavRightIcon
        {
            get { return (string)GetValue(NavRightIconProperty); }
            set { SetValue(NavRightIconProperty, value); }
        }

		public bool IsToggle
        {
            get { return (bool)GetValue(IsToggleProperty); }
            set { SetValue(IsToggleProperty, value); }
        }
        
        public static BindableProperty IsToggleProperty =
			BindableProperty.Create(nameof(IsToggle), typeof(bool), typeof(ContentBackground), false, propertyChanged: IsTogglePropertyChanged);

        public bool Toggle
        {
            get { return (bool)GetValue(ToggleProperty); }
            set { SetValue(ToggleProperty, value); }
        }

        public static BindableProperty ToggleProperty =
			BindableProperty.Create(nameof(Toggle), typeof(bool), typeof(ContentBackground), false, propertyChanged: TogglePropertyChanged);

        public string TrueValueImg
        {
            get { return (string)GetValue(TrueValueImgProperty); }
            set { SetValue(TrueValueImgProperty, value); }
        }

        public static BindableProperty TrueValueImgProperty =
			BindableProperty.Create(nameof(TrueValueImg), typeof(string), typeof(ContentBackground), propertyChanged: TrueValueImgChanged);

        public string FalseValueImg
        {
            get { return (string)GetValue(FalseValueImgProperty); }
            set { SetValue(FalseValueImgProperty, value); }
        }

		public static BindableProperty FalseValueImgProperty =
			BindableProperty.Create(nameof(TrueValueImg), typeof(string), typeof(ContentBackground), propertyChanged: FalseValueImgChanged);


        public static BindableProperty NavRightIconProperty =
			BindableProperty.Create(nameof(NavRightIcon), typeof(string), typeof(ContentBackground), propertyChanged: RightIconChanged);


        public bool RightButtonVisible
        {
            get { return (bool)GetValue(RightButtonVisibleProperty); }
            set { SetValue(RightButtonVisibleProperty, value); }
        }

        public static BindableProperty RightButtonVisibleProperty =
            BindableProperty.Create(nameof(RightButtonVisible), typeof(bool), typeof(ContentBackground), false);


        public string HeaderImg
		{
			get { return (string)GetValue(HeaderImgProperty); }
			set { SetValue(HeaderImgProperty, value); }
		}

		public static BindableProperty HeaderImgProperty =
			BindableProperty.Create(nameof(HeaderImg), typeof(string), typeof(ContentBackground));

		private static void IsTogglePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {

			var transNavBar = (ContentBackground)bindable;

            if (transNavBar != null)
            {
				transNavBar.IsToggleChanged((bool) newValue);
            }
        }

        private void IsToggleChanged(bool newValue)
		{
			IsToggle = newValue;
			NavBar.IsToggle = newValue;
		}


		private static void TogglePropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var transNavBar = (ContentBackground)bindable;

			if(transNavBar != null)
			{
				transNavBar.ToggleChanged((bool)newValue);
			}
		}

        private void ToggleChanged(bool newValue)
		{
			Toggle = newValue;
			NavBar.Toggle = newValue;
		}

		private static void TrueValueImgChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var transNavBar = (ContentBackground)bindable;

            if (transNavBar != null)
            {
				transNavBar.NavBar.TrueValueImg = (string)newValue;
            }
		}

		private static void FalseValueImgChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var transNavBar = (ContentBackground)bindable;

            if (transNavBar != null)
            {
				transNavBar.NavBar.FalseValueImg = (string)newValue;
            }
        }

		private static void RightIconChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var transNavBar = (ContentBackground)bindable;

            if (transNavBar != null)
            {
				transNavBar.NavBar.RightIcon = (string)newValue;
            }
        }
    }
}
