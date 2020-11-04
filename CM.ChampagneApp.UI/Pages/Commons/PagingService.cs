using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.UI.Elements.Lists.InfiniteListView;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.Commons
{
    public class PagingService<T> : IPagingService, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand RequestNextPage { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand ReconnectCommand { get; set; }

        //Inform subscribers when the LoadingEntitiesDidChange.
        public delegate void IsLoadingEntitiesChanged(object sender, ValueChangedEventArgs<bool> args);
        public event IsLoadingEntitiesChanged LoadingEntitiesChanged;

        public delegate void Handle_OutOfService(object sender, System.EventArgs e);
        public event Handle_OutOfService OnOutOfService;

        public delegate void Handle_InvalidSearch(object sender, System.EventArgs e);
        public event Handle_InvalidSearch OnInvalidSearch;

        public delegate void AllEntitiesHasBeenDownloadedChanged(object sender, ValueChangedEventArgs<bool> e);
        public event AllEntitiesHasBeenDownloadedChanged OnAllEntitiesHasBeenDownloadedChanged;

        public delegate void IsRefreshingChanged(object sender, ValueChangedEventArgs<bool> args);
        public event IsRefreshingChanged RefreshingChanged;

        //The previous page number
        public int PreviousPage { get; private set; } = 0;
        //The current page!
        public int Page { get; private set; } = 0;
        //The pageSize of each page.
        public int PageSize { get; private set; }

        //Indicates whether all entities has been downloaded for this respective pagingManager
        public bool AllEntitiesHasBeenDownloaded { get; set; }
        //Indicates if the pagingManager is currently loading more entities
        public bool IsLoadingEntities { get; set; }
        //Indicates whether the current is refreshing its content
        public bool IsRefreshing { get; set; }
        public bool IsRefreshingInterrupted { get; set; }
        public bool IsOutOfService { get; set; }

        public bool IsInitialLoad { get; set; }

        //Loading status 
        public InfiniteListViewActivityStatus LoadingStatus { get; set; }
        public InfiniteListViewActivityStatus RefreshingStatus { get; set; }

        //The query from which data is fetched
        private readonly Func<int, int, Task<IEnumerable<T>>> _dataSourceQuery;
        
        //The itemSource where data elements are added
        public ObservableCollection<T> itemSource { get; set; }

        public ICollectionViewDataResolver<T> _collectionViewData { get; set; }

        //The basic method to call in order for data to be retreived
        private async Task<IEnumerable<T>> DownloadFromDataSource()
        {
            return await _dataSourceQuery(Page, PageSize);
        }

        public PagingService(Func<int, int, Task<IEnumerable<T>>> dataSourceQuery, ICollectionViewDataResolver<T> collectionViewData = null, int pageSize = 30)
        {
            LoadingStatus = InfiniteListViewActivityStatus.Inactive;
            RefreshingStatus = InfiniteListViewActivityStatus.Inactive;

            _dataSourceQuery = dataSourceQuery;
            itemSource = new ObservableCollection<T>();
            PageSize = pageSize;

            _collectionViewData = collectionViewData;

            RequestNextPage = new Command(async () => await FetchEntitiesPagedAsync());
            RefreshCommand = new Command(async () => await Refresh());
            ReconnectCommand = new Command(async () => await Reconnect());
        }

        public async Task Refresh()
        {
            if (IsLoadingEntities)
                return;

            SetRefreshing(true);
            SetAllEntitiesHasBeenDownloaded(false);
            SetPage(0);
            await FetchEntitiesPagedAsync();
        }

        public async Task Reconnect()
        {
            if(IsRefreshingInterrupted && itemSource.Count > 0)
            {
                //This means that a refresh was interuped while there where still entities. Reconnect should merely let people back to the a list with data
                LoadingStatus = InfiniteListViewActivityStatus.DownloadingFinished;
            }
            else
            {
                await FetchEntitiesPagedAsync(false);
            }
        }

        public async Task FetchEntitiesPagedAsync(bool forceInitialLoad = false)
        {
            //Forcing initial load will set page = 0, and allEntitiesHasBeenDownloaded equal to false. Furthermore it will clear the itemSource when new entries has been downloaded
            if (forceInitialLoad)
            {
                ForceInitialLoad();
            }
            else if (IsLoadingEntities || AllEntitiesHasBeenDownloaded)
            {
                return;
            }

            var callId = Guid.NewGuid();
            App.ActiveCallTracker.AddCall(nameof(FetchEntitiesPagedAsync), callId);

            //Start download
            SetIsLoadingEntities(true, IsRefreshing);
            IsRefreshingInterrupted = false;
            var result = await DownloadFromDataSource();

            //In order to make sure that this is the newest call, previous calls
            //might be returning... Check if the respective callId for this
            //thread is still the newest running call. If not dispose of this resource
            if(!App.ActiveCallTracker.IsMostRecentCall(nameof(FetchEntitiesPagedAsync), callId))
            {
                //Since this callId is not the most recent call, dispose of the resource and wait for the newest call
                App.ActiveCallTracker.DisposeActiveCall(nameof(FetchEntitiesPagedAsync), callId);
                return;
            }
            //Since it was the most recent we need continue executing code
            //but the call is still not active anymore and thus has to be 
            //removed nevertheless
            App.ActiveCallTracker.DisposeActiveCall(nameof(FetchEntitiesPagedAsync), callId);

            if (result == null)//<-- Something went wrong fx. There is no service. When the App messaging system gets more sophisticated, send eventHandler to handle this.
            {
                NotifyOutOfService(forceInitialLoad);
                return;
            }

            var uiList = new ObservableCollection<T>(result);
            //The list was downloaded but returned empty list, conclusion is that we are at the end of the list.
            if(uiList.Count == 0)
            {
                //If the load was during a forcedReload, the itemSource is cleared
                if(forceInitialLoad)
                {
                    itemSource.Clear();
                }

                SetRefreshing(false);
                SetAllEntitiesHasBeenDownloaded(true);
                SetIsLoadingEntities(false);
                return;
            }

            //There is data! <-- 
            IncrementPage();
            ResolveNewValues(uiList, forceInitialLoad);
            SetRefreshing(false);
            SetIsLoadingEntities(false); //<-- Add values to list!
        }

        private void ResolveNewValues(IEnumerable<T> newValues, bool isInitialLoad = false)
        {
            //If the view is going through a refresh
            if(IsRefreshing)
            {
                itemSource.Clear();
                if(_collectionViewData != null)
                {
                    _collectionViewData.ClearItemSource();
                }
                SetRefreshing(false);
            }

            if(isInitialLoad)
            {
                itemSource.Clear();
                if (_collectionViewData != null)
                {
                    _collectionViewData.ClearItemSource();
                }
            }

            if (_collectionViewData != null)
            {
                _collectionViewData.ResolveNewValues(newValues);
            }

            foreach (var entry in newValues)
            {
                itemSource.Add(entry);
            }
        }

        private void ForceInitialLoad()
        {
            SetAllEntitiesHasBeenDownloaded(false);
            SetPage(0);
        }

        private void SetAllEntitiesHasBeenDownloaded(bool value)
        {
            if(!AllEntitiesHasBeenDownloaded.Equals(value))
            {
                var oValue = AllEntitiesHasBeenDownloaded;
                AllEntitiesHasBeenDownloaded = value;

                if(OnAllEntitiesHasBeenDownloadedChanged != null)
                {
                    OnAllEntitiesHasBeenDownloadedChanged(this, new ValueChangedEventArgs<bool>(oValue, value));
                }
            }

            if(value && itemSource.Count == 0)
            {
                if(OnInvalidSearch != null)
                {
                    OnInvalidSearch(this, new System.EventArgs());
                }
            }
        }

        private void SetRefreshing(bool value)
        {
            if(!IsRefreshing.Equals(value))
            {
                var oValue = IsRefreshing;
                IsRefreshing = value;

                if (value)
                {
                    //RefreshingStatus = InfiniteListViewActivityStatus.IsRefreshing;
                }
                else
                {
                    //RefreshingStatus = InfiniteListViewActivityStatus.DownloadingFinished;
                }

                if (RefreshingChanged != null)
                {
                    RefreshingChanged(this, new ValueChangedEventArgs<bool>(oValue, value));
                }
            }
        }

        private void SetRefreshInterupted()
        {
            RollBack();
            SetRefreshing(false);
            IsRefreshingInterrupted = true;
        }

        private void NotifyOutOfService(bool forceInitialLoad = false)
        {
            if (IsRefreshing || itemSource.Count < 1 || forceInitialLoad) //This should cause the out of service screen
            {
                if (IsRefreshing)
                {
                    SetRefreshInterupted();
                }

                if(forceInitialLoad)
                {
                    itemSource.Clear();
                }

                IsOutOfService = true;
                SetIsLoadingEntities(false);
                if (OnOutOfService != null)
                {
                    OnOutOfService(this, new EventArgs());
                }

                LoadingStatus = InfiniteListViewActivityStatus.OutOfService;
            }
            else //The user didn't request a reset and there is already content. He has merely paged for more entries
            {
                LoadingStatus = InfiniteListViewActivityStatus.OutOfService;
                SetIsLoadingEntities(false);

                if(OnOutOfService != null)
                {
                    OnOutOfService(this, new EventArgs());
                }
                LoadingStatus = InfiniteListViewActivityStatus.OutOfService;
            }
        }

        /// <summary>
        /// Sets the is loading entities property and invokes the eventHandler LoadingEntitiesChanged if any subscribers/> .
        /// </summary>
        /// <param name="value">If set to <c>true</c> value.</param>
        private void SetIsLoadingEntities(bool value, bool isRefreshing = false)
        {
            if(IsLoadingEntities.Equals(value))
            {
                //If the value did not change do nothing
                return;
            }

            var oldValue = IsLoadingEntities;
            IsLoadingEntities = value;

            if (value)
            {
                if(isRefreshing)
                {
                    LoadingStatus = InfiniteListViewActivityStatus.IsRefreshing;
                }
                else
                {
                    LoadingStatus = InfiniteListViewActivityStatus.IsDownloading;
                }
            }
            else
            {
                LoadingStatus = InfiniteListViewActivityStatus.DownloadingFinished;
            }

            //Fire event that isLoadingEntities has changed;
            if (LoadingEntitiesChanged != null)
            {
                LoadingEntitiesChanged(this, new ValueChangedEventArgs<bool>(oldValue, value));
            }
        }

        public void IncrementPage()
        {
            PreviousPage = Page;
            Page++;
        }

        public void IncrementPage(int incrementBy)
        {
            PreviousPage = Page;
            Page += incrementBy;
        }

        public void SetPage(int page)
        {
            PreviousPage = Page;
            Page = page;
        }

        public void RollBack()
        {
            Page = PreviousPage;
        }

        public bool ShouldReload()
        {
            if(!(itemSource.Count > 0) && ! IsRefreshingInterrupted)
            {
                return true;
            }

            return false;
        }
    }
}
