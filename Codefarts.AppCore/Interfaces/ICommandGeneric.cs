// <copyright file="ICommandGeneric.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.AppCore.Interfaces
{
    using System;

    /// <summary>
    /// Provides a interface for application commands.
    /// </summary>
    /// <typeparam name="T">The parameter type that the command expects.</typeparam>
    public interface ICommand<T>
    {
        /// <summary>
        /// Raised when the state command state changes.
        /// </summary>
        event EventHandler CanExecuteChanged;

        /// <summary>
        /// Used to determine weather or not a command is in an executable state.
        /// </summary>
        /// <param name="parameter">The parameter value that will be passed to the execute <see cref="Execute"/>.</param>
        /// <returns>true is the command can be executed; otherwise false.</returns>
        bool CanExecute(T parameter);

        /// <summary>
        /// Executes the command logic.
        /// </summary>
        /// <param name="parameter">The value that the command may or may no need to execute successfully.</param>
        void Execute(T parameter);
    }
}