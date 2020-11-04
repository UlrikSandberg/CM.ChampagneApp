using System;
using System.Collections.Generic;
using CM.ChampagneApp.Business.Configuration;
using System.ComponentModel;

namespace CM.ChampagneApp.UI.UIFacade.Models.UITastingModels
{
	public class UITasting : BaseUIModel
    {      
		public Guid Id { get; set; }

        //Tasting Content
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
             
        public string ProfileImgUrl
		{
            get
			{
                return !AuthorProfileImgId.Equals(Guid.Empty)
                    ? new Uri(Configuration.BlobUserStorageUrl, $"{AuthorId}/images/{AuthorProfileImgId}/small.jpg").ToString()
                    : "PlaceholderProfileImg.png";
            }
		}

        public string TastingDateStringRepresentation
		{
            get
			{
				var now = DateTime.Now;
                var utc = DateTime.UtcNow;

                var timeZoneBuffer = now.Ticks - utc.Ticks;

                var timeRespectiveToZone = TastedOnDate.Ticks + timeZoneBuffer;

                var dateRespectiveToZone = new DateTime(timeRespectiveToZone);


				return dateRespectiveToZone.ToString("MMMM dd, yyyy");
			}
		}
    }
}
