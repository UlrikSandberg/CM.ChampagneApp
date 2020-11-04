using System;
namespace CM.ChampagneApp.UI.CustomEventArgs
{
    public class ValueChangedEventArgs<T>
    {
        public T OldValue { get; private set; }
        public T NewValue { get; private set; }

        public ValueChangedEventArgs(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
