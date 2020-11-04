using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Buttons
{
    public partial class BorderButton : ContentView
    {

        public delegate void Clicked(object sender, System.EventArgs e);
        public event Clicked OnClicked;

        public BorderButton()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (OnClicked != null)
            {
                OnClicked(sender, e);
            }
        }

        public string Text
        {
            get { return (String)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(RoundButtomMedium));

        public Color Background
        {
            get { return (Color)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        public static BindableProperty BackgroundProperty =
            BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(RoundButtomMedium), Color.FromHex("#665A4D"));

    }
}
