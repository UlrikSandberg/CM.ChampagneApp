using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Headers
{
    public partial class H2 : ContentView
    {
        public H2()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }


        public static BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(H2), propertyChanged: TitleChanged);

        private static void TitleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var Header = (H2)bindable;

            if (Header != null)
            {
                Header.TitleLabel.Text = newValue?.ToString();
            }
        }
    }
}