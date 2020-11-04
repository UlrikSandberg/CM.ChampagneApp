using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;

namespace CM.ChampagneApp.UI.Elements.EmptyStateScreens
{
    public partial class OutOfServiceEmptyState : ContentView
    {

		public delegate void TryAgain(object sender, System.EventArgs e);
		public event TryAgain OnTryAgain;

        public OutOfServiceEmptyState()
        {
            InitializeComponent();
        }      

        public ICommand TryAgainCommand
		{
			get { return (ICommand)GetValue(TryAgainCommandProperty); }
			set { SetValue(TryAgainCommandProperty, value); }
		}

		public static BindableProperty TryAgainCommandProperty =
			BindableProperty.Create(nameof(TryAgainCommand), typeof(ICommand), typeof(OutOfServiceEmptyState));

        public bool IsBackgroundVisible
		{
			get { return (bool)GetValue(IsBackgroundVisibleProperty);}
			set { SetValue(IsBackgroundVisibleProperty, value);}
		}

		public static BindableProperty IsBackgroundVisibleProperty =
			BindableProperty.Create(nameof(IsBackgroundVisible), typeof(bool), typeof(OutOfServiceEmptyState), true);
      

		void Handle_OnClicked(object sender, System.EventArgs e)
		{
			if(OnTryAgain != null)
			{
				OnTryAgain(sender, e);
			}
		}
    }
}
