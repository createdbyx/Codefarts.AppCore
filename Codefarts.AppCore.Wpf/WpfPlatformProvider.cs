// <copyright file="WpfPlatformProvider.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

using Codefarts.AppCore.Interfaces;

namespace Codefarts.AppCore
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    /// <summary>
    /// Default implementation of <see cref="IPlatformProvider"/> for Wpf 3.5 and 4.5.
    /// </summary>
    public class WpfPlatformProvider : IPlatformProvider
    {
        /// <summary>
        /// Gets a value indicating whether or not the framework is in design-time mode.
        /// </summary>
        /// <remarks>Uses <see cref="Application.Current"/> main window to determine if in design mode.</remarks>
        public bool InDesignMode
        {
            get
            {
                var application = Application.Current;
                return DesignerProperties.GetIsInDesignMode(application.MainWindow ?? throw new InvalidOperationException());
            }
        }

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="args">Provides an list of arguments that will be passed to the action via the current <see cref="AppDomain"/>.</param>
        /// <remarks>Invokes the action via <see cref="Application.Current"/> dispatcher.</remarks>
        public void OnUIThreadAsync(Action<object[]> action, params object[] args)
        {
            var application = Application.Current;
            if (application != null)
            {
                application.Dispatcher.BeginInvoke(action, args);
            }
        }

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <remarks>Invokes the action via <see cref="Application.Current"/> dispatcher.</remarks>
        public void OnUIThreadAsync(Action action)
        {
            var application = Application.Current;
            if (application != null)
            {
                application.Dispatcher.BeginInvoke(action);
            }
        }

        /// <summary>
        /// Executes the action on the UI thread.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="args">Provides an list of arguments that will be passed to the action via the current <see cref="AppDomain"/>.</param>
        /// <remarks>Invokes the action via <see cref="Application.Current"/> dispatcher.</remarks>
        public void OnUIThread(Action<object[]> action, params object[] args)
        {
            var application = Application.Current;
            if (application != null)
            {
                application.Dispatcher.Invoke(action, args);
            }
        }

        /// <summary>
        /// Executes the action on the UI thread.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <remarks>Invokes the action via <see cref="Application.Current"/> dispatcher.</remarks>
        public void OnUIThread(Action action)
        {
            var application = Application.Current;
            if (application != null)
            {
                application.Dispatcher.Invoke(action);
            }
        }

        /// <summary>
        /// Gets the platform data.
        /// </summary>
        /// <returns>
        /// Return a new <see cref="T:Codefarts.AppCore.PlatformData"/> object containing information about the current platform and
        /// environment that the code is running under.
        /// </returns>
        public PlatformData GetPlatformData()
        {
            return new PlatformData(
                Environment.OSVersion.VersionString,
                Environment.ProcessorCount,
                Environment.GetCommandLineArgs(),
                System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
        }
    }
}
