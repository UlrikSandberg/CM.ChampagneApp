using System;
using CM.ChampagneApp.UI.UIFacade.Models.UICommentModels;
namespace CM.ChampagneApp.UI.CustomEventArgs
{
    public class UpdateCommentEventArgs
    {
		public UICommentModel Model { get; private set; }
		public string UpdatedComment { get; private set; }

		public UpdateCommentEventArgs(UICommentModel model, string updatedComment)
        {
            UpdatedComment = updatedComment;
			Model = model;
		}
    }
}
