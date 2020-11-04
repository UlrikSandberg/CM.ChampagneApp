using System;

namespace CM.ChampagneApp.UI.UIFacade.Models.UITop10
{
    public class UITop10ListCardModel : BaseUIModel
    {
        public Guid ImageId { get; set; }

        public string Title { get; set; }

        public string AugmentedTitle { get; set; }

        public string Subtitle { get; set; }

        public string AugmentedSubtitle { get; set; }

        public string Description { get; set; }

        public string ContentType { get; set; }

        public string ConfigurationKey { get; set; }
        
        public bool IsValidForVintage { get; set; }
        
        public bool IsValidForNonVintage { get; set; }
        
        public string ImageUrl
        {
            get
            {
                return "PlaceholderCover.jpg";
            }
        }
    }
}
