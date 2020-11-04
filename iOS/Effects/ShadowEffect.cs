using System;
using System.ComponentModel;
using CM.ChampagneApp.UI.Resources.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CoreGraphics;
using System.Linq;

[assembly: ResolutionGroupName("ChampagneApp")]
[assembly: ExportEffect(typeof(CM.ChampagneApp.iOS.Effects.ShadowEffect), "ShadowEffect")]
namespace CM.ChampagneApp.iOS.Effects
{
    public class ShadowEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            UpdateColor();
            UpdateOpacity();
            UpdateDirection();
            UpdateBlurRadius();
        }

        protected override void OnDetached()
        {
            //Container.Layer.ShadowOpacity = 0;
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ShadowEffects.OpacityProperty.PropertyName)
            {
                UpdateOpacity();
            }
            else if (e.PropertyName == ShadowEffects.ColorProperty.PropertyName)
            {
                UpdateColor();
            }
            else if (e.PropertyName == ShadowEffects.BlurRadiusProperty.PropertyName)
            {
                UpdateBlurRadius();
            }
            else if (e.PropertyName == ShadowEffects.DirectionProperty.PropertyName)
            {
                UpdateDirection();
            }
        }

        private void UpdateDirection()
        {
            var direction = ShadowEffects.GetDirection(Element);

            if(!string.IsNullOrEmpty(direction))
            {
                var coords = direction.Split(',').Select(s => float.Parse(s)).ToList();
                Container.Layer.ShadowOffset = new CGSize(coords[0], coords[1]);
            }
        }

        private void UpdateOpacity()
        {
            Container.Layer.ShadowOpacity = ShadowEffects.GetOpacity(Element);
        }

        private void UpdateColor()
        {
            var color = ShadowEffects.GetColor(Element);
            Container.Layer.ShadowColor = color.ToCGColor();
        }

        private void UpdateBlurRadius()
        {
            Container.Layer.ShadowRadius = (nfloat)ShadowEffects.GetBlurRadius(Element);
        }
    }
}
