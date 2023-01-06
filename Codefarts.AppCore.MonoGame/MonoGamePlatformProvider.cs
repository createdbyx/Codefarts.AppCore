// <copyright file="MonoGamePlatformProvider.cs" company="Codefarts">
// Copyright (c) Codefarts
// contact@codefarts.com
// http://www.codefarts.com
// </copyright>

using Codefarts.AppCore.Interfaces;
using Microsoft.Xna.Framework;

// ReSharper disable once CheckNamespace
namespace Codefarts.AppCore;

/// <summary>
/// Default implementation of <see cref="IPlatformProvider"/> for Wpf 3.5 and 4.5.
/// </summary>
public class MonoGamePlatformProvider : IPlatformProvider
{
    private Game game;

    /// <summary>
    /// Initializes a new instance of the <see cref="MonoGamePlatformProvider"/> class.
    /// </summary>
    /// <param name="game"></param>
    public MonoGamePlatformProvider(Game game)
    {
        this.game = game;
    }

    /// <summary>
    /// Gets a value indicating whether or not the framework is in design-time mode.
    /// </summary>
    public bool InDesignMode
    {
        get
        {
            return false;
        }
    }

    /// <summary>
    /// Executes the action on the UI thread asynchronously.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    /// <param name="args">Provides an list of arguments that will be passed to the action via the current <see cref="AppDomain"/>.</param>
    public void OnUIThreadAsync(Action<object[]> action, params object[] args)
    {
        // var application = Application.Current;
        // if (application != null)
        // {
        //     application.Dispatcher.BeginInvoke(action, args);
        // }
    }

    /// <summary>
    /// Executes the action on the UI thread asynchronously.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    public void OnUIThreadAsync(Action action)
    {
        // var application = Application.Current;
        // if (application != null)
        // {
        //     application.Dispatcher.BeginInvoke(action);
        // }
    }

    /// <summary>
    /// Executes the action on the UI thread.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    /// <param name="args">Provides an list of arguments that will be passed to the action via the current <see cref="AppDomain"/>.</param>
    public void OnUIThread(Action<object[]> action, params object[] args)
    {
        action(args);
    }

    /// <summary>
    /// Executes the action on the UI thread.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    public void OnUIThread(Action action)
    {
        action();
    }

    /// <summary>
    /// Gets the platform data.
    /// </summary>
    /// <returns>
    /// Return a new <see cref="T:Codefarts.AppCore.PlatformData"/> object containing information about the current platform and
    /// environment that the code is running under.
    /// </returns>
    public PlatformData GetPlatformData()
    {
        var currentProcess = System.Diagnostics.Process.GetCurrentProcess();
        return new PlatformData(
            Environment.OSVersion.VersionString,
            Environment.ProcessorCount,
            Environment.GetCommandLineArgs(),
            currentProcess.MainModule?.FileName);
    }
}