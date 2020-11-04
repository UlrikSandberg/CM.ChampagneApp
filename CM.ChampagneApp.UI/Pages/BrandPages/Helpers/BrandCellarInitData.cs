using System;
namespace CM.ChampagneApp.UI.Pages.Helpers
{
    public class BrandCellarInitData
    {
		public Guid BrandId { get; private set; }

        public BrandCellarInitData(Guid brandId)
        {
			BrandId = brandId;
        }
    }
}
