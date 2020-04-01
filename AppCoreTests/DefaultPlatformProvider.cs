namespace AppCoreTests
{
    using System;
    using System.Threading;
    using Codefarts.AppCore;
    using Codefarts.AppCore.Interfaces;

    /// <summary>
    /// Default implementation for <see cref="IPlatformProvider"/> that does no platform enlightenment.
    /// </summary>
    public class DefaultPlatformProvider : IPlatformProvider
    {
        /// <summary>
        /// Gets a value indicating whether or not the framework is in design-time mode.
        /// </summary>
        public bool InDesignMode
        {
            get { return true; }
        }

        /// <summary>
        /// Executes the action asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <remarks>On .NET 4.0, 4.5 and Portable will Start a new Task via Task.Factory.StartNew.
        /// Otherwise queues it up on the thread pool via ThreadPool.QueueUserWorkItem.</remarks>
        public void OnUIThreadAsync(Action action)
        {
            ThreadPool.QueueUserWorkItem(x => action());
        }

        /// <summary>
        /// Executes the action immediately.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public void OnUIThread(Action action)
        {
            action();
        }

        /// <summary>
        /// Gets the platform data.
        /// </summary>
        /// <returns>
        /// Return a new <see cref="PlatformData"/> object containing information about the current platform and environment that the code is running under.
        /// </returns>
        public PlatformData GetPlatformData()
        {
            return new PlatformData(null, 0, null);
        }

        /// <summary>
        /// Executes the action asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="args">Provides an list of arguments that will be passed to the action.</param>
        /// <remarks>On .NET 4.0, 4.5 and Portable will Start a new Task via Task.Factory.StartNew.
        /// Otherwise queues it up on the thread pool via ThreadPool.QueueUserWorkItem.</remarks>
        public void OnUIThreadAsync(Action<object[]> action, params object[] args)
        {
            ThreadPool.QueueUserWorkItem(x => action(args));
        }

        /// <summary>
        /// Executes the action immediately.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="args">Provides an list of arguments that will be passed to the action.</param>
        public void OnUIThread(Action<object[]> action, params object[] args)
        {
            action(args);
        }
    }
}
