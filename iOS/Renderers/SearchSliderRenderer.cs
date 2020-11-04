using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CM.ChampagneApp.iOS.Renderers;
using CoreGraphics;
using CoreAnimation;
using CM.ChampagneApp.UI.Elements;
using CM.ChampagneApp.UI.Pages;
using UIKit;

[assembly: ExportRenderer(typeof(Slider), typeof(SearchSliderRenderer))]
namespace CM.ChampagneApp.iOS.Renderers
{
    public class SearchSliderRenderer : SliderRenderer
    {
        public SearchSliderRenderer()
        {
        }

		protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
		{
            base.OnElementChanged(e);

            if(Control != null)
            {
                Control.MinimumTrackTintColor = UIColor.Gray;
                Control.MaximumTrackTintColor = Color.FromHex("#A68F68").ToUIColor();
            }
		}
	}
}
