using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.UIFacade.Models;

namespace CM.ChampagneApp.UI.Pages.Commons
{
	public class PagingManager : INotifyPropertyChanged
    {   
        //The previous page number
        public int PreviousPage { get; private set; } = 0;      
        //The current page!
		public int Page { get; private set; } = 0;
        //The pageSize of each page.
		public int PageSize { get; private set; }

        //***** Boolean values indicating different loading states ****

        //private readonly Func<int, int, Task<IEnumerable<T>>> _dataSourceQuery;

        //Indicates whether the app has lost connection during pagination
		public bool IsOutOfServiceTextVisible { get; set; }

        //Indicates whether all entities has been downloaded for this respective pagingManager
		public bool AllEntitiesHasBeenDownloaded { get; set; }

		//Indicates if the pagingManager is currently loading more entities
		public bool IsLoadingEntities { get; set; }

		//Indicates if the pagingManager is doing inital load is set to true as default
		public bool IsInitialLoadInProgress { get; set; }
      
        //Indicates whether the current page is refreshing its content
		public bool IsRefreshing { get; set; }
		public bool IsRefreshingInterrupted { get; set; }

        /*public PagingManager(Func<int, int, Task<IEnumerable<T>>> dataSourceQuery, int pageSize = 30, bool isInitialLoadInProgress = true)
        {
			PageSize = pageSize;
			IsInitialLoadInProgress = isInitialLoadInProgress;
            _dataSourceQuery = dataSourceQuery;
		}*/

        public PagingManager(int pageSize = 30, bool isInitialLoadInProgress = true)
        {
            PageSize = pageSize;
            IsInitialLoadInProgress = isInitialLoadInProgress;
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

        public void InvokeRefreshState()
        {
            IsRefreshing = true;
            AllEntitiesHasBeenDownloaded = false;
            SetPage(0);
        }

        public void SetBoolFalseState()
		{
			AllEntitiesHasBeenDownloaded = false;
			IsLoadingEntities = false;
			IsInitialLoadInProgress = false;
			IsOutOfServiceTextVisible = false;
			IsRefreshing = false;
		}

        public void ResetBoolStatesToDefault()
		{
			AllEntitiesHasBeenDownloaded = false;
			IsLoadingEntities = false;
			IsRefreshing = false;
			IsOutOfServiceTextVisible = false;
			IsInitialLoadInProgress = true;
		}

        /*public async Task<IEnumerable<T>> FetchEntitiesPagedAsync()
        {
            return await _dataSourceQuery(Page, PageSize);
        }*/

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
