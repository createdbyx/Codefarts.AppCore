// <copyright file="XmlSettingsProvider.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.AppCore.SettingProviders.Xml
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using Codefarts.AppCore.EventArgs;
    using Codefarts.AppCore.EventHandlers;
    using Codefarts.AppCore.Interfaces;

    /// <summary>
    /// Provides a XML file based <see cref="ISettingsProvider"/> implementation.
    /// </summary>
    public class XmlSettingsProvider : ISettingsProvider, IDisposable
    {
        /// <summary>
        /// The data store.
        /// </summary>
        private Dictionary<string, object> dataStore = new Dictionary<string, object>();

        /// <summary>
        /// The last write time.
        /// </summary>
        private DateTime lastWriteTime = DateTime.MinValue;

        private DateTime lastReadTime = DateTime.MinValue;

        /// <summary>
        /// Gets an array of setting names.
        /// </summary>
        /// <returns>Returns an array of setting names.</returns>
        public string[] GetValueKeys()
        {
            this.Read();
            return this.dataStore.Keys.ToArray();
        }

        private int readDelayInSeconds;
        private bool alreadyDisposed;

        /// <inheritdoc/>
        public event SettingChangedEventHandler SettingChanged;

        public virtual int ReadDelayInSeconds
        {
            get
            {
                return this.readDelayInSeconds;
            }

            set
            {
                this.readDelayInSeconds = Math.Max(0, value);
            }
        }

        private IEnumerable<KeyValuePair<string, object>> ReadSettings(string file, bool filterDuplicates)
        {
            var xml = new XmlDocument();
            using (var stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                xml.Load(stream);
            }

            if (xml.DocumentElement == null || xml.DocumentElement.Name != "settings")
            {
                throw new FileLoadException("Settings file root node is not \"settings\"!");
            }

            var results = from x in xml.DocumentElement.ChildNodes.OfType<XmlNode>()
                          where x.Name == "entry" && x.Attributes != null && x.Attributes.Count > 0
                          let key = x.Attributes["key"]
                          where key != null && !string.IsNullOrWhiteSpace(key.Value)
                          select new KeyValuePair<string, object>(key.InnerText, x.InnerText);

            if (filterDuplicates)
            {
                var list = new List<KeyValuePair<string, object>>();
                foreach (var pair in results)
                {
                    if (list.All(x => x.Key != pair.Key))
                    {
                        list.Add(pair);
                    }
                }

                return list;
            }

            return results;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlSettingsProvider"/> class.
        /// </summary>
        /// <param name="fileName">
        /// The settings file name.
        /// </param>
        public XmlSettingsProvider(IPlatformProvider provider)
        {
            var platformData = provider.GetPlatformData();
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.DoNotVerify);
            var appFilename = Path.GetFileNameWithoutExtension(platformData.ApplicationPath);
            var fileName = Path.Combine(folderPath, appFilename);
            fileName = Path.ChangeExtension(fileName, "xml");
            this.InitalizeSettingsFile(fileName, true);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlSettingsProvider"/> class.
        /// </summary>
        /// <param name="fileName">
        /// The settings file name.
        /// </param>
        public XmlSettingsProvider(string fileName)
            : this(fileName, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlSettingsProvider"/> class.
        /// </summary>
        /// <param name="fileName">
        /// The settings file name.
        /// </param>
        /// <param name="create"><c>true</c> Create the settings file immediately if one does not exist.</param>
        /// <exception cref="ArgumentNullException">
        /// If <see cref="fileName"/> is null or empty.
        /// </exception>
        public XmlSettingsProvider(string fileName, bool create)
        {
            this.InitalizeSettingsFile(fileName, create);
        }

        private void InitalizeSettingsFile(string fileName, bool create)
        {
            if (this.alreadyDisposed)
            {
                throw new ObjectDisposedException(nameof(XmlSettingsProvider));
            }
            
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName), $"'{nameof(fileName)}' argument was missing, empty, or null.");
            }

            var directoryName = Path.GetDirectoryName(fileName);
            if (directoryName != null && directoryName.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                throw new ArgumentException("Invalid path characters detected!");
            }

            var name = Path.GetFileName(fileName);
            if (name != null && name.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
            {
                throw new ArgumentException("Invalid filename characters detected!");
            }

            this.readDelayInSeconds = 5;
            this.FileName = fileName;

            if (create && !File.Exists(fileName))
            {
                var doc = new XmlDocument();
                var declaration = doc.CreateXmlDeclaration("1.0", null, null);

                var settings = doc.CreateElement("settings");
                doc.AppendChild(settings);
                doc.InsertBefore(declaration, doc.DocumentElement);

                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }

                this.DoSaveDocument(doc, fileName);
            }

            this.Read();
        }

        /// <summary>
        /// Gets the file name.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Reads values into the <see cref="dataStore"/> filed using linq.
        /// </summary>
        private void Read()
        {
            // only update reading settings file every 5 seconds
            if (DateTime.Now < this.lastReadTime + TimeSpan.FromSeconds(this.ReadDelayInSeconds))
            {
                return;
            }

            if (!File.Exists(this.FileName))
            {
                throw new FileNotFoundException("Could not find settings file.", this.FileName);
            }

#if !UNITY_WEBPLAYER
            var info = new FileInfo(this.FileName);
            var writeTime = info.LastWriteTime;

            // check if the file has been written to since last read attempt
            if (writeTime <= this.lastWriteTime)
            {
                return;
            }
#endif

            var results = this.ReadSettings(this.FileName, true);

            this.dataStore = results.ToDictionary(k => k.Key, v => v.Value);

#if !UNITY_WEBPLAYER
            this.lastWriteTime = writeTime;
#endif
            this.lastReadTime = DateTime.Now;
        }

        /// <summary>
        /// Saves current values in the <see cref="dataStore"/> to a xml file using linq.
        /// </summary>
        private void Write()
        {
            var directoryName = Path.GetDirectoryName(this.FileName);

            if (directoryName != null && !Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            var doc = new XmlDocument();
            var declaration = doc.CreateXmlDeclaration("1.0", null, null);

            var settings = doc.CreateElement("settings");
            doc.AppendChild(settings);
            doc.InsertBefore(declaration, doc.DocumentElement);

            // read existing settings file values
            var existingValues = this.ReadSettings(this.FileName, true);

            var comparer = EqualityComparerCallback<KeyValuePair<string, object>>.Compare((x, y) => string.CompareOrdinal(x.Key, y.Key) == 0);
            var entries = this.dataStore.Union(existingValues, comparer);

            var nodesToWrite = entries.OrderBy(x => x.Key).Select(x =>
            {
                var entry = doc.CreateElement("entry");
                entry.InnerText = x.Value.ToString();
                var key = doc.CreateAttribute("key");
                key.InnerText = x.Key;
                entry.Attributes.Append(key);
                return entry;
            });

            foreach (var node in nodesToWrite)
            {
                settings.AppendChild(node);
            }

            this.DoSaveDocument(doc, this.FileName);

#if !UNITY_WEBPLAYER
            this.lastWriteTime = File.GetLastWriteTime(this.FileName);
#endif
        }

        private void DoSaveDocument(XmlDocument doc, string fileName)
        {
            var fileMode = File.Exists(fileName) ? FileMode.Truncate : FileMode.CreateNew;
            using (var stream = new FileStream(fileName, fileMode, FileAccess.Write, FileShare.None))
            {
                doc.Save(stream);
            }
        }

        public T GetSetting<T>(string key)
        {
            if (this.alreadyDisposed)
            {
                throw new ObjectDisposedException(nameof(XmlSettingsProvider));
            }
            
            try
            {
                return (T)this.dataStore[key];
            }
            catch (Exception e)
            {
                throw new SettingException($"Failed to retrieve setting '{key}'.", e);
            }
        }

        public void SetSetting<T>(string key, T value)
        {
            if (this.alreadyDisposed)
            {
                throw new ObjectDisposedException(nameof(XmlSettingsProvider));
            }
            
            this.dataStore[key] = value;
            this.Write();

            // TODO: Determine if setting actually changed before raising event otherwise it will fire every change
            this.OnSettingChanged(new SettingChangedEventHandlerArgs(key, value));
        }

        public ReadOnlyCollection<string> SettingKeys
        {
            get
            {
                {
                    if (this.alreadyDisposed)
                    {
                        throw new ObjectDisposedException(nameof(XmlSettingsProvider));
                    }
                    
                    return new ReadOnlyCollection<string>(this.dataStore.Keys.ToArray());
                }
            }
        }

        protected virtual void OnSettingChanged(SettingChangedEventHandlerArgs args)
        {
            var handler = this.SettingChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        public void Dispose()
        {
            if(this.alreadyDisposed)
                return;
            
            this.Write();

            this.alreadyDisposed = true;
        }
    }
}