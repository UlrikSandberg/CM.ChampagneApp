using System;
using System.Collections.Generic;

namespace CM.ChampagneApp.DTO.Models
{
    public class ChampagneProfile
    {
		//public List<TechnicalIdentity> TechnicalIdentities { get; set; }
        public ChampagneProfile()
        {
            
        }
        /*
        public string id { get; set; }
        public string bottleName { get; set; }
        public string brandId { get; set; }
        public string bottleImgId { get; set; }
        public bool isPublished { get; set; }
        public string champagneRootId { get; set; }
        public int numberOfTastings { get; set; }
        public int ratingSumOfTastings { get; set; }
        public VintageInfo vintageInfo { get; set; }
        public ChampagneProfile champagneProfile { get; set; }
        public FilterSearchParameters filterSearchParameters { get; set; }

        public class VintageInfo
        {
            public bool isVintage { get; set; }
            public int year { get; set; }
        }

        public class FilterSearchParameters
        {
            public string dosage { get; set; }
            public List<string> style { get; set; }
            public List<string> character { get; set; }
        }

        public class ChampagneProfile
        {
            public string appearance { get; set; }
            public string blendInfo { get; set; }
            public string taste { get; set; }
            public string foodPairing { get; set; }
            public string aroma { get; set; }
            public string bottleHistory { get; set; }
            public int redWineAmount { get; set; }
            public int servingTemp { get; set; }
            public int ageingPotential { get; set; }
            public int reserveWineAmount { get; set; }
            public int dosageAmount { get; set; }
            public int alchoholVol { get; set; }
            public int pinotNoir { get; set; }
            public int pinotMeunier { get; set; }
            public int chardonnay { get; set; }
        }
*/

        /*
        public string MasterId { get; set; }
        public string ChampagneId { get; set; }
        public string BottleName { get; set; }
        public string BrandName { get; set; }
        public string BrandId { get; set; }
        public string Dosage { get; set; }
        public string BottleImageId { get; set; }
        public string BottleImageName { get; set; }
        public int NumberOfTastings { get; set; }
        public int SumOfRatings { get; set; }
        public double AverageRating { get; set; }
        public bool IsVintage { get; set; }
        public string Character { get; set; }
        public double AlchoVol { get; set; }
        public double BottleSize { get; set; }
        public int Year { get; set; }
        public List<TechnicalIdentity> TechnicalIdentities { get; set; }
        public string BlendInformation { get; set; }
        public string TechnicalIdentitiesEye { get; set; }
        public string TechnicalIdentitiesNose { get; set; }
        public string TechnicalIdentitiesPalate { get; set; }
        public string FoodPairing { get; set; }


        public string BottleInfo => InfoString();

        public string GetAverage => Rounded();

        public string Tastings => (string)NumberOfTastings.ToString();

        public string BottleImageUrl => $"https://api-cm-staging.azurewebsites.net/api/v1/files/{BottleImageId}";

        public string GetBottleSize => (string)BottleSize.ToString() +"cl";

        public string GetAlchohol => "Alc.Vol. " + AlchoVol + "%";




        private string InfoString()
        {
            if (IsVintage)
            {
                return "Vintage";// | " + Dosage; 
            }
            else
            {
                return "Non-Vintage";// | " + Dosage;
            }
        }

        private String Rounded()
        {
            if (AverageRating % 1 == 0)
            {
                return (string)AverageRating.ToString() + ".0";
            }
            return Math.Round(AverageRating, 1).ToString();
        }

*/
    }
}
