using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using CM.ChampagneApp.iOS.Renderers;

[assembly: ExportRenderer(typeof(CM.ChampagneApp.UI.Elements.Typography.CustomEditor), typeof(TransparentEditorRenderer))]
namespace CM.ChampagneApp.iOS.Renderers
{
	public class TransparentEditorRenderer : EditorRenderer
    {
        public TransparentEditorRenderer()
        {
        }

		protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            this.Control.InputAccessoryView = null;
            this.Control.KeyboardAppearance = UIKeyboardAppearance.Dark;
            this.Control.ReturnKeyType = UIReturnKeyType.Default;

			this.Control.Layer.BorderColor = ColorFrameRenderer.ToCGColor(Color.Transparent);
			this.Control.Layer.BackgroundColor = ColorFrameRenderer.ToCGColor(Color.Transparent);

        }
    }
}
