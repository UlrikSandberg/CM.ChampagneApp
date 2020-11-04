using System;
using CM.ChampagneApp.Business.Configuration;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.UIFacade.Models.UIArticleStructure
{
    public class UIBrandArticleOverview : UIArticleOverview
    {
        public UIBrandArticleOverview(IAppConfiguration configurationProxy) : base(configurationProxy)
        {
        }

        public Guid brandId { get; set; }

        public override string ArticleImageUrl => new Uri(configurationProxy.BlobBrandStorageUrl, $"{brandId}/images/{ArticleImageId}/small.jpg").ToString();

    }
}

