using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Cards
{
    public partial class PushNotificationCard : ContentView
    {
        public PushNotificationCard()
        {
            InitializeComponent();
        }

        public bool IsPlaceholderTextVisible
		{
			get { return (bool)GetValue(IsPlaceholderTextVisibleProperty); }
			set { SetValue(IsPlaceholderTextVisibleProperty, value); }
		}

		public static BindableProperty IsPlaceholderTextVisibleProperty =
			BindableProperty.Create(nameof(IsPlaceholderTextVisible), typeof(bool), typeof(PushNotificationCard), false);

        public string Image
		{
			get { return (string)GetValue(ImageProperty); }
			set { SetValue(ImageProperty, value); }
		}

		public static BindableProperty ImageProperty =
			BindableProperty.Create(nameof(Image), typeof(string), typeof(PushNotificationCard));

        public string Title
		{
			get { return (string)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}

		public static BindableProperty TitleProperty =
			BindableProperty.Create(nameof(Title), typeof(string), typeof(PushNotificationCard));

        public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public static BindableProperty TextProperty =
			BindableProperty.Create(nameof(Text), typeof(string), typeof(PushNotificationCard));
    }
}
