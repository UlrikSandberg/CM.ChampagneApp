using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;

namespace CM.ChampagneApp.UI.Elements.Buttons
{
    public partial class FullScreenButton : ContentView
    {
        public FullScreenButton()
        {
            InitializeComponent();
        }

		public Color StartColor
        {
            get { return (Color)GetValue(StartColorProperty); }
            set { SetValue(StartColorProperty, value); }
        }

        public static BindableProperty StartColorProperty =
            BindableProperty.Create(nameof(StartColor), typeof(Color), typeof(FullScreenButton), Color.Transparent);

		public Color EndColor
        {
            get { return (Color)GetValue(EndColorProperty); }
            set { SetValue(EndColorProperty, value); }
        }

        public static BindableProperty EndColorProperty =
            BindableProperty.Create(nameof(EndColor), typeof(Color), typeof(FullScreenButton), Color.Transparent);
      
		public double StartPoint
        {
			get { return (double)GetValue(StartPointProperty); }
			set { SetValue(StartPointProperty, value); }
        }

		public static BindableProperty StartPointProperty =
			BindableProperty.Create(nameof(StartPoint), typeof(double), typeof(FullScreenButton), 0.0);

		public double EndPoint
        {
            get { return (double)GetValue(EndPointProperty); }
            set { SetValue(EndPointProperty, value); }
        }

        public static BindableProperty EndPointProperty =
            BindableProperty.Create(nameof(EndPoint), typeof(double), typeof(FullScreenButton), 0.0);

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
