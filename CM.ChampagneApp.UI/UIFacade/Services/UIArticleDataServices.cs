using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CM.ChampagneApp.Business.Configuration;
using CM.ChampagneApp.UI.UIFacade.Models;
using CM.ChampagneApp.UI.UIFacade.Models.UIArticleStructure;
using CM.ChampagneApp.UI.UIFacade.Models.UIChampagneModels;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.UIFacade.Services
{
    public interface IUIArticleDataService
    {
        IEnumerable<UIArticleOverview> GetChampagneProfileArticles(UIChampagne uIChampagne);
    }

    public class UIArticleDataServices : IUIArticleDataService
    {
        private readonly IAppConfiguration configurationProxy;

        public UIArticleDataServices(IAppConfiguration configurationProxy)
        {
            this.configurationProxy = configurationProxy;
        }

        public IEnumerable<UIArticleOverview> GetChampagneProfileArticles(UIChampagne uIChampagne)
        {
            /*Remarks 
            --- This method should download from the API, thus giving us control of which articles to show. Especially, when to
            --- turn on and off our own champagne articles... But for now since the champagneProfile allready holds all the data
            --- for the only articlecardoverview this will have to do ;) Else in the future this method should take as parameters
            --- a Guid brandId.

            --- Likewise the articleURLPath needs to be tailored for a more generic solution.
            */

            var uiArticleList = new List<UIArticleOverview>();

            var uiBrandArticleOverview = new UIBrandArticleOverview(configurationProxy);
            uiBrandArticleOverview.ArticleTitle = "Visit " + uIChampagne.BrandName;
            uiBrandArticleOverview.ArticleSubTitle = uIChampagne.BrandProfileText;
            uiBrandArticleOverview.ArticleImageId = uIChampagne.BrandCoverImgId;
            uiBrandArticleOverview.ArticleURLPath = "brand/" + uIChampagne.BrandId + "/" + "profile" + "/";
            uiBrandArticleOverview.brandId = uIChampagne.BrandId;

            uiArticleList.Add(uiBrandArticleOverview);

            return uiArticleList;

        }
    }
}

