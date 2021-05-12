// <copyright file="PropertyChangedEventArgs.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.AppCore.EventArgs
{
    using System.ComponentModel;

    /// <summary>
    /// Provides event args for a <see cref="INotifyPropertyChanged.PropertyChanged"/> event.
    /// </summary>
    /// <typeparam name="T">Specifies property type.</typeparam>
    public class PropertyChangedEventArgs<T> : PropertyChangedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyChangedEventArgs{T}"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="oldValue">The previous property value.</param>
        /// <param name="newValue">The new property value.</param>
        public PropertyChangedEventArgs(string propertyName, T oldValue, T newValue)
            : base(propertyName)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        /// <summary>
        /// Gets the previous property value.
        /// </summary>
        public virtual T OldValue
        {
            get;
        }

        /// <summary>
        /// Gets the new property value that was assigned.
        /// </summary>
        public virtual T NewValue
        {
            get;
        }
    }
}