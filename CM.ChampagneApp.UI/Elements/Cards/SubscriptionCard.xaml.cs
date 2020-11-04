using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;

namespace CM.ChampagneApp.UI.Elements.Cards
{
    public partial class SubscriptionCard : ContentView
    {      
        public SubscriptionCard()
        {
            InitializeComponent();
        }


        public ICommand ClickedCommand
		{
			get { return (ICommand)GetValue(ClickedCommandProperty); }
			set { SetValue(ClickedCommandProperty, value); }
		}

		public static BindableProperty ClickedCommandProperty =
			BindableProperty.Create(nameof(ClickedCommand), typeof(ICommand), typeof(SubscriptionCard), null);

        public string TotalAmount
		{
			get { return (string)GetValue(TotalAmountProperty); }
			set { SetValue(TotalAmountProperty, value); }
		}

		public static BindableProperty TotalAmountProperty =
			BindableProperty.Create(nameof(TotalAmount), typeof(string), typeof(SubscriptionCard));

        public string WeeklyAmount
		{
			get { return (string)GetValue(WeeklyAmountProperty); }
			set { SetValue(WeeklyAmountProperty, value); }
		}

		public static BindableProperty WeeklyAmountProperty =
			BindableProperty.Create(nameof(WeeklyAmount), typeof(string), typeof(SubscriptionCard));
    }
}
