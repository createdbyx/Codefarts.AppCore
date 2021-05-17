// <copyright file="WpfGenericCommandWrapper.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace AppCoreCommandsWPF
{
    using System;
    using Codefarts.AppCore.Interfaces;

 internal class WpfGenericCommandWrapper<T> : System.Windows.Input.ICommand
    {
        private ICommand<T> command;

        public WpfGenericCommandWrapper(ICommand<T> command)
        {
            this.command = command ?? throw new ArgumentNullException(nameof(command));
        }

        /// <summary>
        /// Gets or set the <see cref="ICommand"/> reference that will be executed.
        /// </summary>
        public ICommand<T> Command
        {
            get { return this.command; }

            // set
            // {
            //     var com = this.command;
            //     if (com != null)
            //     {
            //         com.CanExecuteChanged -= this.ComOnCanExecuteChanged;
            //     }
            //
            //     this.command = value;
            //     if (value != null)
            //     {
            //         value.CanExecuteChanged += this.ComOnCanExecuteChanged;
            //     }
            // }
        }

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">THe event arguments.</param>
        private void ComOnCanExecuteChanged(object sender, EventArgs e)
        {
            var handler = this.CanExecuteChanged;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        /// <inheritdoc />
        public bool CanExecute(object parameter)
        {
            var value = this.command;
            return value != null && value.CanExecute(parameter == null ? default(T) : (T)parameter);
        }

        /// <inheritdoc />
        public void Execute(object parameter)
        {
            var value = this.command;
            if (value != null)
            {
                value.Execute(parameter == null ? default(T) : (T)parameter);
            }
        }

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged;
    }
}