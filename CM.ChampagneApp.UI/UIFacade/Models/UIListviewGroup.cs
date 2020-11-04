using System;
using System.ComponentModel;
namespace CM.ChampagneApp.UI.UIFacade.Models
{
	public class UIListviewGroup<T> : INotifyPropertyChanged
    {
        public UIListviewGroup()
        {
        }

		public event PropertyChangedEventHandler PropertyChanged;

		public T Entity1 { get; set; }
        public T Entity2 { get; set; }
       
        public bool IsEntity1Visible { get; set; } = false;
        public bool IsEntity2Visible { get; set; } = false;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
	}
}
