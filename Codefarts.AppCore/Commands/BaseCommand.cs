// <copyright file="BaseCommand.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.AppCore.Commands
{
    using System;
    using Codefarts.AppCore.Interfaces;

    /// <summary>
    /// Provides a base class for a command.
    /// </summary>
    public abstract class BaseCommand : PropertyChangedBase, ICommand
    {
        /// <inheritdoc />
        public event EventHandler CanExecuteChanged;

        /// <inheritdoc />
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        /// <inheritdoc />
        public abstract void Execute(object parameter);

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            var handler = this.CanExecuteChanged;
            if (handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }
    }
}