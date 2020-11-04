using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.EmptyStateScreens
{
    public partial class NoTastedChampagnes : ContentView
    {
        public delegate void FindChampagneClicked(object sender, System.EventArgs e);
        public event FindChampagneClicked OnFindChampagneClicked;

        public NoTastedChampagnes()
        {
            InitializeComponent();
        }

        void Handle_OnFindChampagneClicked(object sender, System.EventArgs e)
        {
            OnFindChampagneClicked(sender, e);
        }
    }
}
