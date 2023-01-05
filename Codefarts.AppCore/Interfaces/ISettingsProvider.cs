// <copyright file="ISettingsProvider.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.AppCore.Interfaces
{
    using System.Collections.ObjectModel;
    using Codefarts.AppCore.EventHandlers;

    /// <summary>
    /// Provides an interface for storing and retrieving settings.
    /// </summary>
    public interface ISettingsProvider
    {
        /// <summary>
        /// Occurs when a setting is changed.
        /// </summary>
        event SettingChangedEventHandler SettingChanged;

        /// <summary>
        /// Gets the setting keys.
        /// </summary>
        ReadOnlyCollection<string> SettingKeys { get; }

        /// <summary>
        /// Gets a setting value.
        /// </summary>
        /// <typeparam name="T">The type to be returned.</typeparam>
        /// <param name="key">The name of the setting.</param>
        /// <returns>The value of the setting.</returns>
        T GetSetting<T>(string key);

        /// <summary>
        /// Sets a setting value.
        /// </summary>
        /// <typeparam name="T">The type to be set.</typeparam>
        /// <param name="key">The name of the setting.</param>
        /// <param name="value">The setting value to store.</param>
        void SetSetting<T>(string key, T value);
    }
}