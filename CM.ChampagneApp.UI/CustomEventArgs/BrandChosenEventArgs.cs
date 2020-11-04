using System;
namespace CM.ChampagneApp.UI.CustomEventArgs
{
    public class BrandChosenEventArgs
    {
		public Guid BrandId { get; private set; }

		public BrandChosenEventArgs(Guid brandId)
        {
            BrandId = brandId;
        }
    }
}
