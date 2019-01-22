namespace Codefarts.AppCore
{
    using System;
#if NET40 || PORTABLE || NET45
    using System.Threading.Tasks;
#else
    using System.Threading;
#endif
#if UNITY_5
    using Task = Codefarts.UnityThreading.Task;
#endif

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
        /// <remarks>On .NET 4.0, 4.5 & Portable will Start a new Task via Task.Factory.StartNew.
        /// Otherwise queues it up on the thread pool via ThreadPool.QueueUserWorkItem.</remarks>
        public void OnUIThreadAsync(Action action)
        {
#if NET40 || PORTABLE || NET45
            Task.Factory.StartNew(action);
#else
            ThreadPool.QueueUserWorkItem(x => action());
#endif
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
        /// Executes the action asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="args">Provides an list of arguments that will be passed to the action.</param>
        /// <remarks>On .NET 4.0, 4.5 & Portable will Start a new Task via Task.Factory.StartNew.
        /// Otherwise queues it up on the thread pool via ThreadPool.QueueUserWorkItem.</remarks>
        public void OnUIThreadAsync(Action<object[]> action, params object[] args)
        {
#if NET40 || PORTABLE || NET45
            Task.Factory.StartNew(() => action(args));
#else
            ThreadPool.QueueUserWorkItem(x => action(args));
#endif
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
