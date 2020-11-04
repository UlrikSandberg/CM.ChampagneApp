using System;
using CM.ChampagneApp.Business.Configuration;
using System.Collections.Generic;
using CM.ChampagneApp.Business.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace CM.ChampagneApp.Business.Services
{

    public interface IArticleDataService
    {
        IEnumerable<pageLink> GetChampagneProfileArticles();
    }

    public class ArticleDataService: IArticleDataService
    {

        private readonly IConfigurationProxy config;
        private readonly HttpClient httpClient;

        public ArticleDataService(IConfigurationProxy config)
        {
            this.config = config;
            this.httpClient = new HttpClient();
        }

        public IEnumerable<pageLink> GetChampagneProfileArticles()
        {

            var article1 = new pageLink();
            var article2 = new pageLink();


            article1.URLPath = "article/123123123";
            article1.Title = "Champagne";
            article1.Body = "First press juice of mainly Grand and Premier Crus grapes from the heart of the Champagne wine-growing area. 20% to 30% of reserve wines are aged in wooden barrels using the solera system to incorporate older wines without losing freshness.";
            article1.ImageId = "MummCover";

            article2.URLPath = "brands/123123123";
            article2.Title = "Philliponnat";
            //article2.Body = "For almost five hundred years, the Philipponnat family has left its mark on the soil of the Champagne region. Generations of men and women have cultivated the land at Ay, the family's home since the time of Apvril le Philipponnat who owned vines at Le Léon, between Ay and Dizy, in 1522. ";
            article2.Body = "First press juice of mainly Grand and Premier Crus grapes from the heart of the Champagne wine-growing area. 20% to 30% of reserve wines are aged in wooden barrels using the solera system to incorporate older wines without losing freshness.";
            article2.ImageId = "PhilliponatCover";

            var list = new List<pageLink>();
            list.Add(article1);
            list.Add(article2);

            return list;
        }
    }
}
