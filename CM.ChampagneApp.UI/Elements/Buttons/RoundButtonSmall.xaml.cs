using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Buttons
{
    public partial class RoundButtonSmall : ContentView
    {

		public delegate void Clicked(object sender, System.EventArgs e);
        public event Clicked OnClicked;

        public RoundButtonSmall()
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

        public bool HasShadow
        {
            get { return (bool)GetValue(HasShadowProperty); }
            set { SetValue(HasShadowProperty, value); }
        }

        public static BindableProperty HasShadowProperty =
            BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(RoundButtomMedium), false);

  
    }
}
