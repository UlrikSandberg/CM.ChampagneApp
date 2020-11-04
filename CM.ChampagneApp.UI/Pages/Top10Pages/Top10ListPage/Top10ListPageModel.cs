using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CM.ChampagneApp.Instrumentation;
using CM.ChampagneApp.UI.Pages.Commons;
using CM.ChampagneApp.UI.Pages.Top10Pages.Top10LightroomPage;
using CM.ChampagneApp.UI.Pages.Top10Pages.Top10ListPage.Helpers;
using CM.ChampagneApp.UI.UIFacade.Models.UITop10;
using CM.ChampagneApp.UI.UIFacade.Services;
using Xamarin.Forms;
using System.Linq;
using CM.ChampagneApp.UI.Pages.Top10Pages.Top10LightroomPage.Helpers;

namespace CM.ChampagneApp.UI.Pages.Top10Pages.Top10ListPage
{
    public class Top10ListPageModel : BasePageModel
    {
        private readonly IUITop10ListDataService _dataService;
        private bool IsVintageTop10 = false;

        public IDownloadManager<ObservableCollection<UITop10ListCardModel>> DownloadManager { get; set; }
        
        public ObservableCollection<UITop10ListCardModel> ItemSource { get; set; }

        public ICommand ItemClicked { get; set; }
        public ICommand SegmentSelected { get; set; }

        public Top10ListPageModel(IEventCollector eventCollector, IUITop10ListDataService dataService) : base(eventCollector)
        {
            //Configure downloadManager
            DownloadManager = new DownloadManager<ObservableCollection<UITop10ListCardModel>, IEnumerable<UITop10ListCardModel>, IEnumerable<UITop10ListCardModel>>(dataService.GetTop10Lists, dataMapper: new StandardTop10ListsDataMapper(), completionHandler:
                () =>
                {
                    DownloadManager.Data.ToList().ForEach(x => x.AugmentedSubtitle = $"{x.Subtitle}");
                    ItemSource = new ObservableCollection<UITop10ListCardModel>(DownloadManager.Data.ToList().Where(x => x.IsValidForNonVintage));
                });

            ItemClicked = new Command<UITop10ListCardModel>(async (x) => await CoreMethods.PushPageModel<Top10LightroomPageModel>(new Top10LightroomInitData(x.ConfigurationKey, IsVintageTop10, x.Title)));
            SegmentSelected = new Command<double>((x) => Handle_SegmentSelected(x));
            _dataService = dataService;

            HasBackButton = true;
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            DownloadManager.FetchData();
        }

        private void Handle_SegmentSelected(double chosenIndex)
        {
            switch(chosenIndex)
            {
                case 0:
                    IsVintageTop10 = false;
                    DownloadManager.Data.ToList().ToList().ForEach(x => x.AugmentedSubtitle = $"Non-Vintage");
                    ItemSource = new ObservableCollection<UITop10ListCardModel>(DownloadManager.Data.ToList().Where(x => x.IsValidForNonVintage));
                    break;
                case 1:
                    IsVintageTop10 = true;
                    DownloadManager.Data.ToList().ToList().ForEach(x => x.AugmentedSubtitle = $"Vintage");
                    ItemSource = new ObservableCollection<UITop10ListCardModel>(DownloadManager.Data.ToList().Where(x => x.IsValidForVintage));
                    break;
            }
        }
    }
}
