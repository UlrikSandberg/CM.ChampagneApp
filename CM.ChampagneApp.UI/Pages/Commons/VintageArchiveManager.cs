using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CM.ChampagneApp.UI.UIFacade.Models;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.Commons
{
    public class VintageArchiveManager<T> : BaseUIModel
    {
        public ObservableCollection<T> ItemSource { get; set; } = new ObservableCollection<T>();

        public bool IsDownloading { get; set; }
        public bool VintageArchiveIsVisible { get; set; } = false;
        public bool IsVintageArchiveOutOfServiceTextVisible { get; set; }

        public string VintageArchiveTitle { get; set; }
        public string VintageArchiveDescription { get; set; }

        public ICommand CancelVintageArchive { get; set; }
        public ICommand VintageArchiveItemClicked { get; set; }

        private readonly Func<Guid, Task<IEnumerable<T>>> _dataSourceQuery;
        private readonly Func<Task<IEnumerable<T>>> _parameterLessDataSourceQuery;
        private readonly Func<IEnumerable<T>, IEnumerable<T>> _preDisplayDataManipulater;

        public VintageArchiveManager(Func<Guid, Task<IEnumerable<T>>> dataSourceQuery, Func<IEnumerable<T>, IEnumerable<T>> preDisplayDataManipulater = null)
        {
            _dataSourceQuery = dataSourceQuery;
            _preDisplayDataManipulater = preDisplayDataManipulater;
            CancelVintageArchive = new Command(Handle_CancelVintageArchive);
        }

        public VintageArchiveManager(Func<Task<IEnumerable<T>>> dataSourceQuery, Func<IEnumerable<T>, IEnumerable<T>> preDisplayDataManipulater = null)
        {
            _parameterLessDataSourceQuery = dataSourceQuery;
            _preDisplayDataManipulater = preDisplayDataManipulater;
            CancelVintageArchive = new Command(Handle_CancelVintageArchive);
        }

        public async Task DownloadVintageArchive(Guid champagneRootId, string folderContentType)
        {
            VintageArchiveIsVisible = true;
            IsDownloading = true;
            if (folderContentType.Equals("Vintage"))
            {
                VintageArchiveTitle = "Vintage Archive";
                VintageArchiveDescription = "Choose a vintage";
            }
            else
            {
                VintageArchiveTitle = "Edition Archive";
                VintageArchiveDescription = "Choose an edition";
            }

            var dataList = _dataSourceQuery != null ? await _dataSourceQuery(champagneRootId) : await _parameterLessDataSourceQuery();

            if (dataList != null)
            {
                ItemSource = _preDisplayDataManipulater == null ? new ObservableCollection<T>(dataList) : new ObservableCollection<T>(_preDisplayDataManipulater(dataList));
            }
            else
            {
                IsVintageArchiveOutOfServiceTextVisible = true;
            }
            IsDownloading = false;
        }

        public async Task DownloadVintageArchive()
        {
            VintageArchiveIsVisible = true;
            IsDownloading = true;

            var dataList = await _parameterLessDataSourceQuery();

            if (dataList != null)
            {
                ItemSource = _preDisplayDataManipulater == null ? new ObservableCollection<T>(dataList) : new ObservableCollection<T>(_preDisplayDataManipulater(dataList));
            }
            else
            {
                IsVintageArchiveOutOfServiceTextVisible = true;
            }
            IsDownloading = false;
        }

        private void Handle_CancelVintageArchive()
        {
            IsVintageArchiveOutOfServiceTextVisible = false;
            VintageArchiveIsVisible = false;
            if (ItemSource != null)
                ItemSource.Clear();
            VintageArchiveTitle = "";
            VintageArchiveDescription = "";
        }
    }
}
