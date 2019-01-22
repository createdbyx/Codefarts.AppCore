namespace Codefarts.AppCore
{
    using System;
    using System.ComponentModel;
    using System.Windows;

#if NET40 || PORTABLE
    using System.Threading.Tasks;
#else
#endif
#if UNITY_5
    using Task = Codefarts.UnityThreading.Task;
#endif

    /// <summary>
    /// Default implementation of <see cref="IPlatformProvider"/> for Wpf 3.5 & 4.5.
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
    }
}
