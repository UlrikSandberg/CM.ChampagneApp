using System;
using System.ComponentModel;

namespace CM.ChampagneApp.UI.Pages.Commons.Helpers
{
    public class BoolToggle<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public T CurrentValue { get; set; }

        public T ToggledValue { get; set; }
        public T UntoggledValue { get; set; }

        private bool _toggleState = false;

        public bool ToggleState
        {
            get
            {
                return _toggleState;
            }
            set
            {
                CurrentValue = value ? ToggledValue : UntoggledValue;
                _toggleState = value;
            }
        }

        public BoolToggle(T toggledValue, T untoggledValue, bool toggleState = false)
        {
            ToggledValue = toggledValue;
            UntoggledValue = untoggledValue;
            ToggleState = toggleState;

            if(!toggleState)
            {
                CurrentValue = untoggledValue;
            }
        }
    }
}
