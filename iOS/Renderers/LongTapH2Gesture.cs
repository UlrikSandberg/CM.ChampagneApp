using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.iOS.Renderers;
using CoreGraphics;
using CoreAnimation;
using CM.ChampagneApp.UI.Elements.Buttons;
using UIKit;
using CM.ChampagneApp.UI.Elements.Headers;
using CM.ChampagneApp.UI.Elements.Cards;

[assembly: ExportRenderer(typeof(CM.ChampagneApp.UI.Elements.Cards.FilterSearchTagCard), typeof(LongSearchFilterTagCardGesture))]
namespace CM.ChampagneApp.iOS.Renderers
{
    public class LongSearchFilterTagCardGesture : ViewRenderer
    {

        UIGestureRecognizer longPressGesture;

        public LongSearchFilterTagCardGesture()
        {
        }

		protected override void OnElementChanged(ElementChangedEventArgs<View> e)
		{
            base.OnElementChanged(e);

            longPressGesture = new UILongPressGestureRecognizer(Handle_iosLongPressGesture);


            if (e.NewElement == null)
            {
                if (longPressGesture != null)
                {
                    this.RemoveGestureRecognizer(longPressGesture);
                }
            }

            if (e.OldElement == null)
            {
                this.AddGestureRecognizer(longPressGesture);
            }
		}

        private void Handle_iosLongPressGesture()
        {

            var myElement = Element as FilterSearchTagCard;

            if(myElement != null)
            {
               


                if(longPressGesture.State == UIGestureRecognizerState.Began)
                {
                    
                    if(myElement.longPressRecognized.CanExecute(null))
                    {
                        myElement.longPressRecognized.Execute(null);
                    }

                    System.Diagnostics.Debug.WriteLine("Long press gesture activated");
                }

                if(longPressGesture.State == UIGestureRecognizerState.Ended)
                {
                    
                    if (myElement.longPressEnded.CanExecute(null))
                    {
                        myElement.longPressEnded.Execute(null);
                    }

                    System.Diagnostics.Debug.WriteLine("Long press gesture ended");
                }



            }
        }
	}
}
