// <copyright file="ICommand.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.AppCore.Interfaces
{
    /// <summary>
    /// Provides a interface for application commands.
    /// </summary>
    /// <remarks>Used to define a basic command implementation with <see cref="object"/> as a parameter.</remarks>
    public interface ICommand
        : ICommandGeneric<object>
    {
    }
}