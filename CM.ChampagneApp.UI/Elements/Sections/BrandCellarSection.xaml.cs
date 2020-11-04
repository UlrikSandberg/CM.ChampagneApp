using System;
using System.Collections.Generic;
using CM.ChampagneApp.UI.CustomEventArgs;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Sections
{
    public partial class BrandCellarSection : ContentView
    {
		public delegate void ChampagneClicked(object sender, ChampagneClickedEventArgs args);
		public event ChampagneClicked OnChampagneClicked;

		public bool IsHeaderHidden = false;

        public BrandCellarSection()
        {
            InitializeComponent();
        }

		void Handle_OnItemClicked(object sender, ChampagneClickedEventArgs args)
		{
			if(OnChampagneClicked != null)
			{
				OnChampagneClicked(sender, args);
			}
		}
    }
}
