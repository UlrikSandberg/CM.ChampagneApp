using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Typography
{
    public partial class TextField : ContentView
    {
        public TextField()
        {
            InitializeComponent();
        }

        public string TextContent
        {
            get { return (string)GetValue(TextContentProperty); }
            set { SetValue(TextContentProperty, value); }
        }

        public static BindableProperty TextContentProperty =
            BindableProperty.Create(nameof(TextContent), typeof(string), typeof(TextField));


    }
}
