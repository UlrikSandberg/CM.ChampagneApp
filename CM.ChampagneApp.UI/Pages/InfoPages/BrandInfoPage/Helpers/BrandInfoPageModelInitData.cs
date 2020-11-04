using System;
namespace CM.ChampagneApp.UI.PageModelInitData
{
    public class BrandInfoPageModelInitData
    {
        public Guid InfoPageId { get; private set; }
        public string Title { get; }
        public string Subtitle { get; }
        public Guid BrandId { get; private set; }

		public BrandInfoPageModelInitData(Guid brandId, Guid infoPageId, string title = null, string subtitle = null)
        {
            BrandId = brandId;
			InfoPageId = infoPageId;
            Title = title;
            Subtitle = subtitle;
        }
    }
}
