using System;
namespace CM.ChampagneApp.UI.CustomEventArgs
{
    public class ModelClickedEventArgs<T>
    {
        public T SelectedModel { get; }

        public ModelClickedEventArgs(T model)
        {
            SelectedModel = model;
        }
    }
}
