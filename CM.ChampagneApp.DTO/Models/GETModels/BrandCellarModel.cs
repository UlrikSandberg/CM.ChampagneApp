using System;
using System.Collections.Generic;

namespace CM.ChampagneApp.DTO.Models
{
    public class BrandCellarModel
    {
        
		public string Title { get; set; }
		public Guid CardImgId { get; set; }
		public Guid HeaderImgId { get; set; }
		public string Url { get; set; }
		public List<Section> sections { get; set; }

        public class Section
		{
			public string Title { get; set; }
			public string Body { get; set; }
			public List<ChampagneRoot> Champagnes { get; set; }
		}

    }
}
