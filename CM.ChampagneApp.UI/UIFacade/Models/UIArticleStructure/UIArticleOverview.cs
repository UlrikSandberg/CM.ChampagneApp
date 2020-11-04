using System;
using CM.ChampagneApp.Business.Configuration;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.UIFacade.Models
{
    public abstract class UIArticleOverview 
    {
        protected readonly IAppConfiguration configurationProxy;

        public UIArticleOverview(IAppConfiguration configurationProxy)
        {
            this.configurationProxy = configurationProxy;
        }

        public string ArticleTitle { get; set; }

        public string ArticleSubTitle { get; set; }

        public Guid ArticleImageId { get; set; }

        public string ArticleURLPath { get; set; }

        public abstract string ArticleImageUrl { get; }

    }
}

