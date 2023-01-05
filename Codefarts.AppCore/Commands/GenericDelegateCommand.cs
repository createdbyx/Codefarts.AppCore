// <copyright file="GenericDelegateCommand.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.AppCore.Commands
{
    using System;

    /// <summary>
    /// Provides a delegate command with a specified parameter type.
    /// </summary>
    /// <typeparam name="T">The type for the parameter argument.</typeparam>
    public class GenericDelegateCommand<T> : GenericBaseCommand<T>
    {
        private Func<T, bool> canExecuteCallback;
        private Action<T> executeCallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericDelegateCommand{T}"/> class.
        /// </summary>
        public GenericDelegateCommand()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="GenericDelegateCommand{T}"/> class.</summary>
        /// <param name="canExecuteCallback">Sets the <see cref="CanExecuteCallback"/> property.</param>
        /// <param name="executeCallback">Sets the <see cref="ExecuteCallback"/> property.</param>
        public GenericDelegateCommand(Func<T, bool> canExecuteCallback, Action<T> executeCallback)
        {
            this.canExecuteCallback = canExecuteCallback;
            this.executeCallback = executeCallback;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericDelegateCommand{T}"/> class.
        /// </summary>
        /// <param name="executeCallback">Sets the <see cref="ExecuteCallback"/> property.</param>
        public GenericDelegateCommand(Action<T> executeCallback)
        {
            this.executeCallback = executeCallback;
        }

        /// <summary>
        /// Gets or sets the callback that determine weather or not the command can be executed.
        /// </summary>
        public Func<T, bool> CanExecuteCallback
        {
            get
            {
                return this.canExecuteCallback;
            }

            set
            {
                var currentValue = this.canExecuteCallback;
                if (currentValue != value)
                {
                    this.canExecuteCallback = value;
                    this.NotifyOfPropertyChange(() => this.CanExecuteCallback);
                }
            }
        }

        /// <summary>
        /// Gets or sets the execute callback.
        /// </summary>
        public Action<T> ExecuteCallback
        {
            get
            {
                return this.executeCallback;
            }

            set
            {
                var currentValue = this.executeCallback;
                if (currentValue != value)
                {
                    this.executeCallback = value;
                    this.NotifyOfPropertyChange(() => this.ExecuteCallback);
                }
            }
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        /// <param name="parameter">Data used by the command.</param>
        public override bool CanExecute(T parameter)
        {
            var callback = this.canExecuteCallback;
            if (callback == null)
            {
                return true;
            }

            return callback(parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        public override void Execute(T parameter)
        {
            var callback = this.executeCallback;
            if (callback != null)
            {
                callback(parameter);
            }
        }
    }
}