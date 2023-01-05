// <copyright file="CommandFactory.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

using Codefarts.AppCore.Interfaces;

namespace Codefarts.AppCore.Wpf.Commands
{
    using System;

    // TODO: not sure why I need this factory but it may be removed in the future
    public class CommandFactory
    {
        public System.Windows.Input.ICommand CreateCommand(ICommand commandReference)
        {
            if (commandReference == null)
            {
                throw new ArgumentNullException(nameof(commandReference));
            }

            return new WpfCommandWrapper(commandReference);
        }

        public System.Windows.Input.ICommand CreateCommand<T>(ICommand<T> commandReference)
        {
            if (commandReference == null)
            {
                throw new ArgumentNullException(nameof(commandReference));
            }

            return new WpfGenericCommandWrapper<T>(commandReference);
        }

        public System.Windows.Input.ICommand CreateCommand(Func<ICommand> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            var commandReference = callback();
            var wrapper = new WpfCommandWrapper(commandReference);
            return wrapper;
        }

        public System.Windows.Input.ICommand CreateCommand<T>(Func<ICommand<T>> callback)
        {
            if (callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            var commandReference = callback();
            var item = new WpfGenericCommandWrapper<T>(commandReference);
            return item;
        }
    }
}