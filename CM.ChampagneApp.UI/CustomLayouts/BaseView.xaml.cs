using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Windows.Input;
using System.Threading.Tasks;

namespace CM.ChampagneApp.UI.CustomLayouts
{
	public partial class ReconnectView : ContentView
    {

        private bool OutOfServiceVisibilityState = false;

		public delegate void TryAgain(object sender, System.EventArgs e);
		public event TryAgain OnTryAgain;

		public delegate void NavigateBack(object sender, System.EventArgs e);
		public event NavigateBack OnNavigateBack;

		public delegate void RightNavIconClicked(object sender, System.EventArgs e);
		public event RightNavIconClicked OnRightNavIconClicked;

		public ReconnectView()
        {
            InitializeComponent();
        }

		void Handle_OnTryAgain(object sender, System.EventArgs e)
		{
			if(OnTryAgain != null)
			{
				OnTryAgain(sender, e);
			}
		}

		void Handle_OnLeftNavIconClicked(object sender, System.EventArgs e)
		{
			if(OnNavigateBack != null)
			{
				OnNavigateBack(sender, e);
			}
		}

		void Handle_OnRightNavIconClicked(object sender, System.EventArgs e)
		{
			if(OnRightNavIconClicked != null)
			{
				OnRightNavIconClicked(sender, e);
			}
		}

        public bool IsNavigationVisible
		{
			get { return (bool)GetValue(IsNavigationVisibleProperty); }
			set { SetValue(IsNavigationVisibleProperty, value); }
		}

		public static BindableProperty IsNavigationVisibleProperty =
			BindableProperty.Create(nameof(IsNavigationVisible), typeof(bool), typeof(ReconnectView), false);

        public Color NavigationColor
		{
			get { return (Color)GetValue(NavigationColorProperty); }
			set { SetValue(NavigationColorProperty, value); }
		}

		public static BindableProperty NavigationColorProperty =
			BindableProperty.Create(nameof(NavigationColor), typeof(Color), typeof(ReconnectView), Color.Transparent);

		public View AddContent
		{
			get { return (View)GetValue(AddContentProperty); }
			set { SetValue(AddContentProperty, value); }
		}

		public static BindableProperty AddContentProperty =
			BindableProperty.Create(nameof(AddContent), typeof(View), typeof(ReconnectView), propertyChanged: AddContentChanged);

		private static void AddContentChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (ReconnectView)bindable;

			if(control != null)
			{            
				control.innerView.Content = (View)newValue;
			}
		}

        public string LeftIcon
		{
			get { return (string)GetValue(LeftIconProperty); }
			set { SetValue(LeftIconProperty, value); }
		}

		public static BindableProperty LeftIconProperty =
			BindableProperty.Create(nameof(LeftIcon), typeof(string), typeof(ReconnectView), "BackArrowIcon.png");

        public string RightIcon
		{
			get { return (string)GetValue(RightIconProperty); }
			set { SetValue(RightIconProperty, value); }
		}

		public static BindableProperty RightIconProperty =
			BindableProperty.Create(nameof(RightIcon), typeof(string), typeof(ReconnectView), null);

        public ICommand Reconnect
		{
			get { return (ICommand)GetValue(ReconnectProperty); }
			set { SetValue(ReconnectProperty, value); }
		}

		public static BindableProperty ReconnectProperty =
			BindableProperty.Create(nameof(Reconnect), typeof(ICommand), typeof(ReconnectView));


        public bool IsEmptyStateBackgroundVisible
		{
			get { return (bool)GetValue(IsEmptyStateBackgroundVisibleProperty); }
			set { SetValue(IsEmptyStateBackgroundVisibleProperty, value); }
		}

		public static BindableProperty IsEmptyStateBackgroundVisibleProperty =
			BindableProperty.Create(nameof(IsEmptyStateBackgroundVisible), typeof(bool), typeof(ReconnectView), true);
              
        public bool IsOutOfService
		{
			get { return (bool)GetValue(IsOutOfServiceProperty); }
			set { SetValue(IsOutOfServiceProperty, value); }
		}

		public static BindableProperty IsOutOfServiceProperty =
			BindableProperty.Create(nameof(IsOutOfService), typeof(bool), typeof(ReconnectView), false, propertyChanged: IsOutOfServiceChanged);

        public bool IsOutOfServiceHelper
		{
			get { return (bool)GetValue(IsOutOfServiceHelperProperty); }
			set { SetValue(IsOutOfServiceHelperProperty, value); }
		}

		public static BindableProperty IsOutOfServiceHelperProperty =
			BindableProperty.Create(nameof(IsOutOfServiceHelper), typeof(bool), typeof(ReconnectView), true, propertyChanged: IsOutOfServiceChanged);

		private static void IsOutOfServiceChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (ReconnectView)bindable;

			if(control != null)
			{
				System.Diagnostics.Debug.WriteLine((bool)newValue);
                control.SetVisibility((bool)newValue);
			}         
		}

        private async Task SetVisibility(bool isOutOfService)
		{
            OutOfServiceVisibilityState = isOutOfService;
			if(isOutOfService)
			{
                innerView.IsVisible = false;
				OutOfServiceScreen.Opacity = 0;
				OutOfServiceScreen.IsVisible = true;            
				await OutOfServiceScreen.FadeTo(1, 250);            
			}
			else
			{
				await OutOfServiceScreen.FadeTo(0, 250);
				OutOfServiceScreen.IsVisible = false;
				innerView.IsVisible = true;
                if(OutOfServiceVisibilityState != OutOfServiceScreen.IsVisible)
                {
                    OutOfServiceScreen.IsVisible = true;
                    innerView.IsVisible = false;
                }
            }
		}
    }
}
