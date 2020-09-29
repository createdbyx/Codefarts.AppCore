// <copyright file="PropertyChangedEventArgs.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.AppCore.EventArgs
{
    using System.ComponentModel;

    public class PropertyChangedEventArgs<T> : PropertyChangedEventArgs
    {
        public PropertyChangedEventArgs(string propertyName, T oldValue, T newValue)
            : base(propertyName)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        public virtual T OldValue
        {
            get;
        }

        public virtual T NewValue
        {
            get;
        }
    }
}