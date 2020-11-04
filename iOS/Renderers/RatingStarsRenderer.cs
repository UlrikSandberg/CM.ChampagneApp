using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.iOS.Renderers;
using CoreGraphics;
using CoreAnimation;
using CM.ChampagneApp.UI.Elements.Buttons;
using UIKit;


[assembly: ExportRenderer(typeof(CM.ChampagneApp.UI.Elements.Buttons.RatingStars), typeof(RatingStarsRenderer))]
namespace CM.ChampagneApp.iOS.Renderers
{
    public class RatingStarsRenderer : ViewRenderer
    {
        UIPanGestureRecognizer panGestureRecognizer;
        UIGestureRecognizer tapGestureRecognizer;

        public RatingStarsRenderer()
        {
        }

		protected override void OnElementChanged(ElementChangedEventArgs<View> e)
		{
            base.OnElementChanged(e);
            panGestureRecognizer = new UIPanGestureRecognizer(Handle_iosPanGesture);
            tapGestureRecognizer = new UITapGestureRecognizer(Handle_TapGesture);

            if(e.NewElement == null)
            {
                if(panGestureRecognizer != null)
                {
                    this.RemoveGestureRecognizer(panGestureRecognizer);
                    this.RemoveGestureRecognizer(tapGestureRecognizer);
                }
            }

            if(e.OldElement == null)
            {
                this.AddGestureRecognizer(panGestureRecognizer);
                this.AddGestureRecognizer(tapGestureRecognizer);
            }
        }

        private void Handle_TapGesture()
        {
            var myElement = Element as RatingStars;
            CGPoint touchLocation = tapGestureRecognizer.LocationInView(this);
            var nativePanGestureArgs = new NativePanGestureEventArgs(touchLocation.X, touchLocation.Y);
            if (myElement != null)
            {
                if(myElement.TapGestureRecognized.CanExecute(nativePanGestureArgs))
                {
                    myElement.TapGestureRecognized.Execute(nativePanGestureArgs);
                }
            }


        }

        private void Handle_iosPanGesture()
        {

            var myElement = Element as RatingStars;
            CGPoint touchLocation = panGestureRecognizer.LocationInView(this);
            var nativePanGestureArgs = new NativePanGestureEventArgs(touchLocation.X, touchLocation.Y);

            if(myElement != null)
            {
                if(myElement.GetNativePanGestureArgs.CanExecute(nativePanGestureArgs))
                {
                    myElement.GetNativePanGestureArgs.Execute(nativePanGestureArgs);
                }
            }

            if (panGestureRecognizer.State == UIGestureRecognizerState.Ended)
            {
                if(myElement.NativePanGestureEnded.CanExecute(nativePanGestureArgs))
                {
                    myElement.NativePanGestureEnded.Execute(nativePanGestureArgs);
                }
            }
        }
	}
}
