/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

namespace Codefarts.CoreProjectCode
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
