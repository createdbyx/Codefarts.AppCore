/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

using System;
using System.Collections.Generic;
using System.Linq;
using Codefarts.AppCore.Interfaces;
using Codefarts.AppCore.Unity.Editor.Interfaces;
using Codefarts.AppCore.Unity.Editor.Models;
using Codefarts.IoC;
using UnityEditor;
using UnityEngine;

namespace Codefarts.AppCore.Unity.Editor
{
    /// <summary>
    /// Provides a class for showing settings in the unity preferences window.
    /// </summary>
    public class UnityPreferencesManager
    {
        /// <summary>
        /// Holds a list of <see cref="CallbackModel{T}"/> types that implement <see cref="IRun"/>.
        /// </summary>
        private readonly List<IRun> callbacks;

        /// <summary>
        /// Holds the scroll value for the settings
        /// </summary>
        private Vector2 scroll;

        /// <summary>
        ///  Holds the selected settings index.
        /// </summary>
        private int selectedSettingsIndex;

        // hold an array of pre build titles
        private string[] titles;

        /// <summary>
        /// Initializes static members of the <see cref="UnityPreferencesManager"/> class.
        /// </summary>
        static UnityPreferencesManager()
        {
            Instance = new UnityPreferencesManager();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityPreferencesManager"/> class.
        /// </summary>
        public UnityPreferencesManager()
        {
            this.callbacks = new List<IRun>();
        }

        /// <summary>
        /// Gets a singleton instance of the configuration tool.
        /// </summary>
        public static UnityPreferencesManager Instance
        {
            get;
        }
                                                                                       
#if UNITY_2019
        [SettingsProvider]
        public static SettingsProvider CreateCustomSettingsProvider()
        {
            // First parameter is the path in the Settings window.
            // Second parameter is the scope of this setting: it only appears in the Project Settings window.
            var provider = new SettingsProvider("Codefarts/General", SettingsScope.User)
            {
                label = "General Settings",
                guiHandler = searchContext => { Instance.DrawGUI(); },
                keywords = new HashSet<string>(new[] {"Codefarts", "Core", "General"})
            };

            // By default the last token of the path is used as display name if no label is provided.

            // Create the SettingsProvider and initialize its drawing (IMGUI) function in place:

            // Populate the search keywords to enable smart search filtering and label highlighting:

            return provider;
        }
#else
        /// <summary>
        /// Adds preferences section named "Codefarts" to the Preferences Window.
        /// </summary>
        [PreferenceItem("Codefarts")]
        public static void PreferencesGUI()
        {
            // Preferences GUI
            Instance.DrawGUI();
        }
#endif

        /// <summary>
        /// Registers a <see cref="Action{T}"/> callback.
        /// </summary>
        /// <typeparam name="T">The type of data that the <paramref name="callback"/> takes as a parameter.</typeparam>
        /// <param name="title">The title for the settings.</param>
        /// <param name="callback">A reference to a callback.</param>
        public void Register<T>(string title, Action<T> callback)
        {
            this.Register(title, callback, default);
        }

        /// <summary>
        /// Registers a <see cref="Action{T}"/> callback.
        /// </summary>
        /// <typeparam name="T">
        /// The type of data that the <paramref name="callback"/> takes as a parameter.
        /// </typeparam>
        /// <param name="title">The title for the settings.</param>
        /// <param name="callback">
        /// A reference to a callback.
        /// </param>
        /// <param name="data">
        /// The data that will be passed as a parameter when the <paramref name="callback"/> is invoked.
        /// </param>
        public void Register<T>(string title, Action<T> callback, T data)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException("title");
            }

            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }

            var modal = new SettingsMenuModel<T> { Title = title, Callback = callback, Data = data };
            this.callbacks.Add(modal);

            // rebuild titles
            this.BuildTitles();
        }

        /// <summary>
        /// Registers a <see cref="Action"/> callback.
        /// </summary>
        /// <param name="title">The title for the settings.</param>
        /// <param name="callback">A reference to a callback.</param>
        public void Register(string title, Action callback)
        {
            this.Register<object>(title, x => callback(), null);
        }

        /// <summary>
        /// The the draw GUI.
        /// </summary>
        internal void DrawGUI()
        {
            // get reference to localization manager
            var local = Container.Default.Resolve<ILocalizationProvider>();

            var settings = Container.Default.Resolve<ISettingsProvider>();
            if (settings != null)
            {
                using (new GUILayout.VerticalScope())
                {
                    settings.Draw();
                }
            }

            GUILayout.Label(local.GetString("Settings"));

            this.selectedSettingsIndex = EditorGUILayout.Popup(this.selectedSettingsIndex, this.titles);

            // draw current control
            this.scroll = GUILayout.BeginScrollView(this.scroll, false, false);
            this.callbacks[this.selectedSettingsIndex].Run();
            GUILayout.EndScrollView();

            GUILayout.FlexibleSpace();
        }

        private void BuildTitles()
        {
            this.titles = this.callbacks.Where(x => x.GetType().IsAssignableFrom(typeof(ISettingTitle))).Cast<ISettingTitle>().Select(x => x.Title).ToArray();
        }
    }
}