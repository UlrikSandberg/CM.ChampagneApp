using System;
namespace CM.ChampagneApp.UI.Pages.Helpers
{
    public class BrandProfileInitData
    {
        public Guid BrandId { get; private set; }

        public BrandProfileInitData(Guid brandId)
        {
            BrandId = brandId;
        }
    }
}
