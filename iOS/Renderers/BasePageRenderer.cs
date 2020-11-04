using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CM.ChampagneApp.iOS.Renderers;
using CoreGraphics;
using CoreAnimation;
using CM.ChampagneApp.UI.Elements;
using CM.ChampagneApp.UI.Pages;
using UIKit;

[assembly: ExportRenderer(typeof(ContentPage), typeof(BasePageRenderer))]
namespace CM.ChampagneApp.iOS.Renderers
{
    public class BasePageRenderer : PageRenderer, IUIGestureRecognizerDelegate
    {
        public BasePageRenderer()
        {
        }

		public override void ViewWillAppear(bool animated)
        {
            
            base.ViewWillAppear(animated);
			this.AutomaticallyAdjustsScrollViewInsets = false;

            if (NavigationController != null)
            {
                ViewController.NavigationController.NavigationBarHidden = true;
                ViewController.NavigationController.InteractivePopGestureRecognizer.Enabled = true;
                ViewController.NavigationController.InteractivePopGestureRecognizer.Delegate = new UIGestureRecognizerDelegate();

            }
        }      
    }
}
