using System;
namespace CM.ChampagneApp.UI.Pages.ChampagnePages.Helpers
{
    public class ChampagneLightroomInitData
    {
		public string ImageUrl { get; private set; }
		public string BottleName { get; private set; }
		public string BrandName { get; private set; }

		public ChampagneLightroomInitData(string imageUrl, string bottleName, string brandName)
        {
            BrandName = brandName;
			BottleName = bottleName;
			ImageUrl = imageUrl;
		}
    }
}
