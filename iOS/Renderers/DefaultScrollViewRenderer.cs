using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CM.ChampagneApp.iOS.Renderers;
using CoreGraphics;
using CoreAnimation;
using CM.ChampagneApp.UI.Elements;
using UIKit;
using System.Runtime.CompilerServices;

[assembly: ExportRenderer(typeof(ScrollView), typeof(DefaultScrollViewRenderer))]
namespace CM.ChampagneApp.iOS.Renderers
{
    public class DefaultScrollViewRenderer : ScrollViewRenderer
    {
        
		private IDisposable scrollObserver;
        private UIScrollView iosScrollView;

        private const string OBSERVER_KEY = "contentOffset";

        protected override void Dispose(bool disposing)
        {
            if(scrollObserver != null)
            {
                scrollObserver.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            iosScrollView = (UIScrollView)NativeView;
            iosScrollView.ShowsHorizontalScrollIndicator = false;
			iosScrollView.ShowsVerticalScrollIndicator = false;
                
            if(e.NewElement != null)
            {
                scrollObserver = iosScrollView.AddObserver(OBSERVER_KEY, Foundation.NSKeyValueObservingOptions.New, HandleScrolled);
            }
            if(e.OldElement != null)
            {
                scrollObserver.Dispose();
            }
        }

		private void HandleScrolled(Foundation.NSObservedChange obj)
        {
            iosScrollView = (UIScrollView)NativeView;
			iosScrollView.ShowsHorizontalScrollIndicator = false;
            iosScrollView.ShowsVerticalScrollIndicator = false;
        }
    }
}
