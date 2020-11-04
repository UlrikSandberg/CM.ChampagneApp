using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Assets
{
    public partial class CircleImage : ContentView
    {
        public static BindableProperty SourceProperty =
            BindableProperty.Create(nameof(Source), typeof(string), typeof(CircleImage), propertyChanged: DiameterChanged);

        public static BindableProperty ShadowProperty =
            BindableProperty.Create(nameof(Shadow), typeof(bool), typeof(CircleImage), false);

        public static BindableProperty PlaceholderImageProperty =
            BindableProperty.Create(nameof(PlaceholderImageProperty), typeof(string), typeof(CircleImage));

		public CircleImage()
        {
            InitializeComponent();
        }


        public bool Shadow
        {
            get { return (bool)GetValue(ShadowProperty); }
            set { SetValue(ShadowProperty, value); }
        }

        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
			set { SetValue(SourceProperty, value); }
        }

        public string PlaceholderImage
        {
            get { return (string)GetValue(PlaceholderImageProperty); }
            set { SetValue(PlaceholderImageProperty, value); }
        }

        public double Diameter
        {
            get { return (double)GetValue(DiameterProperty); }
            set { SetValue(DiameterProperty, value); }
        }

        public static BindableProperty DiameterProperty =
            BindableProperty.Create(nameof(Diameter), typeof(double), typeof(CircleImageButton), 0.0, propertyChanged: DiameterChanged);

        private static void DiameterChanged(BindableObject bindable, object newValue, object oldValue)
        {
            var Control = (CircleImage)bindable;

            if(Control != null)
            {

				Control.SetCornerRadius();
            }
        }

        private void SetCornerRadius()
        {
            if (Diameter > 0)
            {
				Container.CornerRadius = (int)Diameter;
            }
        }
    }
}
