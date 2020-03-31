namespace Codefarts.AppCore.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a collection that is observable.
    /// </summary>
    /// <typeparam name="T">The type of elements contained in the collection.</typeparam>
    public interface IBindableCollection<T>
    {
        /// <summary>
        /// Adds a collection of items.
        /// </summary>
        /// <param name="items">The items to be added.</param>
        void AddRange(IEnumerable<T> items);

        /// <summary>
        /// Removes the items from the collection.
        /// </summary>
        /// <param name="items">The items to be removed.</param>
        void RemoveRange(IEnumerable<T> items);
    }
}
