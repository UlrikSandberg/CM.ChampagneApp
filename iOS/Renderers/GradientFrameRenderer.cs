using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CM.ChampagneApp.iOS.Renderers;
using CoreGraphics;
using CoreAnimation;
using CM.ChampagneApp.UI.Elements;

[assembly: ExportRenderer(typeof(CM.ChampagneApp.UI.Elements.GradientFrame), typeof(GradientFrameRenderer))]
namespace CM.ChampagneApp.iOS.Renderers
{
    public class GradientFrameRenderer : VisualElementRenderer<Frame>
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            var frame = (GradientFrame)this.Element;

            CGColor startColor = frame.StartColor.ToCGColor();
            startColor = ToCGColor(frame.StartColor);
            CGColor endColor = frame.EndColor.ToCGColor();
            endColor = ToCGColor(frame.EndColor);

            var startPoint = frame.StartPoint.ToPointF();
            var endPoint = frame.EndPoint.ToPointF();

            var gradientLayer = new CAGradientLayer()
            {
                StartPoint = startPoint,
                EndPoint = endPoint
            };
            gradientLayer.CornerRadius = frame.CornerRadius;
            gradientLayer.Frame = rect;
            gradientLayer.Colors = new CGColor[] { startColor, endColor };

            NativeView.Layer.InsertSublayer(gradientLayer, 0);
        }

        public static CGColor ToCGColor(Color color)
        {
            return new CGColor(CGColorSpace.CreateSrgb(), new nfloat[] { (float)color.R, (float)color.G, (float)color.B, (float)color.A });
        }

    }
}
