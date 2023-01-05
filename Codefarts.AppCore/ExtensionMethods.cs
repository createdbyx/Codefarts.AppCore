// <copyright file="ExtensionMethods.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.AppCore
{
    using System;
    using Codefarts.AppCore.Interfaces;

    /// <summary>
    /// provides extension methods for the <see cref="ISettingsProvider"/> type.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>Gets the localized string.</summary>
        /// <param name="provider">The localization provider reference.</param>
        /// <param name="name">The name of the localized string.</param>
        /// <param name="defaultValue">The default value to return if unable to retrieve the localized string.</param>
        /// <returns>The value of the localized string.</returns>
        public static string GetString(this ILocalizationProvider provider, string name, string defaultValue)
        {
            try
            {
                if (provider == null)
                {
                    return defaultValue;
                }

                return provider.GetString(name);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>Gets the setting value.</summary>
        /// <param name="provider">The settings provider provider reference.</param>
        /// <param name="name">The name of the setting.</param>
        /// <param name="defaultValue">The default value to return if unable to retrieve the setting value.</param>
        /// <typeparam name="T">Specifies the property type.</typeparam>
        /// <returns>The value of the setting.</returns>
        public static T GetSetting<T>(this ISettingsProvider provider, string name, T defaultValue)
        {
            T value;
            return provider.TryGetSetting(name, out value) ? value : defaultValue;
        }

        /// <summary>Gets the setting value.</summary>
        /// <param name="provider">The settings provider provider reference.</param>
        /// <param name="name">The name of the setting.</param>
        /// <param name="value">The setting value to return.</param>
        /// <returns>The true if successful; otherwise false.</returns>
        public static bool TryGetSetting<T>(this ISettingsProvider provider, string name, out T value)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            if (!provider.SettingKeys.Contains(name))
            {
                value = default(T);
                return false;
            }

            T retrievedValue;
            try
            {
                var type = typeof(T);
                if (type.IsEnum)
                {
                    retrievedValue = (T)Enum.Parse(type, provider.GetSetting<T>(name).ToString());
                }
                else
                {
                    retrievedValue = (T)provider.GetSetting<T>(name);
                }
            }
            catch (Exception)
            {
                value = default(T);
                return false;
            }

            value = retrievedValue;
            return true;
        }
    }
}
