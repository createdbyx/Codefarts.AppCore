// <copyright file="GenericBaseCommand.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.AppCore.Commands
{
    using System;
    using Codefarts.AppCore.Interfaces;

    /// <summary>
    /// Provides a base class for a command with a specified parameter type.
    /// </summary>
    /// <typeparam name="T">The type for the parameter argument.</typeparam>
    public abstract class GenericBaseCommand<T> : PropertyChangedBase, ICommandGeneric<T>
    {
        /// <inheritdoc />
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc />
        public virtual bool CanExecute(T parameter)
        {
            return true;
        }

        /// <inheritdoc />
        public abstract void Execute(T parameter);

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            var handler = this.CanExecuteChanged;
            if (handler != null)
            {
                handler.Invoke(this, System.EventArgs.Empty);
            }
        }
    }
}