using System;
namespace CM.ChampagneApp.DTO.Models.GETModels
{
    public class ConfigurationModel
    {
        public Uri ApiBaseUrl { get; set; }
        public Uri BlobStorageBaseUrl { get; set; }
        public Uri IdentityBaseUrl { get; set; }

        public ConfigurationModel()
        {
        }
    }
}
