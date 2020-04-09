/*
<copyright>
  Copyright (c) 2012 Codefarts
  All rights reserved.
  contact@codefarts.com
  http://www.codefarts.com
</copyright>
*/

namespace Codefarts.AppCore.Unity.Editor.Models
{
    using Codefarts.AppCore.Unity.Editor.Interfaces;

    /// <summary>
    /// Provides a menu model 
    /// </summary>
    /// <typeparam name="T">
    /// The generic type used to represent the data type.
    /// </typeparam>
    internal class SettingsMenuModel<T> : CallbackModel<T>, ISettingTitle
    {
        /// <summary>
        /// Gets or sets a title for the menu.
        /// </summary>
        public virtual string Title
        {
            get; set;
        }

        /// <summary>         
        /// Returns a string containing the title.
        /// </summary>
        /// <returns>Returns the value of the <see cref="Title"/> property.</returns>
        public override string ToString()
        {
            return this.Title;
        }
    }
}