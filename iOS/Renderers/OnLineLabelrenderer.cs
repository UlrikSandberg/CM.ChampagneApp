using System;
using CM.ChampagneApp.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CM.ChampagneApp.UI.Elements.Typography.OneLineLabel), typeof(OneLineLabelRenderer))]
namespace CM.ChampagneApp.iOS.Renderers
{
	public class OneLineLabelRenderer : LabelRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			var label = Control as UILabel;

			if(label != null)
			{
				label.AdjustsFontSizeToFitWidth = true;
				label.Lines = 1;
				label.BaselineAdjustment = UIBaselineAdjustment.AlignCenters;
				label.LineBreakMode = UILineBreakMode.Clip;
			}
		}
	}
}

