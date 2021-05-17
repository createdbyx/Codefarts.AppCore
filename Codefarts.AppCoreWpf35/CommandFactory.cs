// <copyright file="CommandFactory.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace AppCoreCommandsWPF
{
    using System;
    using Codefarts.AppCore.Interfaces;

    public class CommandFactory
    {
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