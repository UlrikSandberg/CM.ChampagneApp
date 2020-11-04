using System;
using CM.ChampagneApp.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ViewCell), typeof(TransparentViewCellRenderer))]
namespace CM.ChampagneApp.iOS.Renderers
{
    public class TransparentViewCellRenderer : ViewCellRenderer
    {
        public override UIKit.UITableViewCell GetCell(Xamarin.Forms.Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            cell.SelectionStyle = UIKit.UITableViewCellSelectionStyle.None;

            if(cell !=  null)
            {
                cell.BackgroundColor = UIColor.Clear;
            }

            return cell;
        }
    }
}