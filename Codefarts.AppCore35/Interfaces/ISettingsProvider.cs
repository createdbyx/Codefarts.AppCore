namespace Codefarts.AppCore.Interfaces
{
    using System;

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

    /// <summary>
    /// Provides a delegate for the <see cref="ISettingsProvider.SettingChanged"/> event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The arguments containing information about what setting changed.</param>
    public delegate void SettingChangedEventHandler(object sender, SettingChangedEventHandlerArgs args);

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
        /// Gets the value that changed.
        /// </summary>
        public object Value
        {
            get;
            private set;
        }
    }
}
