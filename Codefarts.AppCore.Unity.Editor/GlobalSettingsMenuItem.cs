/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

namespace Codefarts.AppCore.Unity.Editor
{
    using Codefarts.AppCore.Interfaces;
    using Codefarts.IoC;
    using UnityEditor;

    /// <summary>
    /// Provides a menu for general grid mapping settings.
    /// </summary>
    public class GlobalSettingsMenuItem
    {
        /// <summary>
        /// Used to draw the global settings.
        /// </summary>
        public static void Draw()
        {
            var local = Container.Default.Resolve<ILocalizationProvider>();  
            var settings = Container.Default.Resolve<ISettingsProvider>(); 

            var textValue = settings.GetSetting(Constants.ResourceFolderKey, "Codefarts.Unity");

            var value = EditorGUILayout.TextField(local.GetString("SETT_ResourceFolder"), textValue);
            if (value != textValue)
            {
                settings.SetSetting(Constants.ResourceFolderKey, value);
            }
        }
    }
}