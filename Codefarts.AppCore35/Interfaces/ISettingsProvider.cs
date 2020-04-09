// <copyright file="ISettingsProvider.cs" company="Codefarts">
// Copyright (c) Codefarts
// </copyright>

namespace Codefarts.AppCore.Interfaces
{
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
        /// Gets a setting value.
        /// </summary>
        /// <typeparam name="T">The type to be returned.</typeparam>
        /// <param name="key">The name of the setting.</param>
        /// <param name="defaultValue">The default value to return if there was a problem returning the setting value.</param>
        /// <returns>The value of the setting.</returns>
        T GetSetting<T>(string key, T defaultValue);

        /// <summary>
        /// Tries the get a setting value.
        /// </summary>
        /// <typeparam name="T">The type to be returned.</typeparam>
        /// <param name="key">The name of the setting.</param>
        /// <param name="value">The setting value to be returned.</param>
        /// <returns>true if successful; otherwise false.</returns>
        bool TryGetSetting<T>(string key, out T value);

        /// <summary>
        /// Sets a setting value.
        /// </summary>
        /// <typeparam name="T">The type to be set.</typeparam>
        /// <param name="key">The name of the setting.</param>
        /// <param name="value">The setting value to store.</param>
        void SetSetting<T>(string key, T value);

        void Draw();
    }
}
