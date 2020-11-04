using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CM.ChampagneApp.iOS.Renderers;
using CoreGraphics;
using CoreAnimation;
using CM.ChampagneApp.UI.Elements;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(Frame), typeof(ColorFrameRenderer))]
namespace CM.ChampagneApp.iOS.Renderers
{
    public class ColorFrameRenderer : FrameRenderer
    {
        public ColorFrameRenderer()
        {
        }


        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (Element != null)
            {
                Layer.BackgroundColor = ToCGColor(Element.BackgroundColor);
            }
        }

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
            base.OnElementPropertyChanged(sender, e);
            if (Element != null)
            {
                Layer.BackgroundColor = ToCGColor(Element.BackgroundColor);
            }
		}

		public static CGColor ToCGColor(Color color)
        {
            return new CGColor(CGColorSpace.CreateSrgb(), new nfloat[] { (float)color.R, (float)color.G, (float)color.B, (float)color.A });
        }
    }
}
