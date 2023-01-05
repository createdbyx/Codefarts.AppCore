/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

using Codefarts.IoC;
using UnityEditor;

namespace Codefarts.AppCore.Unity.Editor
{
    /// <summary>
    /// Handles settings registration.
    /// </summary>
    [InitializeOnLoad]
    public class EditorInitialization
    {
        /// <summary>
        /// Holds a value indicating whether the RunCallbacks method has been called at least once before.
        /// </summary>
        private static bool ranOnce;

        /// <summary>
        /// Initializes static members of the <see cref="EditorInitialization"/> class.
        /// </summary>
        static EditorInitialization()
        {
            EditorApplication.update += RunCallbacks;
        }

        /// <summary>
        /// Runs any registered callbacks.
        /// </summary>
        /// <remarks>The first time this method is run it will setup settings, localization and register global settings, then exit without running any callbacks.</remarks>
        private static void RunCallbacks()
        {
            // wait 5 sec before calling callbacks the first time
            if (!ranOnce)
            {
                ranOnce = true;

                // register global settings
                RegisterGlobalSettings();

                return;
            }

            // invoke callbacks from editor callback service
            EditorCallbackService.Instance.Run();
        }

        /// <summary>
        /// Registers global settings
        /// </summary>
        private static void RegisterGlobalSettings()
        {
            var local = Container.Default.Resolve<ILocalizationProvider>(); // LocalizationManager.Instance;
            var config = UnityPreferencesManager.Instance;
            config.Register(local.GetString("SETT_GlobalSettings"), GlobalSettingsMenuItem.Draw);
        }
    }
}