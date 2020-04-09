// <copyright file="ILocalizationProvider.cs" company="Codefarts">
// Copyright (c) Codefarts
// </copyright>

namespace Codefarts.AppCore.Interfaces
{
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Provides a date source interface.
    /// </summary>
    public interface ILocalizationProvider
    {
        /// <summary>Gets a localized string.</summary>
        /// <param name="key">The key for the localized string.</param>
        /// <returns>The value of the localized string.</returns>
        string GetString(string key);

        /// <summary>
        /// Registers a dataset of localized strings from a <see cref="IDictionary{TKey,TValue}"/> reference.
        /// </summary>
        /// <param name="culture">The <see cref="CultureInfo"/> that the strings will be registered into.</param>
        /// <param name="entries">A <see cref="IDictionary{TKey,TValue}"/> containing the localized strings to add.</param>
        /// <remarks>This method will override any existing key values.</remarks>
        void Register(CultureInfo culture, IDictionary<string, string> entries);

        /// <summary>
        /// Registers a dataset of localized strings from a <see cref="IDictionary{TKey,TValue}"/> reference.
        /// </summary>
        /// <param name="culture">
        /// The <see cref="CultureInfo"/> that the strings will be registered into.
        /// </param>
        /// <param name="entries">
        /// A <see cref="IDictionary{TKey,TValue}"/> containing the localized strings to add.
        /// </param>
        /// <param name="replace">
        /// If true existing key values will be replaced. Otherwise the existing values will be left as they are.
        /// </param>
        void Register(CultureInfo culture, IDictionary<string, string> entries, bool replace);
    }
}