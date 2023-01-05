// <copyright file="CommandConverter.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

using Codefarts.AppCore.Interfaces;

namespace Codefarts.AppCore.Wpf.Commands
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Data;
    using Commands;

    public class CommandConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            if (value is System.Windows.Input.ICommand)
            {
                return value;
            }

            var genericCommandType = typeof(ICommand<>);
            var commandReference = value;
            var commandType = commandReference.GetType();

            if (typeof(ICommand).IsAssignableFrom(commandType))
            {
                var wrapper = new WpfCommandWrapper(commandReference as ICommand);
                return wrapper;
            }

            // Just a generic func
            var implementationType = commandType.GetInterfaces().FirstOrDefault(x => x.FullName.StartsWith(genericCommandType.FullName));

            if (implementationType != null)
            {
                var genericArguments = implementationType.GetGenericArguments();
                var parameterType = genericArguments[0];
                var wrapper = this.CreateAction(parameterType, commandReference);
                return wrapper;
            }

            throw new InvalidCastException(
                $"Unable to cast {value.GetType().FullName} to a wrapper class. The value may not implement {typeof(ICommand).FullName} or {genericCommandType.FullName}.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            var commandType = value.GetType();
            var genericCommandType = typeof(WpfGenericCommandWrapper<>);

            if (value is WpfCommandWrapper)
            {
                var wrapper = value as WpfCommandWrapper;
                return wrapper.Command;
            }

            // Just a generic func
            var implementationType = commandType.FullName.StartsWith(genericCommandType.FullName) ? commandType : null;

            if (implementationType != null)
            {
                var genericArguments = implementationType.GetGenericArguments();
                var parameterType = genericArguments[0];

                var methodInfo = this.GetType()
                    .GetMethod(nameof(this.Fetch), BindingFlags.NonPublic | BindingFlags.Instance)
                    .MakeGenericMethod(parameterType);

                var wrapper = methodInfo.Invoke(this, new[] { value });
                return wrapper;
            }

            throw new InvalidCastException($"Unable to cast {value.GetType().FullName} to a {nameof(ICommand)} type.");
        }

        /// <summary>
        /// Private method that acts as a wrapper for the Resolve method.
        /// </summary>
        /// <typeparam name="T">The type to cast to before returning.</typeparam>
        /// <remarks>This is called by the <seealso cref="CreateAction"/> method.</remarks>
        private System.Windows.Input.ICommand Perform<T>(ICommand<T> command)
        {
            var item = new WpfGenericCommandWrapper<T>(command);
            return item;
        }

        /// <summary>
        /// Private method that acts as a wrapper for the Resolve method.
        /// </summary>
        /// <typeparam name="T">The type to cast to before returning.</typeparam>
        /// <remarks>This is called by the <seealso cref="CreateAction"/> method.</remarks>
        private ICommand<T> Fetch<T>(WpfGenericCommandWrapper<T> command)
        {
            return command.Command;
        }

        /// <summary>
        /// Called by the <seealso cref="ResolveGenericType"/> method.
        /// </summary>
        private System.Windows.Input.ICommand CreateAction(Type type, object commandReference)
        {
            var methodInfo = this.GetType()
                .GetMethod(nameof(Perform), BindingFlags.NonPublic | BindingFlags.Instance)
                .MakeGenericMethod(type);
            return (System.Windows.Input.ICommand)methodInfo.Invoke(this, new[] { commandReference });
        }
    }
}