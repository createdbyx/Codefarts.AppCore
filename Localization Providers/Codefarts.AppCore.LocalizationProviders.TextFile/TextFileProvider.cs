// <copyright file="TextFileProvider.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.AppCore.LocalizationProviders.TextFileCore31
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Codefarts.AppCore.Interfaces;

    public class TextFileProvider : ILocalizationProvider
    {
        public const string LanguagesFolderKey = "SETT_LanguagesFolder";

        /// <summary>Initializes a new instance of the <see cref="TextFileProvider"/> class.</summary>
        /// <param name="languagesFolder">The languages folder where the localization files exist.</param>
        /// <exception cref="System.ArgumentNullException">If the languagesFolder parameter is null.</exception>
        public TextFileProvider(ISettingsProvider settingProvider)
        {
            if (settingProvider == null)
            {
                throw new ArgumentNullException(nameof(settingProvider));
            }

            var folder = this.LanguagesFolder;
            if (settingProvider.TryGetSetting(LanguagesFolderKey, out folder))
            {
                this.LanguagesFolder = folder;

            }
            else
            {
                throw new NoLanguagesFolderSettingFound();
            }
        }

        /// <summary>Gets the languages folder.</summary>
        /// <value>The languages folder.</value>
        public string LanguagesFolder
        {
            get; private set;
        }

        /// <summary>The last write time of the file that was read.</summary>
        private DateTime lastWriteTime = DateTime.MinValue;

        /// <summary>Stores all the localized strings.</summary>
        private IDictionary<string, string> dataStore = new Dictionary<string, string>();

        /// <summary>Reads the localization file.</summary>
        /// <exception cref="System.IO.FileNotFoundException">Could not find language file.</exception>
        /// <remarks>Only reads the localization file if the file has been written to since it was last read.</remarks>
        private void Read()
        {
            var file = System.IO.Path.Combine(this.LanguagesFolder, System.Globalization.CultureInfo.CurrentCulture.Name);
            file = System.IO.Path.ChangeExtension(file, ".txt");
            if (!System.IO.File.Exists(file))
            {
                throw new System.IO.FileNotFoundException("Could not find language file.", file);
            }

            var writeTime = System.IO.File.GetLastWriteTime(file);
            if (writeTime <= this.lastWriteTime)
            {
                return;
            }

            using (var fileStream = System.IO.File.OpenRead(file))
            {
                this.dataStore = this.ReadData(fileStream);
            }

            this.lastWriteTime = writeTime;
        }

        /// <summary>Reads the localization data from a stream.</summary>
        /// <param name="stream">The stream to read the localization data from.</param>
        /// <returns>A <see cref="IDictionary{TKey,TValue}"/> of string key and value pairs.</returns>
        private IDictionary<string, string> ReadData(System.IO.Stream stream)
        {
            // read all lines
            var data = new Dictionary<string, string>();
            using (var reader = new System.IO.StreamReader(stream))
            {
                var lines = reader.ReadToEnd().Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    var index = line.IndexOf(" ", StringComparison.Ordinal);
                    if (index == -1)
                    {
                        continue;
                    }

                    var key = line.Substring(0, index).Trim();
                    var value = line.Substring(index).Trim();
                    value = value.Replace(@"\r\n", "\r\n");
                    data.Add(key, value);
                }
            }

            return data;
        }

        /// <summary>Gets the localized string.</summary>
        /// <param name="key">The key for the localized string.</param>
        /// <returns>The value of the localized string.</returns>
        public string GetString(string key)
        {
            this.Read();
            return this.dataStore[key];
        }

        public void Register(CultureInfo culture, IDictionary<string, string> entries, bool replace)
        {
            foreach (var entry in entries)
            {
                if (replace)
                {
                    this.dataStore[entry.Key] = entry.Value;
                }
                else
                {
                    if (!this.dataStore.ContainsKey(entry.Key))
                    {
                        this.dataStore.Add(entry.Key, entry.Value);
                    }
                }
            }
        }
    }
}
