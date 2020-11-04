using System;
using System.Collections.Generic;
using CM.ChampagneApp.DTO.Models.GETModels.ImageModels;

namespace CM.ChampagneApp.DTO.Models.GETModels.BrandModels
{
    public class BrandInfoPageModel
    {
        public BrandInfoPageModel()
        {
        }

		public Guid BrandId { get; set; }

        public Guid BrandPageId { get; set; }

        public string Title { get; set; }

		public string BrandName { get; set; }

		public string uiTemplateIdentifier { get; set; }

        public Guid CardImgId { get; set; }

        public Guid HeaderImgId { get; set; }

		public BrandImage HeaderImg { get; set; }

        public bool Published { get; set; }

        public string Url { get; set; }

        public List<BrandInfoPageSectionModel> Sections { get; set; }
    }
}
