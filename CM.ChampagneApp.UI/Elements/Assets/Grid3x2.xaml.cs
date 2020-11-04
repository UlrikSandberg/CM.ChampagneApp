using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Assets
{
    public partial class Grid3x2 : ContentView
    {
        public Grid3x2()
        {
            InitializeComponent();
        }

        public string Field2x3
        {
            get { return (string)GetValue(Field2x3Property); }
            set { SetValue(Field2x3Property, value); }
        }

        public static BindableProperty Field2x3Property =
            BindableProperty.Create(nameof(Field2x3), typeof(string), typeof(Grid3x2));

        public string Field2x2
        {
            get { return (string)GetValue(Field2x2Property); }
            set { SetValue(Field2x2Property, value); }
        }

        public static BindableProperty Field2x2Property =
            BindableProperty.Create(nameof(Field2x2), typeof(string), typeof(Grid3x2));
        
        public string Field2x1
        {
            get { return (string)GetValue(Field2x1Property); }
            set { SetValue(Field2x1Property, value); }
        }

        public static BindableProperty Field2x1Property =
            BindableProperty.Create(nameof(Field2x1), typeof(string), typeof(Grid3x2));

        public string Field1x3
        {
            get { return (string)GetValue(Field1x3Property); }
            set { SetValue(Field1x3Property, value); }
        }

        public static BindableProperty Field1x3Property =
            BindableProperty.Create(nameof(Field1x3), typeof(string), typeof(Grid3x2));

        public string Field1x2
        {
            get { return (string)GetValue(Field1x2Property); }
            set { SetValue(Field1x2Property, value); }
        }

        public static BindableProperty Field1x2Property =
            BindableProperty.Create(nameof(Field1x2), typeof(string), typeof(Grid3x2));

        public string Field1x1
        {
            get { return (string)GetValue(Field1x1Property); }
            set { SetValue(Field1x1Property, value); }
        }

        public static BindableProperty Field1x1Property =
            BindableProperty.Create(nameof(Field1x1), typeof(string), typeof(Grid3x2));

    }
}
