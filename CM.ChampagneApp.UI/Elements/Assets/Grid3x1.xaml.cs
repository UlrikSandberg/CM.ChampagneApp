using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Assets
{
    public partial class Grid3x1 : ContentView
    {

        public delegate void ChosenField1x1(object sender, System.EventArgs e);
        public event ChosenField1x1 OnChosenField1x1;

        public delegate void ChosenField1x2(object sender, System.EventArgs e);
        public event ChosenField1x2 OnChosenField1x2;

        public delegate void ChosenField1x3(object sender, System.EventArgs e);
        public event ChosenField1x3 OnChosenField1x3;

        public Grid3x1()
        {
            InitializeComponent();
        }

        void Handle_ClickedField1x1(object sender, System.EventArgs e)
        {
            if(OnChosenField1x1 != null)
            {
                OnChosenField1x1(sender, e);
            }
        }

        void Handle_ClickedField1x2(object sender, System.EventArgs e)
        {
            if(OnChosenField1x2 != null)
            {
                OnChosenField1x2(sender, e);
            }
        }

        void Handle_ClickedField1x3(object sender, System.EventArgs e)
        {
            if(OnChosenField1x3 != null)
            {
                OnChosenField1x3(sender, e);
            }
        }

        public string Field1x3Header
        {
            get { return (string)GetValue(Field1x3HeaderProperty); }
            set { SetValue(Field1x3HeaderProperty, value); }
        }

        public static BindableProperty Field1x3HeaderProperty =
            BindableProperty.Create(nameof(Field1x3Header), typeof(string), typeof(Grid3x1));

        public string Field1x3
        {
            get { return (string)GetValue(Field1x3Property); }
            set { SetValue(Field1x3Property, value); }
        }

        public static BindableProperty Field1x3Property =
            BindableProperty.Create(nameof(Field1x3), typeof(string), typeof(Grid3x1));

        public string Field1x2Header
        {
            get { return (string)GetValue(Field1x2HeaderProperty); }
            set { SetValue(Field1x2HeaderProperty, value); }
        }

        public static BindableProperty Field1x2HeaderProperty =
            BindableProperty.Create(nameof(Field1x2Header), typeof(string), typeof(Grid3x1));

        public string Field1x2
        {
            get { return (string)GetValue(Field1x2Property); }
            set { SetValue(Field1x2Property, value); }
        }

        public static BindableProperty Field1x2Property =
            BindableProperty.Create(nameof(Field1x2), typeof(string), typeof(Grid3x1));

        public string Field1x1Header
        {
            get { return (string)GetValue(Field1x1HeaderProperty); }
            set { SetValue(Field1x1HeaderProperty, value); }
        }

        public static BindableProperty Field1x1HeaderProperty =
            BindableProperty.Create(nameof(Field1x1Header), typeof(string), typeof(Grid3x1));

        public string Field1x1
        {
            get { return (string)GetValue(Field1x1Property); }
            set { SetValue(Field1x1Property, value); }
        }

        public static BindableProperty Field1x1Property =
            BindableProperty.Create(nameof(Field1x1), typeof(string), typeof(Grid3x1));

    }
}
