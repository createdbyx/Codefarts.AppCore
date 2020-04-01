namespace Codefarts.AppCore.Interfaces
{
    using System;

    /// <summary>
    /// Interface for platform specific operations that need enlightenment.
    /// </summary>
    public interface IPlatformProvider
    {
        /// <summary>
        /// Gets a value indicating whether or not the framework is in design-time mode.
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

        /// <summary>
        /// Gets the platform data.
        /// </summary>
        /// <returns>Return a new <see cref="PlatformData"/> object containing information about the current platform and environment that the code is running under.</returns>
        PlatformData GetPlatformData();
    }
}
