// <copyright file="WpfCommandWrapper.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace AppCoreCommandsWPF
{
    using System;
    using Codefarts.AppCore.Interfaces;

    internal class WpfCommandWrapper : System.Windows.Input.ICommand
    {
        private ICommand command;

        public WpfCommandWrapper(ICommand command)
        {
            this.command = command ?? throw new ArgumentNullException(nameof(command));
        }

        /// <summary>
        /// Gets or set the <see cref="ICommand"/> reference that will be executed.
        /// </summary>
        public ICommand Command
        {
            get { return this.command; }

            // set
            // {
            //     var com = this.command;
            //     if (com != null)
            //     {
            //         com.CanExecuteChanged -= ComOnCanExecuteChanged;
            //     }
            //
            //     this.command = value;
            //     if (value != null)
            //     {
            //         value.CanExecuteChanged += ComOnCanExecuteChanged;
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
            var value = this.Command;
            return value != null && value.CanExecute(parameter);
        }

        /// <inheritdoc />
        public void Execute(object parameter)
        {
            var value = this.Command;
            if (value != null)
            {
                value.Execute(parameter);
            }
        }

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged;
    }
}