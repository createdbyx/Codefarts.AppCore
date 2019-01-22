namespace Codefarts.AppCore
{
    using System;
#if NET40 || PORTABLE
#endif
#if UNITY_5
    using Task = Codefarts.UnityThreading.Task;
#endif

    /// <summary>
    /// Interface for platform specific operations that need enlightenment.
    /// </summary>
    public interface IPlatformProvider
    {
        #region Execute

        /// <summary>
        /// Gets a value indicates whether or not the framework is in design-time mode.
        /// </summary>
        bool InDesignMode { get; }

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="args">Provides an list of arguments that will be passed to the action.</param>
        /// <remarks>Implementation details are left up to the platform implementor weather in the form of Tasks or spawning a new thread.</remarks>
        void OnUIThreadAsync(Action<object[]> action, params object[] args);

        /// <summary>
        /// Executes the action on the UI thread.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="args">Provides an list of arguments that will be passed to the action.</param>
        void OnUIThread(Action<object[]> action, params object[] args);

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <remarks>Implementation details are left up to the platform implementor weather in the form of Tasks or spawning a new thread.</remarks>
        void OnUIThreadAsync(Action action);

        /// <summary>
        /// Executes the action on the UI thread.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        void OnUIThread(Action action);

        #endregion
    }
}
