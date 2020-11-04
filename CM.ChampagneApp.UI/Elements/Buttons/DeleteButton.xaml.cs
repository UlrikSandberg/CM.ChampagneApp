using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Buttons
{
    public partial class DeleteButton : ContentView
    {
        public delegate void Clicked(object sender, System.EventArgs e);
        public event Clicked OnClicked;

        public DeleteButton()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            if(OnClicked != null)
            {
                OnClicked(sender, e);
            }
        }
    }
}
