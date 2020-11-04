using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Lists.ListTemplates
{
    public partial class BrandSearchModelTemplate : ContentView
    {
        public static BindableProperty ImageUrlProperty =
            BindableProperty.Create(nameof(ImageUrl), typeof(string), typeof(BrandSearchModelTemplate));

        public static BindableProperty NameProperty =
            BindableProperty.Create(nameof(Name), typeof(string), typeof(BrandSearchModelTemplate));

        public BrandSearchModelTemplate()
        {
            InitializeComponent();
        }

        public string ImageUrl
        {
            get { return (string)GetValue(ImageUrlProperty); }
            set { SetValue(ImageUrlProperty, value); }
        }

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

    }
}
