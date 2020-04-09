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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Codefarts.AppCore.Unity.Editor.Interfaces;
    using Codefarts.AppCore.Unity.Editor.Models;
    using UnityEngine;

    /// <summary>
    /// The editor callback service.
    /// </summary>
    public class EditorCallbackService
    {
        /// <summary>
        /// Holds a list of <see cref="CallbackModel{T}"/> types that implement <see cref="IRun"/>.
        /// </summary>
        private readonly List<IRun> callbacks;

        /// <summary>
        /// Used to determine whether callbacks are currently being run.
        /// </summary>
        private bool isRunning;

        /// <summary>
        /// Initializes the <see cref="EditorCallbackService"/> class.
        /// </summary>
        static EditorCallbackService()
        {
            Instance = new EditorCallbackService();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorCallbackService"/> class.
        /// </summary>
        public EditorCallbackService()
        {
            this.callbacks = new List<IRun>();
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="EditorCallbackService"/> type.
        /// </summary>
        public static EditorCallbackService Instance
        {
            get;
        }

        /// <summary>
        /// Runs any callbacks that have been registered.
        /// </summary>
        public void Run()
        {
            this.isRunning = true;

            var items = this.callbacks.OrderBy(x => x.Priority);
            foreach (var item in items)
            {
                // wrap callbacks in a try/catch but still report exceptions to the console
                // this is to prevent a chain of callbacks being canceled if a callback earlier
                // in the call chain throws an exception. Also ensures the callbacks list
                // will be cleared
                try
                {
                    item.Run();
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }

            this.callbacks.Clear();
            this.isRunning = false;
        }

        /// <summary>
        /// Registers a <see cref="Action{T}"/> callback.
        /// </summary>
        /// <typeparam name="T">
        /// The type of data that the <paramref name="callback"/> takes as a parameter.
        /// </typeparam>
        /// <param name="callback">
        /// A reference to a callback.
        /// </param>
        /// <param name="data">
        /// The data that will be passed as a parameter when the <paramref name="callback"/> is invoked.
        /// </param>
        /// <param name="priority">A value indicating the execution priority.</param>
        /// <exception cref="ArgumentNullException">If callback parameter is null.</exception>
        public void Register<T>(Action<T> callback, T data, int priority)
        {
            if (callback == null)
            {
                throw new ArgumentNullException("callback");
            }

            if (this.isRunning)
            {
                // helps prevent infinite recusion or endless looping
                throw new InvalidOperationException("Cannot register new callback while running callbacks.");
            }

            var modal = new CallbackModel<T> { Callback = callback, Data = data, Priority = priority };
            this.callbacks.Add(modal);
        }
    }
}
