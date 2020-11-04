using System;
using CM.ChampagneApp.Business.Configuration;
namespace CM.ChampagneApp.UI.UIFacade.Models.UIUser
{
	public class UIUserCellarChampagneModel : BaseUIModel
    {      
		public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid TastingId { get; set; }
        public Guid ChampagneId { get; set; }
        public Guid BrandId { get; set; }

        public string BrandName { get; set; }
        public string BottleName { get; set; }
        public Guid BottleImgId { get; set; }
        public bool IsVintage { get; set; }
		public int Year { get; set; }

        public string Dosage { get; set; }
        public double PersonalRating { get; set; }
		public int NumberOfTastings { get; set; }

        public bool IsFlipped { get; set; }

        public bool IsSavedModel { get; set; }

        public string BottleImgUrl => new Uri(Configuration.BlobBrandStorageUrl, $"{BrandId}/images/{BottleImgId}/small.png").ToString();

		public string PersonalRatingString => $"{PersonalRating.ToString("0.0")}";

        public string VintageInfo
		{
            get
			{
                return IsVintage ? $"Vintage - {Year}" : $"Non-Vintage";
            }
		}

        public string NumberOfTastingsStringRepresentation
		{
            get
			{
                return IsSavedModel ? $"{NumberOfTastings}" : null;
            }
        }
    }
}
