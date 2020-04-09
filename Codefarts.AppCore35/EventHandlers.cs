// <copyright file="EventHandlers.cs" company="Codefarts">
// Copyright (c) Codefarts
// </copyright>

namespace Codefarts.AppCore.EventHandlers
{
    using Codefarts.AppCore.EventArgs;
    using Codefarts.AppCore.Interfaces;

    /// <summary>
    /// Provides a delegate for the <see cref="ISettingsProvider.SettingChanged"/> event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The arguments containing information about what setting changed.</param>
    public delegate void SettingChangedEventHandler(object sender, SettingChangedEventHandlerArgs args);
}