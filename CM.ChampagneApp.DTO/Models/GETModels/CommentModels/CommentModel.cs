using System;
namespace CM.ChampagneApp.DTO.Models.GETModels.CommentModels
{
    public class CommentModel
    {

		public Guid Id { get; set; }

        public Guid ContextId { get; set; }
        public string ContextType { get; set; }

        //
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public Guid AuthorProfileImgId { get; set; }

        //
        public DateTime Date { get; set; }
        public string Comment { get; set; }

        public int NumberOfLikes { get; set; }
        public bool IsLikedByRequester { get; set; }

        public CommentModel()
        {
        }
    }
}
