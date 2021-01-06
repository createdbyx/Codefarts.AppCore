// <copyright file="SettingException.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.AppCore
{
    using System;

    /// <summary>
    /// Provides exception class for setting related exceptions.
    /// </summary>
    public class SettingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingException"/> class.
        /// </summary>
        public SettingException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingException"/> class.
        /// </summary>
        /// <param name="message">The error message to report.</param>
        public SettingException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingException"/> class.
        /// </summary>
        /// <param name="message">The error message to report.</param>
        /// <param name="innerException">Gets the Exception instance that caused the current exception.</param>
        public SettingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
