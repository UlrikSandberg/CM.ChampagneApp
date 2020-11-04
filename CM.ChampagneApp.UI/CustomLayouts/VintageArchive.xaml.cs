using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.UI.Elements.Cards;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.CustomLayouts
{
    public partial class VintageArchive : ContentView
    {
        //The height equal the container
        private int cardWidth;

        //The padding factor on bot an top
        private int paddingFactor = 20;

        public delegate void BackgroundClicked(object sender, System.EventArgs e);
        public event BackgroundClicked OnBackgroundClicked;

		public delegate void ChampagneClicked(object sender, ChampagneEditionCardClickedEventArgs args);
        public event ChampagneClicked OnChampagneClicked;

        public static BindableProperty BackgroundClickedCommandProperty =
            BindableProperty.Create(nameof(BackgroundClickedCommand), typeof(ICommand), typeof(VintageArchive));

        public static BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(VintageArchive), "Vintage Archive");

        public static BindableProperty DescriptionProperty =
            BindableProperty.Create(nameof(Description), typeof(string), typeof(VintageArchive), "Choose a vintage");

        public static BindableProperty IsOutOfServiceTextVisibleProperty =
            BindableProperty.Create(nameof(IsOutOfServiceTextVisible), typeof(bool), typeof(VintageArchive), false, propertyChanged: IsOutOfServiceTextVisibleChanged);

        public static BindableProperty IsVisibleWithAnimationProperty =
            BindableProperty.Create(nameof(IsVisibleWithAnimation), typeof(bool), typeof(VintageArchive), false, propertyChanged: IsVisibleChanged);

        public static BindableProperty ItemSourceProperty =
           BindableProperty.Create(nameof(ItemSource), typeof(IEnumerable), typeof(VintageArchive), propertyChanged: ItemSourceChanged);

        public VintageArchive()
        {
            InitializeComponent();

            cardWidth = (App.DisplaySettings.Width / 2);
        }

        public ICommand BackgroundClickedCommand
        {
            get { return (ICommand)GetValue(BackgroundClickedCommandProperty); }
            set { SetValue(BackgroundClickedCommandProperty, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public bool IsOutOfServiceTextVisible
		{
			get { return (bool)GetValue(IsOutOfServiceTextVisibleProperty); }
			set { SetValue(IsOutOfServiceTextVisibleProperty, value); }
		}

        public bool IsVisibleWithAnimation
        {
            get { return (bool)GetValue(IsVisibleWithAnimationProperty); }
            set { SetValue(IsVisibleWithAnimationProperty, value); }
        }

        public IEnumerable ItemSource
        {
            get { return (IEnumerable)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            if(OnBackgroundClicked != null)
            {
                OnBackgroundClicked(sender, e);
            }
            if(BackgroundClickedCommand != null)
            {
                if(BackgroundClickedCommand.CanExecute(null))
                {
                    BackgroundClickedCommand.Execute(null);
                }
            }
        }

        void Handle_OnChampagneClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.ChampagneEditionCardClickedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Vintage archive");
            OnChampagneClicked(sender, args);
        }

        public static void IsVisibleChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (VintageArchive)bindable;

			if(control != null)
			{
				control.OnVisibleChanged((bool)newValue);
			}
		}

        private void OnVisibleChanged(bool isVisible)
		{

			System.Diagnostics.Debug.WriteLine("New value is: " + isVisible);

			if(isVisible)
			{
				vintageArchive.Opacity = 0;
				vintageArchive.IsVisible = true;
				//Indicator.IsVisible = true;
				vintageArchive.FadeTo(1, 200);
			}
			else
			{
				vintageArchive.FadeTo(0, 200);
				vintageArchive.IsVisible = false;
                Archive.Children.Clear();
                LoadingIndicator.IsVisible = true;
			}
		}

        public static void IsOutOfServiceTextVisibleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (VintageArchive)bindable;

            if (control != null)
            {
                control.Indicator.IsVisible = false;
            }
        }

        private static void ItemSourceChanged(BindableObject bindable, object newValue, object oldValue)
        {
            var VintageArchive = (VintageArchive)bindable;
            if(VintageArchive != null)
            {
                VintageArchive.UpdateList();   
            }
        }

        private void UpdateList()
        {
            if (ItemSource != null)
            {
                //LoadingIndicator.IsVisible = true;
                Archive.Children.Clear();
                foreach (Object champagne in ItemSource)
                {
                    LoadingIndicator.IsVisible = false;
                    var Champagne = new ChampagneEditionCard();
                    Champagne.WidthRequest = cardWidth;
                    Champagne.BindingContext = champagne;
                    Champagne.OnChosenChampagne += new ChampagneEditionCard.ChosenChampagne(Handle_OnChampagneClicked);
                    Archive.Children.Add(Champagne);
                }
            }    
        }
    }
}
