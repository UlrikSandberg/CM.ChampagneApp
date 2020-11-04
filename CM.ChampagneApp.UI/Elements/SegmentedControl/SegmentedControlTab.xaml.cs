using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.SegmentedControl
{
    public partial class SegmentedControlTab : ContentView
    {
		public delegate void TabSelected(object sender, SegmentedControlTabEventArgs e);
		public event TabSelected OnTabSelected;

        public SegmentedControlTab()
        {
            InitializeComponent();
        }

		public int Index { get; set; }

		public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(SegmentedControlTab));

		void Handle_Clicked(object sender, System.EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine(Text + " Clicked");
			if(OnTabSelected != null)
			{
				OnTabSelected(sender, new SegmentedControlTabEventArgs(Index));
				System.Diagnostics.Debug.WriteLine(this.Width);
			}
		}
    }
}
