using System;
using System.Collections.Generic;
using CM.ChampagneApp.Business.Configuration;

namespace CM.ChampagneApp.UI.UIFacade.Models.UIChampagneModels
{
	public class UIChampagneRoot : BaseUIModel
    {     
		public Guid Id { get; set; }
        public string FolderName { get; set; }
        public string AuthorName { get; set; }
		public Guid AuthorId { get; set; }
		public Guid DisplayImageId { get; set; }
		public List<Guid> ChampagneIds { get; set; }
        public string FolderContentType { get; set; }
        public string FolderType { get; set; }
        public double AverageRating { get; set; }
        public double SumOfRating { get; set; }
        public double NumberOfTasting { get; set; }
              
        public string BottleImgUrl => new Uri(Configuration.BlobBrandStorageUrl, $"{AuthorId}/images/{DisplayImageId}/small.png").ToString();

		public string AverageRatingString => $"{AverageRating.ToString("0.0")}";

		public int NumberOfTastings => (int)NumberOfTasting;
      
    }
}
