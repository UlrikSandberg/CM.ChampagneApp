using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;

namespace CM.ChampagneApp.UI.Elements.Buttons
{
    public partial class BorderButtonFullScreen : ContentView
    {
        public BorderButtonFullScreen()
        {
            InitializeComponent();
        }

		public string Text
        {
            get { return (String)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static BindableProperty TextProperty =
			BindableProperty.Create(nameof(Text), typeof(string), typeof(BorderButtonFullScreen));

        public Color Background
        {
            get { return (Color)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public static BindableProperty BackgroundProperty =
			BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(BorderButtonFullScreen), Color.FromHex("#665A4D"));

		public ICommand ClickedCommand
        {
            get { return (ICommand)GetValue(ClickedCommandProperty); }
            set { SetValue(ClickedCommandProperty, value); }
        }

        public static BindableProperty ClickedCommandProperty =
			BindableProperty.Create(nameof(ClickedCommand), typeof(ICommand), typeof(BorderButtonFullScreen));      
    }
}
