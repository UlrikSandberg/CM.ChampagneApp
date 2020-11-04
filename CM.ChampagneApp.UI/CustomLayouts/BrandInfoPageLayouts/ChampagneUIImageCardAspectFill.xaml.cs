using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.CustomLayouts.BrandInfoPageLayouts
{
    public partial class ChampagneUIImageCardAspectFill : ContentView
    {
        public ChampagneUIImageCardAspectFill()
        {
            InitializeComponent();
        }

		public string Source
        {
            get { return(string)GetValue(SourceProperty);}
            set { SetValue(SourceProperty, value); }
        }

        public static BindableProperty SourceProperty =
            BindableProperty.Create(nameof(Source), typeof(string), typeof(ChampagneUIImageCard));

        public int ImageWidth
        {
            get { return (int)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        public static BindableProperty ImageWidthProperty =
            BindableProperty.Create(nameof(ImageWidth), typeof(int), typeof(ChampagneUIImageCard), 0);
    }
}
