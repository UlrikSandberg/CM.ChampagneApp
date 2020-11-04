using System;
namespace CM.ChampagneApp.UI.UIFacade.Models.UIBrandModels
{
    public class UIBrandSearchModel : BaseUIModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ImageId { get; set; }

        public string ImageUrl
        {
            get
            {
                return ImageId != Guid.Empty
                    ? new Uri(Configuration.BlobBrandStorageUrl, $"{Id}/images/{ImageId}/small.jpg").ToString()
                    : "PlaceholderBrandLogoImg.png";
            }
        }
    }
}
