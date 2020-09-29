// <copyright file="SettingChangedEventHandlerArgs.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.AppCore.EventArgs
{
    using System;
    using Codefarts.AppCore.Interfaces;

    /// <summary>
    /// Provides an argument class for the <seealso cref="ISettingsProvider.SettingChanged"/> event.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class SettingChangedEventHandlerArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingChangedEventHandlerArgs"/> class.
        /// </summary>
        /// <param name="key">The setting key.</param>
        /// <param name="value">The value that changed.</param>
        public SettingChangedEventHandlerArgs(string key, object value)
        {
            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the setting key.
        /// </summary>
        public string Key
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the value that changed.
        /// </summary>
        public object Value
        {
            get;
            private set;
        }
    }
}