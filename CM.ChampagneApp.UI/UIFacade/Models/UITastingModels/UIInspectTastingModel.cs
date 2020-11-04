using System;
using System.Collections.Generic;
using CM.ChampagneApp.Business.Configuration;
using CM.ChampagneApp.UI.UIFacade.Models.UICommentModels;
using System.ComponentModel;

namespace CM.ChampagneApp.UI.UIFacade.Models.UITastingModels
{
	public class UIInspectTastingModel : BaseUIModel
    {   
		//TastingData
        public Guid Id { get; set; }

		public UITasting Tasting { get; set; }

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
		public IEnumerable<UICommentModel> Comments { get; set; }


		public string ProfileImgUrl
        {
            get
            {
                if (!AuthorProfileImgId.Equals(Guid.Empty))
                {
                    return new Uri(Configuration.BlobUserStorageUrl, $"{AuthorId}/images/{AuthorProfileImgId}/small.jpg").ToString();
                }
                else
                {
                    return "PlaceholderProfileImg.png";
                }
            }
        }

		public string TastingDateStringRepresentation
        {
            get
            {
                return TastedOnDate.ToString("MMMM dd, yyyy");
            }
        }
    }
}
