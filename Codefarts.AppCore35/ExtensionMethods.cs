// <copyright file="ExtensionMethods.cs" company="Codefarts">
// Copyright (c) Codefarts
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
        /// <summary>
        /// Gets a localized string.
        /// </summary>
        /// <param name="provider">The localization provider reference.</param>
        /// <param name="name">The name of the localized string.</param>
        /// <param name="defaultValue">The default value to return if unable to retrieve the localized string.</param>
        /// <returns>The value of the localized string.</returns>
        public static string GetString(this ILocalizationProvider provider, string name, string defaultValue)
        {
            try
            {
                return provider.GetString(name);
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
