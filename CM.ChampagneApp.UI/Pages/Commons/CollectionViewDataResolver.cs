using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CM.ChampagneApp.UI.UIFacade.Models;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser;

namespace CM.ChampagneApp.UI.Pages.Commons
{
    public class CollectionViewDataResolver<T> : ICollectionViewDataResolver<T>
    {
        public ObservableCollection<UIListviewGroup<T>> ItemSource { get; set; }

        public CollectionViewDataResolver()
        {
            ItemSource = new ObservableCollection<UIListviewGroup<T>>();
        }

        public void ClearItemSource()
        {
            ItemSource.Clear();
        }

        public IEnumerable GetItemSource()
        {
            return ItemSource;
        }

        public void ItemSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ResolveNewValues(e.NewItems);
        }

        public void ResolveNewValues(IEnumerable newValues)
        {
            var obslist = new ObservableCollection<T>();

            foreach (var entry in newValues)
            {
                obslist.Add((T)entry);
            }

            AddBucketToList(obslist);
        }

        private bool Contains(T model)
        {
            foreach(var entry in ItemSource)
            {
                if(entry.Entity1 != null)
                {
                    if(entry.Entity1.Equals(model))
                    {
                        return true;
                    }
                }
                if(entry.Entity2 != null)
                {
                    if(entry.Entity2.Equals(model))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void AddBucketToList(ObservableCollection<T> newItems)
        {
            var index = 0;
            var isExcessBucket = false;
            var bucket = new UIListviewGroup<T>();

            if (ItemSource.Count != 0)//Not initial download check if the last bucket is of odd numbers if so fill this bucket first
            {
                if (ItemSource[ItemSource.Count - 1].Entity2 == null)
                {
                    bucket = ItemSource[ItemSource.Count - 1];
                    index = 1;
                    isExcessBucket = true;
                }
            }

            foreach (var item in newItems)
            {
                if (index == 0)
                {
                    bucket.Entity1 = item;
                    bucket.IsEntity1Visible = true;
                }
                else if (index == 1)
                {
                    bucket.Entity2 = item;
                    bucket.IsEntity2Visible = true;

                    // If IsExcessChampagneRootGroup is true this means the download was not initial and last champagneGroups champagne 2 was null
                    // Since we update the last champagneGroup break current iteration and continue at a fresh with index = 0

                    if (isExcessBucket)
                    {
                        index = 0;
                        isExcessBucket = false;
                        bucket = new UIListviewGroup<T>();
                        continue;
                    }
                }
                index++;
                if (index == 2)
                {
                    ItemSource.Add(bucket);
                    index = 0;
                    bucket = new UIListviewGroup<T>();
                }
            }

            //Check if the leep ended with a partially filled bucket due to odd numbers of champagnes
#pragma warning disable RECS0017 // Possible compare of value type with 'null'
            if (bucket.Entity1 != null && bucket.Entity2 == null)
#pragma warning restore RECS0017 // Possible compare of value type with 'null'
            {
                ItemSource.Add(bucket);
            }
        }
    }
}
