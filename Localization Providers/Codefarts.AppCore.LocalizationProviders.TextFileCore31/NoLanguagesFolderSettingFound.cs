// <copyright file="NoLanguagesFolderSettingFound.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

namespace Codefarts.AppCore.LocalizationProviders.TextFileCore31
{
    using System;

    public class NoLanguagesFolderSettingFound : Exception
    {
        public NoLanguagesFolderSettingFound()
            : base("Missing languages folder setting.")
        {
        }
    }
}