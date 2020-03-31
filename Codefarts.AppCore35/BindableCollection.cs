namespace Codefarts.AppCore
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;

    /// <summary>
    /// A base collection class that supports automatic UI thread marshaling.
    /// </summary>
    /// <typeparam name="T">The type of elements contained in the collection.</typeparam>
    /// <remarks>Any action that changes the collection will happen synchronously on the UI Thread via <see cref="PlatformProvider.Current"/>.</remarks>
    public class BindableCollection<T> : ObservableCollection<T>, IBindableCollection<T>, INotifyPropertyChangedEx
    {
        /// <summary>
        /// The backing filed for the <see cref="IsNotifying"/> property.
        /// </summary>
        private bool isNotifying;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableCollection{T}" /> class.
        /// </summary>
        public BindableCollection()
        {
            this.IsNotifying = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableCollection{T}" /> class.
        /// </summary>
        /// <param name="collection">The collection from which the elements are copied.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="collection" /> parameter cannot be <see langword="null" />.</exception>
        public BindableCollection(IEnumerable<T> collection)
            : base(collection)
        {
            this.IsNotifying = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether property change notifications are raised.
        /// </summary>
        public bool IsNotifying
        {
            get
            {
                return this.isNotifying;
            }

            set
            {
                var currentValue = this.isNotifying;
                if (currentValue != value)
                {
                    this.isNotifying = value;
                    base.OnPropertyChanged(new PropertyChangedEventArgs(nameof(this.IsNotifying)));
                }
            }
        }

        /// <summary>
        /// Notifies subscribers of the property change.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public virtual void NotifyOfPropertyChange(string propertyName)
        {
            if (this.IsNotifying)
            {
                var provider = PlatformProvider.Current;
                if (provider != null)
                {
                    provider.OnUIThread(() => this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName)));
                }
            }
        }

        /// <summary>
        /// Raises a change notification indicating that all bindings should be refreshed.
        /// </summary>
        /// <remarks>Raises 2 <seealso cref="ObservableCollection{T}.PropertyChanged"/> events in the following order.
        /// <see cref="Collection{T}.Count"/>, then <see cref="Collection{T}.this"/>, then raises a <see cref="ObservableCollection{T}.CollectionChanged"/>
        /// event with the <seealso cref="NotifyCollectionChangedAction.Reset"/> argument.</remarks>
        public void Refresh()
        {
            var provider = PlatformProvider.Current;
            if (provider != null)
            {
                provider.OnUIThread(() =>
                {
                    this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                    this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                    this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                });

                return;
            }

            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        /// Adds a collection of items.
        /// </summary>
        /// <param name="items">The items to be added.</param>
        public virtual void AddRange(IEnumerable<T> items)
        {
            var provider = PlatformProvider.Current;
            if (provider != null)
            {
                provider.OnUIThread(() => { this.AddRangeInternal(items); });
                return;
            }

            this.AddRangeInternal(items);
        }

        /// <summary>
        /// Adds a collection of items.
        /// </summary>
        /// <param name="items">The items to be added.</param>
        /// <remarks>This method is private not meant to be called from outside the <see cref="BindableCollection{T}"/> class.</remarks>
        private void AddRangeInternal(IEnumerable<T> items)
        {
            var previousNotificationSetting = this.IsNotifying;
            this.IsNotifying = false;
            foreach (var item in items)
            {
                this.InsertItemBase(this.Count, item);
            }

            this.IsNotifying = previousNotificationSetting;

            this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        /// Removes the items from the collection.
        /// </summary>
        /// <param name="items">The items to be removed.</param>
        public virtual void RemoveRange(IEnumerable<T> items)
        {
            var provider = PlatformProvider.Current;
            if (provider != null)
            {
                provider.OnUIThread(() => { this.RemoveRangeInternal(items); });
                return;
            }

            this.RemoveRangeInternal(items);
        }

        /// <summary>
        /// Removes the items from the collection.
        /// </summary>
        /// <param name="items">The items to be removed.</param>
        /// <remarks>This method is private not meant to be called from outside the <see cref="BindableCollection{T}"/> class.</remarks>
        private void RemoveRangeInternal(IEnumerable<T> items)
        {
            var previousNotificationSetting = this.IsNotifying;
            this.IsNotifying = false;
            foreach (var item in items)
            {
                var index = this.IndexOf(item);
                if (index >= 0)
                {
                    this.RemoveItemBase(index);
                }
            }

            this.IsNotifying = previousNotificationSetting;

            this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        /// Inserts the item to the specified position.
        /// </summary>
        /// <param name="index">The index to insert at.</param>
        /// <param name="item">The item to be inserted.</param>
        protected sealed override void InsertItem(int index, T item)
        {
            var provider = PlatformProvider.Current;
            if (provider != null)
            {
                provider.OnUIThread(() => this.InsertItemBase(index, item));
                return;
            }

            this.InsertItemBase(index, item);
        }

        /// <summary>
        /// Exposes the base implementation of the <see cref="InsertItem" /> function.
        /// </summary>
        /// <param name="index">The index where insertion will take place.</param>
        /// <param name="item">The item to be inserted.</param>
        /// <remarks>
        /// Used to avoid compiler warning regarding unverifiable code.
        /// </remarks>
        protected virtual void InsertItemBase(int index, T item)
        {
            base.InsertItem(index, item);
        }

        /// <summary>
        /// Sets the item at the specified position.
        /// </summary>
        /// <param name="index">The index to set the item at.</param>
        /// <param name="item">The item to set.</param>
        protected sealed override void SetItem(int index, T item)
        {
            var provider = PlatformProvider.Current;
            if (provider != null)
            {
                provider.OnUIThread(() => this.SetItemBase(index, item));
                return;
            }

            this.SetItemBase(index, item);
        }

        /// <summary>
        /// Exposes the base implementation of the <see cref="SetItem" /> function.
        /// </summary>
        /// <param name="index">The index where the item is to be set.</param>
        /// <param name="item">The item to be set.</param>
        /// <remarks>
        /// Used to avoid compiler warning regarding unverifiable code.
        /// </remarks>
        protected virtual void SetItemBase(int index, T item)
        {
            base.SetItem(index, item);
        }

        /// <summary>
        /// Removes the item at the specified position.
        /// </summary>
        /// <param name="index">The position used to identify the item to remove.</param>
        protected sealed override void RemoveItem(int index)
        {
            var provider = PlatformProvider.Current;
            if (provider != null)
            {
                provider.OnUIThread(() => this.RemoveItemBase(index));
                return;
            }

            this.RemoveItemBase(index);
        }

        /// <summary>
        /// Exposes the base implementation of the <see cref="RemoveItem" /> function.
        /// </summary>
        /// <param name="index">The index of the item to be removed.</param>
        /// <remarks>
        /// Used to avoid compiler warning regarding unverifiable code.
        /// </remarks>
        protected virtual void RemoveItemBase(int index)
        {
            base.RemoveItem(index);
        }

        /// <summary>
        /// Clears the items contained by the collection.
        /// </summary>
        protected sealed override void ClearItems()
        {
            var provider = PlatformProvider.Current;
            if (provider != null)
            {
                provider.OnUIThread(this.ClearItemsBase);
                return;
            }

            this.ClearItemsBase();
        }

        /// <summary>
        /// Exposes the base implementation of the <see cref="ClearItems" /> function.
        /// </summary>
        /// <remarks>
        /// Used to avoid compiler warning regarding unverifiable code.
        /// </remarks>
        protected virtual void ClearItemsBase()
        {
            base.ClearItems();
        }

        /// <summary>
        /// Raises the <see cref="ObservableCollection{T}.CollectionChanged" /> event with the provided arguments.
        /// </summary>
        /// <param name="e">Arguments of the event being raised.</param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (this.IsNotifying)
            {
                base.OnCollectionChanged(e);
            }
        }

        /// <summary>
        /// Raises the PropertyChanged event with the provided arguments.
        /// </summary>
        /// <param name="e">The event data to report in the event.</param>
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (this.IsNotifying)
            {
                base.OnPropertyChanged(e);
            }
        }
    }
}
