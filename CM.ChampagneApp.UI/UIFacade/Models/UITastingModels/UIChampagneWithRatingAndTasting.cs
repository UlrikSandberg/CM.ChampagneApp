using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace CM.ChampagneApp.UI.UIFacade.Models.UITastingModels
{
	public class UIChampagneWithRatingAndTasting : INotifyPropertyChanged
    {

		public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

		public Guid Id { get; set; }
        public Guid BrandId { get; set; }

        public string BottleName { get; set; }
        public string BrandName { get; set; }

        public List<double> Ratings { get; set; }
		public List<int> RatingByStars { get; set; }

        public bool IsBookmarkedByRequester { get; set; }
        public bool IsTastedByRequester { get; set; }

        public IEnumerable<UITasting> Tastings { get; set; }

        public UIChampagneWithRatingAndTasting()
        {
        }
    }
}
