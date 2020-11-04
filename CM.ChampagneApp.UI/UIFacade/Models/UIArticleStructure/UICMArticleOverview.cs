using System;
using CM.ChampagneApp.Business.Configuration;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.UIFacade.Models.UIArticleStructure
{
    public class UICMArticleOverview : UIArticleOverview
    {
        public UICMArticleOverview(IAppConfiguration configurationProxy) : base(configurationProxy)
        {
        }

        public override string ArticleImageUrl => new Uri(configurationProxy.BlobStorageBaseUrl, $"cm/{ArticleImageId}/small.jpg").ToString();


    }
}

