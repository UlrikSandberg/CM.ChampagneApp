using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CM.ChampagneApp.iOS.Renderers;
using CoreGraphics;
using CoreAnimation;
using CM.ChampagneApp.UI.Elements;
using CM.ChampagneApp.UI.Pages;
using UIKit;

namespace CM.ChampagneApp.iOS.Renderers
{
    public class CustomTabPageRenderer : TabbedRenderer
    {
        public CustomTabPageRenderer()
        {
        }

		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
            base.OnElementChanged(e);


		}

	}
}
