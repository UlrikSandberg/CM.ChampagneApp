using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CM.ChampagneApp.iOS.Renderers;
using CoreGraphics;
using CoreAnimation;
using CM.ChampagneApp.UI.Elements;
using CM.ChampagneApp.UI.Pages;
using UIKit;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(NavigationPageRenderer))]
namespace CM.ChampagneApp.iOS.Renderers
{
	public class NavigationPageRenderer : NavigationRenderer
    {
        public NavigationPageRenderer()
        {
        }

		public override void ViewDidLoad()
		{
            base.ViewDidLoad();

			this.AutomaticallyAdjustsScrollViewInsets = false;
           
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			System.Diagnostics.Debug.WriteLine("View will appear navigationPage");

		}

		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e); 
		}
	}
}
