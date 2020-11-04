using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.EmptyStateScreens
{
	public partial class EmptyStateDefault : ContentView
	{

		public delegate void EmptyStateButtonClicked(object sender, System.EventArgs e);
		public event EmptyStateButtonClicked OnEmptyStateButtonClicked;

		public EmptyStateDefault()
		{
			InitializeComponent();
		}

		void Handle_OnClicked(object sender, System.EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("Empty state clicked in emptyState");
			if (OnEmptyStateButtonClicked != null)
			{
				OnEmptyStateButtonClicked(sender, e);
			}
		}

		public bool IsBackgroundVisible
        {
            get { return (bool)GetValue(IsBackgroundVisibleProperty);}
            set { SetValue(IsBackgroundVisibleProperty, value);}
        }

        public static BindableProperty IsBackgroundVisibleProperty =
            BindableProperty.Create(nameof(IsBackgroundVisible), typeof(bool), typeof(OutOfServiceEmptyState), true);
      

		public string Title
		{
			get { return (string)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}

		public static BindableProperty TitleProperty =
			BindableProperty.Create(nameof(Title), typeof(string), typeof(EmptyStateDefault));


		public string Body
		{
			get { return (string)GetValue(BodyProperty); }
			set { SetValue(TitleProperty, value); }
		}

		public static BindableProperty BodyProperty =
			BindableProperty.Create(nameof(Body), typeof(string), typeof(EmptyStateDefault));

		public string ButtonText
		{
			get { return (string)GetValue(ButtonTextProperty); }
			set { SetValue(ButtonTextProperty, value); }
		}

		public static BindableProperty ButtonTextProperty =
			BindableProperty.Create(nameof(ButtonText), typeof(string), typeof(EmptyStateDefault));

		public bool isButtonVisible
		{
			get { return (bool)GetValue(isButtonVisibleProperty); }
			set { SetValue(isButtonVisibleProperty, value); }
		}

		public static BindableProperty isButtonVisibleProperty =
			BindableProperty.Create(nameof(isButtonVisible), typeof(bool), typeof(EmptyStateDefault), true);

		public bool IsLoading
		{
			get { return (bool)GetValue(IsLoadingProperty); }
			set { SetValue(IsLoadingProperty, value); }
		}

		public static BindableProperty IsLoadingProperty =
			BindableProperty.Create(nameof(IsLoading), typeof(bool), typeof(EmptyStateDefault), false, propertyChanged: IsLoadingPropertyChanged);

		private static void IsLoadingPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var emptyStateDefault = (EmptyStateDefault)bindable;
			System.Diagnostics.Debug.WriteLine("Changed in emptyscreen control");
			if (emptyStateDefault != null)
			{
				System.Diagnostics.Debug.WriteLine((bool)oldValue);
				System.Diagnostics.Debug.WriteLine((bool)newValue);
			}

		}

	}

}
