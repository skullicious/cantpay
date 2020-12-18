using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace CantPay.Helpers
{
   public class ObservableRangeCollection<T> : ObservableCollection<T>
   {
        //Initialize new instance of Observable Collection
        public ObservableRangeCollection()
           : base()
        {
        }

        //Initializes collection of ObservableCollections
        public ObservableRangeCollection(IEnumerable<T> collection)
            :base(collection)
        {
        }

        //Add elements to the end of ObservableCollection
        public void AddRange(IEnumerable<T> collection, NotifyCollectionChangedAction notificationMode = NotifyCollectionChangedAction.Add)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            CheckReentrancy();

            if (notificationMode == NotifyCollectionChangedAction.Reset)
            {
                foreach (var i in collection)
                {
                    Items.Add(i);
                }

                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Count"));
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Item[]"));
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

                return;

            }

            int startIndex = Count;
            var changedItems = collection is List<T> ? (List<T>)collection : new List<T>(collection);
            foreach (var i in changedItems)
            {
                Items.Add(i);
            }

            OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, changedItems, startIndex));

        }


        //Removes first occurence of each item in collection from ObservableCollection
        public void RemoveRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentException("collection");

            foreach (var i in collection)
                Items.Remove(i);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
      
        //Clears the current collection and replaces with specified item
        public void Replace(T item)
        {
            ReplaceRange(new T[] { item });
        }

        //Clears current collection and replaces it with collections
        public void ReplaceRange(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            Items.Clear();
            AddRange(collection, NotifyCollectionChangedAction.Reset);

        }
        
    }
}
