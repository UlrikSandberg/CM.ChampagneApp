using System;
using System.Collections.Generic;
namespace CM.ChampagneApp.DTO.Models.GETModels.TastingModels
{
    public class Tasting
    {
        public Tasting()
        {
        }

		public Guid Id { get; set; }

        //Tasting content
		public string Review { get; set; }
		public double Rating { get; set; }

        //Tasting Author
		public Guid AuthorId { get; set; }
		public string AuthorName { get; set; }
		public Guid AuthorProfileImgId { get; set; }

        //Tasting details
		public Guid ChampagneId { get; set; }
		public Guid BrandId { get; set; }
		public DateTime TastedOnDate { get; set; }

        //Social information
		public int NumberOfComments { get; set; }
		public int NumberOfLikes { get; set; }
		public bool IsLikedByRequester { get; set; }
		public bool IsCommentedByRequester { get; set; }
    }
}
