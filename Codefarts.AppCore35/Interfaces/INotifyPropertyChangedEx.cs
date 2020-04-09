// <copyright file="INotifyPropertyChangedEx.cs" company="Codefarts">
// Copyright (c) Codefarts
// </copyright>

namespace Codefarts.AppCore.Interfaces
{
    using System.ComponentModel;

    /// <summary>
    /// Extends <see cref="INotifyPropertyChanged" /> such that the change event can be raised by external parties.
    /// </summary>
    public interface INotifyPropertyChangedEx : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets a value indicating whether property changes will raise notification events.
        /// </summary>
        bool IsNotifying { get; set; }

        /// <summary>
        /// Notifies subscribers of the property change.
        /// </summary>
        /// <param name="propertyName">Name of the property that changed.</param>
        void NotifyOfPropertyChange(string propertyName);

        /// <summary>
        /// Raises a change notification indicating that collection has changed.
        /// </summary>
        /// <remarks>Useful for when you make sweeping changes to a collection and want any UI binding to update.</remarks>
        void Refresh();
    }
}
