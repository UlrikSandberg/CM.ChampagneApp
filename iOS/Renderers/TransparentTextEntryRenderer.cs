using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.iOS.Renderers;
using CM.ChampagneApp.UI.Elements.Typography;
using CoreGraphics;
using CoreAnimation;
using CM.ChampagneApp.UI.Elements.Buttons;
using UIKit;

[assembly: ExportRenderer(typeof(CM.ChampagneApp.UI.Elements.Typography.TransparentTextEntry),typeof(TransparentTextEntryRenderer))]

namespace CM.ChampagneApp.iOS.Renderers
{
    public class TransparentTextEntryRenderer : EntryRenderer 
    {
        public TransparentTextEntryRenderer()
        {
        }

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
            base.OnElementChanged(e);

            if(Control != null)
            {
                System.Diagnostics.Debug.WriteLine("Should apply custom renderer");
                Control.BorderStyle = UITextBorderStyle.None;
				this.Control.InputAccessoryView = null;
                this.Control.KeyboardAppearance = UIKeyboardAppearance.Dark;
                this.Control.ReturnKeyType = UIReturnKeyType.Default;
            }
		}


	}
}
