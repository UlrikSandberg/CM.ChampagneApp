using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;

namespace CM.ChampagneApp.UI.Elements.Assets
{
	public partial class SwitchButtonWithDescription : ContentView
	{
		public delegate void Toggle(object sender, Xamarin.Forms.ToggledEventArgs e);
		public event Toggle OnToggle;

		public SwitchButtonWithDescription()
		{
			InitializeComponent();
		}

		void Handle_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
		{
			if (OnToggle != null)
			{
				OnToggle(sender, e);
			}
			if(ToggleCommand != null)
			{
				if(ToggleCommand.CanExecute(e.Value))
				{
					ToggleCommand.Execute(e.Value);
				}
			}
		}

		public ICommand ToggleCommand
		{
			get { return (ICommand)GetValue(ToggleCommandProperty); }
			set { SetValue(ToggleCommandProperty, value); }
		}

		public static BindableProperty ToggleCommandProperty =
			BindableProperty.Create(nameof(ToggleCommand), typeof(ICommand), typeof(SwitchButtonWithDescription));
      
        public bool IsTextToggleVisible
		{
			get { return (bool)GetValue(IsTextToggleVisibleProperty); }
			set { SetValue(IsTextToggleVisibleProperty, value); }
		}

		public static BindableProperty IsTextToggleVisibleProperty =
			BindableProperty.Create(nameof(IsTextToggleVisible), typeof(bool), typeof(SwitchButtonWithDescription), false);

        public bool IsToggled
		{
			get { return (bool)GetValue(IsToggledProperty); }
			set { SetValue(IsToggledProperty, value); }
		}

		public static BindableProperty IsToggledProperty =
			BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(SwitchButtonWithDescription), false);

        public string Description
		{
			get { return (string)GetValue(DescriptionProperty); }
			set { SetValue(DescriptionProperty, value); }
		}

		public static BindableProperty DescriptionProperty =
			BindableProperty.Create(nameof(Description), typeof(string), typeof(SwitchButtonWithDescription));

    }
}
