// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CodefartsContainerObject.cs" company="Codefarts">
//   Copyright (c) 2012 Codefarts
//   All rights reserved.
//   contact@codefarts.com
//   http://www.codefarts.com
// </copyright>
// <summary>
//   Used to provide a singleton reference that houses a GameObject that is to be used as a common location for various managerial game objects.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Codefarts.AppCore.Unity
{
    using System;
    using System.Collections;

    using UnityEngine;

    /// <summary>
    /// Used to provide a singleton reference that houses a GameObject that is to be used as a common location for various managerial game objects.
    /// </summary>
    /// <remarks>
    /// The use of the game object is intended to provide a more cleaner hierarchy.</remarks>
    public class CodefartsContainerObject
    {
        #region Static Fields

        /// <summary>
        /// The singleton instance of the Codefarts container object.
        /// </summary>
        private static CodefartsContainerObject singleton;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the singleton instance to the Codefarts container.
        /// </summary>
        public static CodefartsContainerObject Instance
        {
            get
            {
                // check if instance is not set and of no set it and create the game object
                if (singleton == null)
                {
                    singleton = new CodefartsContainerObject();
                    singleton.GameObject = new GameObject("Codefarts Container");

                    // ensure persistence across scenes by default
                    GameObject.DontDestroyOnLoad(singleton.GameObject);
                }

                // return the instance reference
                return singleton;
            }
        }

        /// <summary>
        /// Gets or sets the game object that will be used as the general purpose Codefarts container.
        /// </summary>     
        public GameObject GameObject { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Runs a coroutine.
        /// </summary>
        /// <param name="routine">
        /// The coroutine to be run.
        /// </param>
        /// <returns>
        /// Coroutine result.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// If <see cref="routine"/> is null.
        /// </exception>
        /// <exception cref="System.NullReferenceException">
        /// 'GameObject' property is not set.
        /// </exception>
        /// <remarks>
        /// Calling this method the first time will add a <see cref="HiddenCoroutineBehaviour"/> to the <see cref="GameObject"/> property if one has not already been added.  
        /// </remarks>
        public Coroutine StartCoroutine(Func<IEnumerator> routine)
        {
            // make sure routine is valid
            if (routine == null)
            {
                throw new ArgumentNullException("routine");
            }

            // get game object and check if it exists
            var gameObject = this.GameObject;
            if (gameObject == null)
            {
                throw new NullReferenceException("'GameObject' property is not set.");
            }

            // if HiddenCoroutineBehaviour not available add it
            var behaviour = gameObject.GetComponent<HiddenCoroutineBehaviour>();
            if (behaviour == null)
            {
                behaviour = gameObject.AddComponent<HiddenCoroutineBehaviour>();
            }

            // run coroutine
            return behaviour.StartCoroutine(routine());
        }

        #endregion

        /// <summary>
        /// Provides a hidden class for executing unity coroutines.
        /// </summary>
        private class HiddenCoroutineBehaviour : MonoBehaviour
        {
        }
    }
}