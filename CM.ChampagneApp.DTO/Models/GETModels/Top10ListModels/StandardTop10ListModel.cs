using System;

namespace CM.ChampagneApp.DTO.Models.GETModels.Top10ListModels
{
    public class StandardTop10ListModel
    {
        public Guid ImageId { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Description { get; set; }

        public string ContentType { get; set; }

        public string Configurationkey { get; set; }
        
        public bool IsValidForVintage { get; set; }
        
        public bool IsValidForNonVintage { get; set; }
    }
}
