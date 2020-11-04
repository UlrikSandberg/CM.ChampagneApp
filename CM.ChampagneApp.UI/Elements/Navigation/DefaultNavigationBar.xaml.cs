using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;

namespace CM.ChampagneApp.UI.Elements.Navigation
{
    public partial class DefaultNavigationBar : ContentView
    {

		public delegate void LeftIconClicked(object sender, System.EventArgs e);
        public event LeftIconClicked OnLeftNavIconClicked;

		public delegate void TitleClicked(object sender, System.EventArgs e);
        public event TitleClicked OnNavTitleClicked;

		public delegate void RightIconClicked(object sender, System.EventArgs e);
        public event RightIconClicked OnRightNavIconClicked;

        public DefaultNavigationBar()
        {
            InitializeComponent();

            if(App.IsOnIphoneX)
            {
                NavigationPadding.HeightRequest += 20;
            }
        }

        /* Left side functionality
         * 
         * ***** Left *****
         * 
         */

		public ICommand LeftIconCommand
		{
			get { return (ICommand)GetValue(LeftIconCommandProperty); }
			set { SetValue(LeftIconCommandProperty, value); }
		}

		public static BindableProperty LeftIconCommandProperty =
			BindableProperty.Create(nameof(LeftIconCommand), typeof(ICommand), typeof(DefaultNavigationBar));

		void Handle_OnLeftIconClicked(object sender, System.EventArgs e)
        {
			System.Diagnostics.Debug.WriteLine("Left clicked pushed");
            if (OnLeftNavIconClicked != null)
            {
                OnLeftNavIconClicked(sender, e);
            }
        }

        public bool IsBadgeVisible
        {
            get { return (bool)GetValue(IsBadgeVisibleProperty); }
            set { SetValue(IsBadgeVisibleProperty, value); }
        }

        public static BindableProperty IsBadgeVisibleProperty =
            BindableProperty.Create(nameof(IsBadgeVisible), typeof(bool), typeof(DefaultNavigationBar), false);

		public string LeftIcon
        {
            get { return (string)GetValue(LeftIconProperty); }
            set { SetValue(LeftIconProperty, value); }
        }

		public static BindableProperty LeftIconProperty =
            BindableProperty.Create(nameof(LeftIcon), typeof(string), typeof(DefaultNavigationBar));

      
        /* Titles, Headers and the middle functionality
         * 
         * ***** Middle *****
         * 
         */      

		void Handle_OnTitleClicked(object sender, System.EventArgs e)
        {
			System.Diagnostics.Debug.WriteLine("Tittle clicked pushed");
            if (OnNavTitleClicked != null)
            {
                OnNavTitleClicked(sender, e);
            }
        }

		public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(DefaultNavigationBar));

        public string SubTitle
        {
            get { return (string)GetValue(SubTitleProperty); }
            set { SetValue(SubTitleProperty, value); }
        }

        public static BindableProperty SubTitleProperty =
            BindableProperty.Create(nameof(SubTitle), typeof(string), typeof(DefaultNavigationBar));

        /* Right side functionality
         * 
         * ***** Right *****
         * 
         */      

		public ICommand RightIconCommand
        {
            get { return (ICommand)GetValue(RightIconCommandProperty); }
            set { SetValue(RightIconCommandProperty, value); }
        }

        public static BindableProperty RightIconCommandProperty =
            BindableProperty.Create(nameof(RightIconCommand), typeof(ICommand), typeof(DefaultNavigationBar));


		void Handle_OnRightIconClicked(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Right clicked pushed");
            if (OnRightNavIconClicked != null)
            {
                OnRightNavIconClicked(sender, e);
            }
        }

		public string RightIcon
        {
            get { return (string)GetValue(RightIconProperty); }
            set { SetValue(RightIconProperty, value); }
        }

        public static BindableProperty RightIconProperty =
            BindableProperty.Create(nameof(RightIcon), typeof(string), typeof(DefaultNavigationBar));
        
		public string TrueValueImg
        {
			get { return (string)GetValue(TrueValueImgProperty); }
			set { SetValue(TrueValueImgProperty, value); }
        }

		public static BindableProperty TrueValueImgProperty =
			BindableProperty.Create(nameof(TrueValueImg), typeof(string), typeof(DefaultNavigationBar));

		public string FalseValueImg
        {
			get { return (string)GetValue(FalseValueImgProperty); }
			set { SetValue(FalseValueImgProperty, value); }
        }

		public static BindableProperty FalseValueImgProperty =
			BindableProperty.Create(nameof(FalseValueImg), typeof(string), typeof(DefaultNavigationBar));


        // --- IsToggle start
        public bool IsToggle
		{
			get { return (bool)GetValue(IsToggleProperty); }
			set { SetValue(IsToggleProperty, value); }
		}

		public static BindableProperty IsToggleProperty =
			BindableProperty.Create(nameof(IsToggle), typeof(bool), typeof(DefaultNavigationBar), false, propertyChanged:IsTogglePropertyChanged);
       
        // --- IsToggleEnded


        // -- Toggle
		public bool Toggle
        {
            get { return (bool)GetValue(ToggleProperty); }
            set { SetValue(ToggleProperty, value); }
        }

        public static BindableProperty ToggleProperty =
            BindableProperty.Create(nameof(Toggle), typeof(bool), typeof(DefaultNavigationBar), false, propertyChanged: TogglePropertyChanged);

        //***
        
		private static void IsTogglePropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (DefaultNavigationBar)bindable;
			if(control != null)
			{
				control.IsToggleChanged((bool) newValue);
			}
		}
       
        private void IsToggleChanged(bool newValue)
		{
			IsToggle = newValue;
			ToggleChanged();
		}

		private static void TogglePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (DefaultNavigationBar)bindable;
            if (control != null)
            {
                control.ToggleChanged((bool)newValue);
            }
        }
      
        private void ToggleChanged(bool newValue)
		{
			Toggle = newValue;
			ToggleChanged();
		}

        private void ToggleChanged()
		{
			if(!IsToggle)
			{
				RightIconImg.Source = RightIcon;
			}
			else
			{
				if(Toggle)
				{
					RightIconImg.Source = TrueValueImg;
				}
				else
				{
					RightIconImg.Source = FalseValueImg;
				}
			}
		}
    }
}
