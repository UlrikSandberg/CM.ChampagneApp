using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.UIFacade.Models.UIChampagneModels
{
	public class UIChampagneRootGroup : INotifyPropertyChanged
    {
		public UIChampagneRootGroup()
        {
			IsChampagneRoot1Visible = false;
			IsChampagneRoot2Visible = false;
        }

		public event PropertyChangedEventHandler PropertyChanged;

		public UIChampagneRoot ChampagneRoot1 { get; set; }

		public UIChampagneRoot ChampagneRoot2 { get; set; }


        public bool IsChampagneRoot1Visible { get; set; } = false;
        public bool IsChampagneRoot2Visible { get; set; } = false;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

	}
}

