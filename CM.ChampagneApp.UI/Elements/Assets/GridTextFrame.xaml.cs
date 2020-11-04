using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Assets
{
    public partial class GridTextFrame : ContentView
    {
        public GridTextFrame()
        {
            InitializeComponent();
        }


        public string ContentText
        {
            get { return (string)GetValue(ContentTextProperty); }
            set { SetValue(ContentTextProperty, value); }
        }

        public static BindableProperty ContentTextProperty =
            BindableProperty.Create(nameof(ContentText), typeof(string), typeof(GridTextFrame));


    }
}
