using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Buttons
{
    public partial class ButtonSwitch : ContentView
    {

        public delegate void ChosenLeft(object sender, System.EventArgs e);
        public event ChosenLeft OnChosenLeft;

        public delegate void ChosenRight(object sender, System.EventArgs e);
        public event ChosenRight OnChosenRight;

        public bool switchState { get; private set; }

        public ButtonSwitch()
        {
            InitializeComponent();
            switchState = false;
			//ButtonView.HeightRequest = 50;
        }

        void Handle_ClickedLeft(object sender, System.EventArgs e)
        {
            GoldIndicator.TranslateTo(0, 0, 250, null);
            switchState = false;
			GoldIndicator.IsVisible = true;
			if (OnChosenLeft != null)
			{
				OnChosenLeft(sender, e);
			}
        }

        void Handle_ClickedRight(object sender, System.EventArgs e)
        {
            GoldIndicator.TranslateTo((ContentGrid.Width - GoldIndicator.Width + 20), 0, 250, null);
            switchState = true;
			if (OnChosenRight != null)
			{
				OnChosenRight(sender, e);
			}
        }

       
        public string LeftValue
        {
            get { return (string)GetValue(LeftValueProperty); }
            set { SetValue(LeftValueProperty, value); }
        }


        public static BindableProperty LeftValueProperty =
            BindableProperty.Create(nameof(LeftValue), typeof(string), typeof(ButtonSwitch));

        public string LeftTitle
        {
            get { return (string)GetValue(LeftTitleProperty); }
            set { SetValue(LeftTitleProperty, value); }
        }

        public static BindableProperty LeftTitleProperty =
            BindableProperty.Create(nameof(LeftTitle), typeof(string), typeof(ButtonSwitch));


        public string RightValue
        {
            get { return (string)GetValue(RightValueProperty); }
            set { SetValue(RightValueProperty, value); }
        }


        public static BindableProperty RightValueProperty =
            BindableProperty.Create(nameof(RightValue), typeof(string), typeof(ButtonSwitch));

        public string RightTitle
        {
            get { return (string)GetValue(RightTitleProperty); }
            set { SetValue(RightTitleProperty, value); }
        }


        public static BindableProperty RightTitleProperty =
            BindableProperty.Create(nameof(RightTitle), typeof(string), typeof(ButtonSwitch));


    }
}
