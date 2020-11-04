using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CM.ChampagneApp.iOS.Renderers;
using CoreGraphics;
using CoreAnimation;
using CM.ChampagneApp.UI.Elements;
using CM.ChampagneApp.UI.Pages;
using UIKit;
using System.ComponentModel;
using Foundation;
using System.Runtime.CompilerServices;
using CM.ChampagneApp.UI.Elements.Lists;

[assembly: ExportRenderer(typeof(ListView), typeof(GroupListRenderer))]
namespace CM.ChampagneApp.iOS.Renderers
{
    public class GroupListRenderer : ListViewRenderer
    {  
		private IDisposable scrollObserver;

        private const string OBSERVER_KEY = "contentOffset";

        public GroupListRenderer()
        {
            //Subscribe to messageCenter --> Scroll to Top
        }

        protected override void Dispose(bool disposing)
        {

            //Unsubscribe messageCenter --> Scroll to Top

            if(scrollObserver != null)
            {
                scrollObserver.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
		{
            base.OnElementChanged(e);

            if(this.Control != null)
            {
                var footerFrame = new CGRect(0, 0, Control.Frame.Size.Width, 1);
                var view = new UIView(footerFrame);

                this.Control.TableFooterView = view;
            }

            Control.SeparatorColor = Color.FromHex("#A68F68").ToUIColor();
            Control.SizeToFit();

            if(e.NewElement != null)
            {
                scrollObserver = Control.AddObserver(OBSERVER_KEY, Foundation.NSKeyValueObservingOptions.New, HandleScrolled);
            }
            if(e.OldElement != null)
            {
                scrollObserver.Dispose();
            }
        }

		private void HandleScrolled(Foundation.NSObservedChange obj)
		{         
			var myList = Element as ListviewWithScrollEvent;
			Control.ContentInset = new UIEdgeInsets(0, 0, 0, 0);
		}
      
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{

			if (e.PropertyName == ListView.ItemsSourceProperty.PropertyName)
			{
                UITableViewRowAnimation animation = UITableViewRowAnimation.Fade;
				if (Control.NumberOfSections() > 0)
					Control.ReloadSections(NSIndexSet.FromNSRange(new NSRange(0, Control.NumberOfSections())), animation);
			}
			base.OnElementPropertyChanged(sender, e);

			Control.ShowsHorizontalScrollIndicator = false;
			Control.ShowsVerticalScrollIndicator = false;
		}
	}
}













