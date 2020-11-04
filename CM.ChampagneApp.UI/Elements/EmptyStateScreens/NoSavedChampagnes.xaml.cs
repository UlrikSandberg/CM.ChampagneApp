using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.EmptyStateScreens
{
    public partial class NoSavedChampagnes : ContentView
    {
        public delegate void EmptyStateClicked(object sender, System.EventArgs e);
        public event EmptyStateClicked OnEmptyStateClicked;

        public static BindableProperty ClickedCommandProperty =
            BindableProperty.Create(nameof(ClickedCommand), typeof(ICommand), typeof(NoSavedChampagnes));

        public NoSavedChampagnes()
        {
            InitializeComponent();
        }

        void Handle_OnClicked(object sender, System.EventArgs e)
        {
			if(OnEmptyStateClicked != null)
			{
				OnEmptyStateClicked(sender, e);
			}
            if(ClickedCommand != null)
            {
                if(ClickedCommand.CanExecute(null))
                {
                    ClickedCommand.Execute(null);
                }
            }
        }

		public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public ICommand ClickedCommand
        {
            get { return (ICommand)GetValue(ClickedCommandProperty); }
            set { SetValue(ClickedCommandProperty, value); }
        }

        public static BindableProperty TitleProperty =
			BindableProperty.Create(nameof(Title), typeof(string), typeof(NoSavedChampagnes));


        public string Body
        {
            get { return (string)GetValue(BodyProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static BindableProperty BodyProperty =
			BindableProperty.Create(nameof(Body), typeof(string), typeof(NoSavedChampagnes));

        public string ButtonText
        {
            get { return (string)GetValue(ButtonTextProperty); }
            set { SetValue(ButtonTextProperty, value); }
        }

        public static BindableProperty ButtonTextProperty =
			BindableProperty.Create(nameof(ButtonText), typeof(string), typeof(NoSavedChampagnes));
        
        public bool IsButtonVisible
        {
            get { return (bool)GetValue(IsButtonVisibleProperty); }
            set { SetValue(IsButtonVisibleProperty, value); }
        }

        public static BindableProperty IsButtonVisibleProperty =
			BindableProperty.Create(nameof(IsButtonVisible), typeof(bool), typeof(NoSavedChampagnes), true);

        public bool IsIconVisible
		{
			get { return (bool)GetValue(IsIconVisibleProperty); }
			set { SetValue(IsIconVisibleProperty, value); }
		}

		public static BindableProperty IsIconVisibleProperty =
			BindableProperty.Create(nameof(IsIconVisible), typeof(bool), typeof(NoSavedChampagnes), true);

		public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static BindableProperty IconProperty =
			BindableProperty.Create(nameof(Icon), typeof(string), typeof(NoSavedChampagnes));

        public bool IsBackgroundVisible
        {
            get { return (bool)GetValue(IsBackgroundVisibleProperty); }
            set { SetValue(IsBackgroundVisibleProperty, value); }
        }

        public static BindableProperty IsBackgroundVisibleProperty =
            BindableProperty.Create(nameof(IsBackgroundVisible), typeof(bool), typeof(NoSavedChampagnes));

    }
}
