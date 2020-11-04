using System;
using CM.ChampagneApp.Business.Configuration;
namespace CM.ChampagneApp.UI.UIFacade.Models.UITastingModels
{
	public class UIEditTastingModel : BaseUIModel
    {      
		public Guid Id { get; set; }
        public string Review { get; set; }
        public double Rating { get; set; }
        
		public bool IsTastingNull { get; set; }

		public Guid BrandId { get; set; }
        public Guid ChampagneId { get; set; }
		public string BottleName { get; set; }
		public string BrandName { get; set; }
		public Guid ChampagneBottleImgId { get; set; }
		public Guid ChampagneCoverImgId { get; set; }
              

        public string ChampagneBottleImgUrl
		{
            get
            {
	            return new Uri(Configuration.BlobBrandStorageUrl, $"{BrandId}/images/{ChampagneBottleImgId}/large.png")
		            .ToString();
            }
		}

        public string ChampagneCoverImgUrl
		{
			get
			{
				return new Uri(Configuration.BlobBrandStorageUrl, $"{BrandId}/images/{ChampagneCoverImgId}/large.jpg")
					.ToString();
            }
		}
    }
}
