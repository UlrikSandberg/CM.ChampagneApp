using System;
using System.Collections;
using System.Collections.Generic;

namespace CM.ChampagneApp.UI.Pages.Commons
{
    public interface ICollectionViewDataResolver<T>
    {
        void ItemSource_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e);
        void ResolveNewValues(IEnumerable newValues);
        IEnumerable GetItemSource();
        void ClearItemSource();
    }
}
