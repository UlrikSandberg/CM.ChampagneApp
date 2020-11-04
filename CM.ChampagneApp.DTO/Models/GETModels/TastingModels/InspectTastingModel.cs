using System;
using System.Collections.Generic;
using CM.ChampagneApp.DTO.Models.GETModels.CommentModels;

namespace CM.ChampagneApp.DTO.Models.GETModels.TastingModels
{
    public class InspectTastingModel
    {

		//TastingData
        public Guid Id { get; set; }

        public string Review { get; set; }
        public double Rating { get; set; }

        //Tasting author
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public Guid AuthorProfileImgId { get; set; }
        public DateTime TastedOnDate { get; set; }

        //Tasting Details
        public Guid ChampagneId { get; set; }
        public string BottleName { get; set; }

        public Guid BrandId { get; set; }
        public string BrandName { get; set; }

        //Social information
        public int NumberOfComments { get; set; }
        public int NumberOfLikes { get; set; }

        public bool IsLikedByRequester { get; set; }
        public bool IsCommentedByRequester { get; set; }
		public bool IsBookmarkedByRequester { get; set; }
        public bool IsTastedByRequester { get; set; }

        //Comments!
        public IEnumerable<CommentModel> Comments { get; set; }

        public InspectTastingModel()
        {
        }
    }
}
