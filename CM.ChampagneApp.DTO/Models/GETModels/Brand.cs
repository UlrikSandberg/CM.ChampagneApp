using System;
namespace CM.ChampagneApp.DTO.Models
{
    public class Brand
    {
        public string Id
        {
            get;
            set;
        }
    
        public string BrandName
        {
            get;
            set;
        }

        public int NumberOfChampagnes
        {
            get;
            set;
        }

        public string BrandCoverImageId
        {
            get;
            set;
        }

        public string BrandCoverImageUrl => $"https://api-cm-staging.azurewebsites.net/api/v1/files/{BrandCoverImageId}";

        public Brand()
        {
        }
    }
}
