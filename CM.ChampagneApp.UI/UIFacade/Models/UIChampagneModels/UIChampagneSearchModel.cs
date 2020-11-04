using System;
namespace CM.ChampagneApp.UI.UIFacade.Models.UIChampagneModels
{
    public class UIChampagneSearchModel : BaseUIModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public Guid ImageId { get; set; }
        public Guid BrandId { get; set; }

        //IsVintage
        public bool IsVintage { get; set; }
        public int Year { get; set; }

        //Tastings
        public int NumberOfTastings { get; set; }
        public double AverageRating { get; set; }

        public string BottleImageUrl
        {
            get
            {
                return ImageId != Guid.Empty
                    ? new Uri(Configuration.BlobBrandStorageUrl, $"{BrandId}/images/{ImageId}/small.png").ToString()
                    : "bottle_bubbles";
            }
        }


        public string AverageRatingString
        {
            get
            {
                if(AverageRating == 0.0)
                {
                    return "0.0";
                }
                else
                {
                    return AverageRating.ToString("0.0");
                }
            }
        }

        public string VintageInfo
        {
            get
            {
                if(IsVintage)
                {
                    return $"Vintage - {Year}";
                }
                else
                {
                    return $"Non-Vintage";
                }
            }
        }
    }
}
