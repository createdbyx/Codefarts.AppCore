// <copyright file="NoLanguagesFolderSettingFound.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.AppCore.LocalizationProviders.TextFileCore31
{
    using System;

    /// <summary>
    /// Provides an exception for instances when the languages folder is missing.
    /// </summary>
    public class NoLanguagesFolderSettingFound : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoLanguagesFolderSettingFound"/> class.
        /// </summary>
        public NoLanguagesFolderSettingFound()
            : base("Missing languages folder setting.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoLanguagesFolderSettingFound"/> class.
        /// </summary>
        /// <param name="message">THe error message.</param>
        public NoLanguagesFolderSettingFound(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NoLanguagesFolderSettingFound"/> class.
        /// </summary>
        /// <param name="message">THe error message.</param>
        /// <param name="innerException">A reference to a exception.</param>
        public NoLanguagesFolderSettingFound(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}