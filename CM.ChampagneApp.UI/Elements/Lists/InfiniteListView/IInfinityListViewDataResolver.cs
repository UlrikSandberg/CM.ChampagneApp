using System;
using System.Collections;
using System.Collections.ObjectModel;

namespace CM.ChampagneApp.UI.Elements.Lists.InfiniteListView
{
    public interface IInfinityListViewDataResolver
    {
        void ResolveNewValues(IEnumerable newValues);
        IEnumerable GetCustomItemSource();
        IEnumerable GetOriginalItemSource();

    }
}
