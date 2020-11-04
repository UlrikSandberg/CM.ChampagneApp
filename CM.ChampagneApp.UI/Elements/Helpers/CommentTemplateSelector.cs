using System;
using Xamarin.Forms;
using CM.ChampagneApp.UI.UIFacade.Models.UICommentModels;
namespace CM.ChampagneApp.UI.Elements.Helpers
{
	public class CommentTemplateSelector : DataTemplateSelector
    {
		public DataTemplate DefaultCommentViewCell { get; set; }

		public DataTemplate EditingCommentViewCell { get; set; }

        public CommentTemplateSelector()
        {
        }

		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			var model = container.BindingContext;

			var binding = item as UICommentModel;

			if(binding == null)
			{
				return null;
			}

			if(binding.IsRequesterAuthor)
			{
				return EditingCommentViewCell;
			}
			else
			{
				return DefaultCommentViewCell;
			}
		}
	}
}
