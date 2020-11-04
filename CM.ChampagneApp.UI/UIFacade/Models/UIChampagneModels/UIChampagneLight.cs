using System;
using CM.ChampagneApp.Business.Configuration;

namespace CM.ChampagneApp.UI.UIFacade.Models.UIChampagneModels
{
	public class UIChampagneLight : BaseUIModel
    {     
		public Guid Id { get; set; }
        public string BottleName { get; set; }
		public string BrandName { get; set; }
        public Guid BrandId { get; set; }
        public Guid BottleImgId { get; set; }
        public bool IsPublished { get; set; }
        public Guid ChampagneRootId { get; set; }
        public double NumberOfTastings { get; set; }
        public double RatingSumOfTastings { get; set; }
        public UIVintageInfo VintageInfo { get; set; }

        public class UIVintageInfo
        {
            public bool isVintage { get; set; }
            public int year { get; set; }
        }

		//Helper attributes

        public string BottleImgUrl => new Uri(Configuration.BlobBrandStorageUrl, $"{BrandId}/images/{BottleImgId}/small.png").ToString();

		public string VintageYear
        {
            get
            {
                if(VintageInfo.year < 1)
                {
                    return "";
                }
                else
                {
                    return VintageInfo.year.ToString();
                }

            }
        }

		public string AverageRating
		{
			get
			{
				if(NumberOfTastings.Equals(0.0) || RatingSumOfTastings.Equals(0.0))
				{
					return "0.0";
				}
				else
				{
					return (RatingSumOfTastings / NumberOfTastings).ToString("0.0");
				}
			}
		}      
    }
}
