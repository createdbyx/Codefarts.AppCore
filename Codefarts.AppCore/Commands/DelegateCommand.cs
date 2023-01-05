// <copyright file="DelegateCommand.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.AppCore.Commands
{
    using System;

    /// <summary>
    /// Provides a basic delegate command.
    /// </summary>
    public class DelegateCommand : GenericDelegateCommand<object>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/> class.
        /// </summary>
        public DelegateCommand()
        {
        }

        /// <inheritdoc />
        public DelegateCommand(Func<object, bool> canExecuteCallback, Action<object> executeCallback)
            : base(canExecuteCallback, executeCallback)
        {
        }

        /// <inheritdoc />
        public DelegateCommand(Action<object> executeCallback)
            : base(executeCallback)
        {
        }
    }
}