using System;
using CM.ChampagneApp.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(CM.ChampagneApp.UI.Elements.Typography.CommentEditor), typeof(CustomChatEditorRenderer))]
namespace CM.ChampagneApp.iOS.Renderers
{
	public class CustomChatEditorRenderer : EditorRenderer
    {
        public CustomChatEditorRenderer()
        {
        }

		protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
		{
			base.OnElementChanged(e);

			this.Control.InputAccessoryView = null;
			this.Control.KeyboardAppearance = UIKeyboardAppearance.Dark;
			this.Control.ReturnKeyType = UIReturnKeyType.Default;

			this.Control.Layer.BorderColor = ColorFrameRenderer.ToCGColor(Color.FromHex("#A68F68"));
			this.Control.Layer.BackgroundColor = ColorFrameRenderer.ToCGColor(Color.FromHex("#A68f68"));
			this.Control.Layer.BorderWidth = 1;
			this.Control.Layer.CornerRadius = 12;
          
		}
	}
}
