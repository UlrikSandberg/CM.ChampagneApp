using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CM.ChampagneApp.Instrumentation;
using CM.ChampagneApp.UI.Pages.ChampagnePages.ChampagneProfilePage;
using CM.ChampagneApp.UI.Pages.ChampagnePages.Helpers;
using CM.ChampagneApp.UI.Pages.Commons;
using CM.ChampagneApp.UI.Pages.Top10Pages.Top10LightroomPage.Helpers;
using CM.ChampagneApp.UI.UIFacade.Models.UITop10;
using CM.ChampagneApp.UI.UIFacade.Services;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.Top10Pages.Top10LightroomPage
{
    public class Top10LightroomPageModel : BasePageModel
    {
        //Private properties
        private double SlidePosition { get; set; }
        private int CurrentIndex { get; set; }
        private bool filterByHighestRating = true;

        //DataServices and initData
        private Top10LightroomInitData _initData;
        private readonly IUITop10ListDataService _dataService;

        public string NavigationTitle { get; set; }
        public string NavigationSubtitle { get; set; }

        public bool IsLoading { get; set; }

        //DownloadManager
        public ObservableCollection<UITop10CardModel> ItemSource { get; set; }

        private ObservableCollection<UITop10CardModel> highestRatingCache;
        private ObservableCollection<UITop10CardModel> mostTastingCache;

        public IDownloadManager<ObservableCollection<UITop10CardModel>> HighestRatingManager { get; set; }
        public IDownloadManager<ObservableCollection<UITop10CardModel>> MostTastingManager { get; set; }

        //Card Selected
        public ICommand CardSelected { get; set; }

        //SegmentedControl
        public ICommand SegmentIndexChanged { get; set; }

        //Parallax commands
        public ICommand ScrollLeft { get; set; }
        public ICommand ScrollRight { get; set; }
        public ICommand PositionSelected { get; set; }
        public ICommand ReconnectCommand { get; set; }

        private async Task<IEnumerable<UITop10CardModel>> Top10Endpoint()
        {
            return await _dataService.GetTop10List(_initData.ConfigurationKey, _initData.IsVintage, filterByHighestRating);
        }

        public Top10LightroomPageModel(IEventCollector eventCollector, IUITop10ListDataService dataService) : base(eventCollector)
        {
            ScrollLeft = new Command<double>(Handle_ScrollLeft);
            ScrollRight = new Command<double>(Handle_ScrollRight);
            PositionSelected = new Command<int>(Handle_PositionSelected);
            ReconnectCommand = new Command(async () => await Download());

            CardSelected = new Command<UITop10CardModel>(async (x) => await CoreMethods.PushPageModel<ChampagneProfilePageModel>(new ChampagneProfileInitData(x.Id)));
            SegmentIndexChanged = new Command<double>(async (x) => await Handle_SegmentIndexChanged(x));

            HighestRatingManager = new DownloadManager<ObservableCollection<UITop10CardModel>, IEnumerable<UITop10CardModel>, IEnumerable<UITop10CardModel>>(
                async () => await _dataService.GetTop10List(_initData.ConfigurationKey, _initData.IsVintage, filterByHighestRating),
                () =>
                {
                    highestRatingCache = new ObservableCollection<UITop10CardModel>();
                    HighestRatingManager.Data.ToList().ForEach(x => highestRatingCache.Add(x));
                    //If the view is still on this respective load then set itemSource else dont
                    if (filterByHighestRating)
                        ItemSource = highestRatingCache;
                    DownloadFinished(true);
                },
                dataMapperFunc: (x) => new ObservableCollection<UITop10CardModel>(x),
                outOfServiceCompletionHandler: () => HandleOutOfService(true));
            MostTastingManager = new DownloadManager<ObservableCollection<UITop10CardModel>, IEnumerable<UITop10CardModel>, IEnumerable<UITop10CardModel>>(
            async () => await dataService.GetTop10List(_initData.ConfigurationKey, _initData.IsVintage, false),
                () => 
                {
                    mostTastingCache = new ObservableCollection<UITop10CardModel>();
                    MostTastingManager.Data.ToList().ForEach(x => mostTastingCache.Add(x));
                    if (!filterByHighestRating)
                        ItemSource = mostTastingCache;
                    DownloadFinished(false);
                },
                dataMapperFunc: (x) => new ObservableCollection<UITop10CardModel>(x),
                outOfServiceCompletionHandler: () => HandleOutOfService(false));

            HasBackButton = true;
            _dataService = dataService;
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            _initData = initData as Top10LightroomInitData;

            if(_initData != null)
            {
                //Download data!
                NavigationTitle = _initData.Title;
                NavigationSubtitle = _initData.Subtitle;

                IsLoading = true;
                HighestRatingManager.FetchData();

            }
            else
            {
                throw new ArgumentException($"InitData for {nameof(Top10LightroomPageModel)} can't be null --> Use initModel: {nameof(Top10LightroomInitData)}");
            }
        }

        private void HandleOutOfService(bool isHighestRating)
        {
            if(isHighestRating && filterByHighestRating)
            {
                Console.WriteLine("Segment index 0 and highestRating out of service");
                IsLoading = false;
                IsOutOfService = true;
            }

            if(!isHighestRating && !filterByHighestRating)
            {
                Console.WriteLine("Segment index 1 and mostTasting out of service");
                IsLoading = false;
                IsOutOfService = true;
            }
        }

        private void DownloadFinished(bool isHighestRating)
        {
            if ((isHighestRating && filterByHighestRating) || (!isHighestRating && !filterByHighestRating))
                IsLoading = false;
        }

        private async Task Download()
        {
            IsOutOfService = false;
            if(filterByHighestRating)
            {
                if(highestRatingCache != null) //Check if cache holds data
                {
                    ItemSource = highestRatingCache;

                    if (ItemSource.Count > 0)
                    {
                        if(ItemSource[CurrentIndex] != null)
                        {
                            ItemSource[CurrentIndex].Position = 0;
                        }
                    }
                    
                    IsLoading = false;
                }
                else
                {
                    IsLoading = true;
                    await HighestRatingManager.FetchData();
                }
            }
            else
            {
                if(mostTastingCache != null)
                {
                    ItemSource = mostTastingCache;
                    if (ItemSource.Count > 0)
                    {
                        if (ItemSource[CurrentIndex] != null)
                        {
                            ItemSource[CurrentIndex].Position = 0;
                        }    
                    }
                    
                    IsLoading = false;
                }
                else
                {
                    IsLoading = true;
                    await MostTastingManager.FetchData();
                }
            }
        }

        private async Task Handle_SegmentIndexChanged(double newValue)
        {
            switch(newValue)
            {
                case 0:
                    filterByHighestRating = true;
                    await Download();
                    break;
                case 1:
                    filterByHighestRating = false;
                    await Download();
                    break;
            }
        }

        //****** Methods below here are used for the parallax effect.
        private void Handle_PositionSelected(int position)
        {
            //Set the new index, so that we may use this index when offsetting elements
            CurrentIndex = position;
            //Set the slide position to zero so that the paralax scroll for the current visible index as
            //well as the next will have a paralax scroll which is normal and not offset by previous slidePositions
            SlidePosition = 0;
        }

        private void Handle_ScrollRight(double slidePosition)
        {
            //Set the slidePosition
            SlidePosition = slidePosition;

            if(filterByHighestRating)
            {
                //Offset the current index slidePosition
                if(HighestRatingManager.Data.Count == 0)
                {
                    return;
                }
                HighestRatingManager.Data[CurrentIndex].Position = -SlidePosition;

                //When we are scrolling to the right, we also offset the next element if such exists
                if (CurrentIndex < HighestRatingManager.Data.Count - 1)
                {
                    HighestRatingManager.Data[CurrentIndex + 1].Position = 100 - slidePosition;
                }
            }
            else
            {
                if(MostTastingManager.Data.Count == 0)
                {
                    return;
                }
                //Offset the current index slidePosition
                MostTastingManager.Data[CurrentIndex].Position = -SlidePosition;

                //When we are scrolling to the right, we also offset the next element if such exists
                if (CurrentIndex < MostTastingManager.Data.Count - 1)
                {
                    MostTastingManager.Data[CurrentIndex + 1].Position = 100 - slidePosition;
                }
            }
        }

        private void Handle_ScrollLeft(double slidePosition)
        {
            //Set the slidePosition
            SlidePosition = slidePosition;

            //Offset the current index slidePosition
            if(filterByHighestRating)
            {
                if(HighestRatingManager.Data.Count == 0)
                {
                    return;
                }
                HighestRatingManager.Data[CurrentIndex].Position = +slidePosition;

                //When we are scrolling to the left, we also offset the previous element
                if (CurrentIndex > 0)
                {
                    HighestRatingManager.Data[CurrentIndex - 1].Position = -100 + slidePosition;
                }
            }
            else
            {
                if(MostTastingManager.Data.Count == 0)
                {

                    return;
                }

                MostTastingManager.Data[CurrentIndex].Position = +slidePosition;

                //When we are scrolling to the left, we also offset the previous element
                if (CurrentIndex > 0)
                {
                    MostTastingManager.Data[CurrentIndex - 1].Position = -100 + slidePosition;
                }
            }
        }
    }
}
