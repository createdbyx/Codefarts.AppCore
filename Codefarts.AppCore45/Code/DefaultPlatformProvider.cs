﻿namespace Codefarts.AppCore
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
        /// Indicates whether or not the framework is in design-time mode.
        /// </summary>
        public bool InDesignMode
        {
            get { return true; }
        }

        ///// <summary>
        ///// Executes the action on the UI thread asynchronously.
        ///// </summary>
        ///// <param name="action">The action to execute.</param>
        //public void BeginOnUIThread(Action action)
        //{
        //    action();
        //}

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <returns></returns>
        public void OnUIThreadAsync(Action action)
        {
#if NET40 || PORTABLE || NET45
            Task.Factory.StartNew(action);
#else
            ThreadPool.QueueUserWorkItem(x => action());
#endif
        }

        /// <summary>
        /// Executes the action on the UI thread.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public void OnUIThread(Action action)
        {
            action();
        }

        /// <summary>
        /// Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <returns></returns>
        public void OnUIThreadAsync(Action<object[]> action, params object[] args)
        {
#if NET40 || PORTABLE || NET45
            Task.Factory.StartNew(() => action(args));
#else
            ThreadPool.QueueUserWorkItem(x => action(args));
#endif
        }

        /// <summary>
        /// Executes the action on the UI thread.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public void OnUIThread(Action<object[]> action, params object[] args)
        {
            action(args);
        }

        ///// <summary>
        ///// Used to retrieve the root, non-framework-created view.
        ///// </summary>
        ///// <param name="view">The view to search.</param>
        ///// <returns>
        ///// The root element that was not created by the framework.
        ///// </returns>
        ///// <remarks>
        ///// In certain instances the services create UI elements.
        ///// For example, if you ask the window manager to show a UserControl as a dialog, it creates a window to host the UserControl in.
        ///// The WindowManager marks that element as a framework-created element so that it can determine what it created vs. what was intended by the developer.
        ///// Calling GetFirstNonGeneratedView allows the framework to discover what the original element was.
        ///// </remarks>
        // public object GetFirstNonGeneratedView(object view)
        // {
        // return view;
        // }

        ///// <summary>
        ///// Executes the handler the fist time the view is loaded.
        ///// </summary>
        ///// <param name="view">The view.</param>
        ///// <param name="handler">The handler.</param>
        ///// <returns>true if the handler was executed immediately; false otherwise</returns>
        // public void ExecuteOnFirstLoad(object view, Action<object> handler)
        // {
        // handler(view);
        // }

        ///// <summary>
        ///// Executes the handler the next time the view's LayoutUpdated event fires.
        ///// </summary>
        ///// <param name="view">The view.</param>
        ///// <param name="handler">The handler.</param>
        // public void ExecuteOnLayoutUpdated(object view, Action<object> handler)
        // {
        // handler(view);
        // }

        ///// <summary>
        ///// Get the close action for the specified view model.
        ///// </summary>
        ///// <param name="viewModel">The view model to close.</param>
        ///// <param name="views">The associated views.</param>
        ///// <param name="dialogResult">The dialog result.</param>
        ///// <returns>
        ///// An <see cref="Action" /> to close the view model.
        ///// </returns>
        // public Action GetViewCloseAction(object viewModel, ICollection<object> views, bool? dialogResult)
        // {
        // return () => { };
        // }
    }
}
