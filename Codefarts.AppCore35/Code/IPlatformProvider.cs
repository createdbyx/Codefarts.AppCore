namespace Codefarts.AppCore
{
    using System;
#if NET40 || PORTABLE
    using System.Threading.Tasks;
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
        ///   Indicates whether or not the framework is in design-time mode.
        /// </summary>
        bool InDesignMode { get; }

        ///// <summary>
        /////   Executes the action on the UI thread asynchronously.
        ///// </summary>
        ///// <param name="action">The action to execute.</param>
        //void BeginOnUIThread(Action action);

        /// <summary>
        ///   Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name = "action">The action to execute.</param>
        /// <remarks>Implementation details are left up to the platform implementor weather in the form of Tasks or spawning a new thread.</remarks>
        void OnUIThreadAsync(Action<object[]> action, params object[] args);

        /// <summary>
        ///   Executes the action on the UI thread.
        /// </summary>
        /// <param name = "action">The action to execute.</param>
        void OnUIThread(Action<object[]> action, params object[] args);

        /// <summary>
        ///   Executes the action on the UI thread asynchronously.
        /// </summary>
        /// <param name = "action">The action to execute.</param>
        /// <remarks>Implementation details are left up to the platform implementor weather in the form of Tasks or spawning a new thread.</remarks>
        void OnUIThreadAsync(Action action);

        /// <summary>
        ///   Executes the action on the UI thread.
        /// </summary>
        /// <param name = "action">The action to execute.</param>
        void OnUIThread(Action action);

        #endregion

        // #region ViewAware

        ///// <summary>
        ///// Used to retrieve the root, non-framework-created view.
        ///// </summary>
        ///// <param name="view">The view to search.</param>
        ///// <returns>The root element that was not created by the framework.</returns>
        ///// <remarks>In certain instances the services create UI elements.
        ///// For example, if you ask the window manager to show a UserControl as a dialog, it creates a window to host the UserControl in.
        ///// The WindowManager marks that element as a framework-created element so that it can determine what it created vs. what was intended by the developer.
        ///// Calling GetFirstNonGeneratedView allows the framework to discover what the original element was.
        ///// </remarks>
        // object GetFirstNonGeneratedView(object view);

        ///// <summary>
        ///// Executes the handler the fist time the view is loaded.
        ///// </summary>
        ///// <param name="view">The view.</param>
        ///// <param name="handler">The handler.</param>
        // void ExecuteOnFirstLoad(object view, Action<object> handler);

        ///// <summary>
        ///// Executes the handler the next time the view's LayoutUpdated event fires.
        ///// </summary>
        ///// <param name="view">The view.</param>
        ///// <param name="handler">The handler.</param>
        // void ExecuteOnLayoutUpdated(object view, Action<object> handler);

        ///// <summary>
        ///// Get the close action for the specified view model.
        ///// </summary>
        ///// <param name="viewModel">The view model to close.</param>
        ///// <param name="views">The associated views.</param>
        ///// <param name="dialogResult">The dialog result.</param>
        ///// <returns>An <see cref="Action"/> to close the view model.</returns>
        // Action GetViewCloseAction(object viewModel, ICollection<object> views, bool? dialogResult);

        // #endregion
    }
}
