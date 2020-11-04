using System;
using CM.ChampagneApp.DTO.Models.Helpers.EnumOptions;
namespace CM.ChampagneApp.DTO.Models.POSTModels.TastingModels
{
    public class CreateCommentRequestModel
    {
		public Guid ContextId { get; set; }

		public CommentContextTypes.ContextTypes ContextType { get; set; }

		public DateTime Date { get; set; }

		public string Comment { get; set; }
    }
}
