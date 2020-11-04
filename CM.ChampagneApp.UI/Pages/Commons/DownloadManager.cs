using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CM.ChampagneApp.UI.Pages.Commons.Helpers;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.Commons
{
    public interface IDownloadManager<TData> where TData : class
    {
        Task FetchData();
        TData Data { get; set; }
        void UpdateHandler(object sender, System.EventArgs e);
        void AssertConfigurations();
    }

    public class DownloadManager<TData, TSource, TUpdate> : IDownloadManager<TData>, INotifyPropertyChanged where TData : class where TSource : class where TUpdate : class
    {
        public TData Data { get; set; }

        public bool IsDownloading { get; set; } = true;
        public bool IsOutOfService { get; set; } = false;
        public ICommand Reconnect { get; set; }

        private readonly Func<Task<TSource>> _dataSourceQuery;

        private readonly Action _completionHandler;
        private readonly IDataMapper<TData, TSource> _dataMapper;
        private readonly Func<TSource, TData> _dataMapperFunc;
        private readonly Action _outOfServiceCompletionHandler;
        private readonly IUpdateMapper<TData, TUpdate> _updateMapper;
        private readonly Func<Task<TUpdate>> _updateEndpoint;
        private readonly Action _updateCompletionHandler;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DownloadManager(Func<Task<TSource>> dataSourceQuery, Action completionHandler = null, IDataMapper<TData, TSource> dataMapper = null, IUpdateMapper<TData, TUpdate> updateMapper = null, Func<Task<TUpdate>> updateEndpoint = null, Action updateCompletionHandler = null, Func<TSource, TData> dataMapperFunc = null, Action outOfServiceCompletionHandler = null)
        {
            _dataSourceQuery = dataSourceQuery;
            _completionHandler = completionHandler;
            _dataMapper = dataMapper;
            _dataMapperFunc = dataMapperFunc;
            _outOfServiceCompletionHandler = outOfServiceCompletionHandler;
            _updateMapper = updateMapper;
            _updateEndpoint = updateEndpoint;
            _updateCompletionHandler = updateCompletionHandler;
            Reconnect = new Command(async () => await Handle_Reconnect());

            if(updateMapper == null && updateEndpoint != null)
            {
                if(!typeof(TData).Equals(typeof(TUpdate)))
                {
                    throw new ArgumentException($"{nameof(DownloadManager<TData, TSource, TUpdate>)} can't be constructed with a null {nameof(IUpdateMapper<TData, TUpdate>)} when {nameof(TData)}:{typeof(TData)} is not equal it {nameof(TUpdate)}:{typeof(TUpdate)}");
                }
            }

            if (dataMapper == null && dataMapperFunc == null)
            {
                if(!typeof(TData).Equals(typeof(TSource)))
                {
                    throw new ArgumentException($"{nameof(DownloadManager<TData, TSource, TUpdate>)} can't be constructed with a null {nameof(IDataMapper<TData, TSource>)} when {nameof(TData)}:{typeof(TData)} is not equal to {nameof(TSource)}:{typeof(TSource)}");
                }
            }
        }

        private async Task Handle_Reconnect()
        {
            IsOutOfService = false;
            await FetchData();
        }

        public async Task FetchData()
        {
            IsDownloading = true;

            var result = await _dataSourceQuery();

            if (result == null)
            {
                _outOfServiceCompletionHandler?.Invoke();
                IsOutOfService = true;
                IsDownloading = false;
            }
            else
            {
                IsDownloading = false;
                if(typeof(TData).Equals(typeof(TSource)))
                {
                    Data = result as TData;
                }
                else
                {
                    //Check which dataMapper to use
                    if(_dataMapper != null)
                    {
                        Data = _dataMapper.Map(result);
                    }
                    else
                    {
                        Data = _dataMapperFunc(result);
                    }
                }
                if(_completionHandler != null)
                {
                    _completionHandler();
                }
            }
        }

        public void UpdateHandler(object sender, System.EventArgs e)
        {
            if (IsOutOfService || Data == null) //Dont update if the DownloadManager is out ofservice and or the data was never downloaded or havn't been yet
            {
                return;
            }

            //Ensure configurations are valid
            if (_updateMapper == null || _updateEndpoint == null)
            {
                throw new MissingMemberException($"Can't invoke {nameof(UpdateHandler)} with null value {nameof(IUpdateMapper<TData, TUpdate>)}_type:{typeof(IUpdateMapper<TData, TUpdate>)} and or null {nameof(_updateEndpoint)}. SOLUTION --> Specify both {nameof(IUpdateMapper<TData, TUpdate>)} and {nameof(_updateEndpoint)}");
            }

            Task.Run(UpdateData);
        }

        private async Task UpdateData()
        {
            var result = await _updateEndpoint();

            //Ignore if we fail to update, dont bother the user
            if(result != null)
            {
                _updateMapper.UpdateData(Data, result);

                if(_updateCompletionHandler != null)
                {
                    _updateCompletionHandler();
                }
            }
        }
        
        public void AssertConfigurations()
        {
        }
    }
}
