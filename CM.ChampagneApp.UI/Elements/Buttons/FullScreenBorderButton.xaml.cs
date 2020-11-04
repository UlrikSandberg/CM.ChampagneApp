using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Buttons
{
    public partial class FullScreenBorderButton : ContentView
    {
        public FullScreenBorderButton()
        {
            InitializeComponent();
        }

		public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public static BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(FullScreenButton), Color.Transparent);

        public Color FillColor
        {
			get { return (Color)GetValue(FillColorProperty); }
			set { SetValue(FillColorProperty, value); }
        }

		public static BindableProperty FillColorProperty =
			BindableProperty.Create(nameof(FillColor), typeof(Color), typeof(FullScreenButton), Color.Transparent);
      
        public string LeftText
        {
            get { return (string)GetValue(LeftTextProperty); }
            set { SetValue(LeftTextProperty, value); }
        }

        public static BindableProperty LeftTextProperty =
            BindableProperty.Create(nameof(LeftText), typeof(string), typeof(FullScreenButton));

        public string RightText
        {
            get { return (string)GetValue(RightTextProperty); }
            set { SetValue(RightTextProperty, value); }
        }

        public static BindableProperty RightTextProperty =
            BindableProperty.Create(nameof(RightText), typeof(string), typeof(FullScreenButton));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(FullScreenButton));
    }
}
