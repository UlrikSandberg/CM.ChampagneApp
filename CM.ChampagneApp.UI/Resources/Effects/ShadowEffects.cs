using System;
using System.Linq;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Resources.Effects
{
    public static class ShadowEffects
    {
        private static void OnOpacityChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as View;
            if (view == null)
                return;

            var opacity = (float)newValue;
            if (opacity > 0)
            {
                view.Effects.Add(new ShadowEffect());
            }
            else
            {
                var toRemove = view.Effects.FirstOrDefault(e => e is ShadowEffect);
                if (toRemove != null)
                    view.Effects.Remove(toRemove);
            }
        }

        public static readonly BindableProperty OpacityProperty = BindableProperty.CreateAttached("Opacity", typeof(float), typeof(ShadowEffects), 0f, propertyChanged: OnOpacityChanged);

        public static readonly BindableProperty BlurRadiusProperty = BindableProperty.CreateAttached("BlurRadius", typeof(double), typeof(ShadowEffects), 3d);

        public static readonly BindableProperty ColorProperty = BindableProperty.CreateAttached("Color", typeof(Color), typeof(ShadowEffects), Color.Default);

        public static readonly BindableProperty DirectionProperty = BindableProperty.CreateAttached("Direction", typeof(string), typeof(ShadowEffects), string.Empty);


        public static void SetDirection(BindableObject view, string direction)
        {
            view.SetValue(DirectionProperty, direction);
        }

        public static string GetDirection(BindableObject view)
        {
            return (string)view.GetValue(DirectionProperty);
        }

        public static void SetOpacity(BindableObject view, float opacity)
        {
            view.SetValue(OpacityProperty, opacity);
        }

        public static float GetOpacity(BindableObject view)
        {
            return (float)view.GetValue(OpacityProperty);
        }

        public static void SetBlurRadius(BindableObject view, double radius)
        {
            view.SetValue(BlurRadiusProperty, radius);
        }

        public static double GetBlurRadius(BindableObject view)
        {
            return (double)view.GetValue(BlurRadiusProperty);
        }

        public static void SetColor(BindableObject view, Color color)
        {
            view.SetValue(ColorProperty, color);
        }

        public static Color GetColor(BindableObject view)
        {
            return (Color)view.GetValue(ColorProperty);
        }

        class ShadowEffect : RoutingEffect
        {
            public ShadowEffect() : base("ChampagneApp.ShadowEffect")
            {

            }
        }
    }
}
