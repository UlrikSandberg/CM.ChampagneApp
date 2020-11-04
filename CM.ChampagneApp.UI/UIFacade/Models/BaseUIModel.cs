using System;
using System.ComponentModel;
using CM.ChampagneApp.Business.Configuration;
namespace CM.ChampagneApp.UI.UIFacade.Models
{
	public abstract class BaseUIModel : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected IAppConfiguration Configuration = App.AppConfig;

        public BaseUIModel()
        {
        }
    }
}
