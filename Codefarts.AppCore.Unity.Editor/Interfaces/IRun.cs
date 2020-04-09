/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

using Codefarts.AppCore.Unity.Editor.Models;

namespace Codefarts.AppCore.Unity.Editor.Interfaces
{
    /// <summary>
    /// Provides a interface for running a <see cref="CallbackModel{T}"/> type.
    /// </summary>
    internal interface IRun
    {
        /// <summary>
        /// Gets a value indicating the priority.
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// Runs the <see cref="CallbackModel{T}"/> type.
        /// </summary>
        void Run();
    }
}