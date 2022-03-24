// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SingletonBehaviour.cs" company="TODO: Company Name">
//   Copyright (c) 2022 TODO: Company Name
// </copyright>
// <summary>
//   If this project is helpful please take a short survey at ->
//   http://ux.mastercam.com/Surveys/APISDKSupport
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Level_Exporter.Annotations;
using System;

namespace Level_Exporter.Services
{
    /// <summary>
    /// The singleton behaviour.
    /// </summary>
    /// <typeparam name="T">Type T
    /// </typeparam>
    public abstract class SingletonBehaviour<T>
    {
        /// <summary>
        /// Backing field for the T Instance property
        /// </summary>
        // ReSharper disable once StyleCop.SA1309
        private static T instance;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        [UsedImplicitly]
        public static T Instance
        {
            get
            {
                if (instance != null && !instance.Equals(null))
                {
                    return instance;
                }

                instance = Activator.CreateInstance<T>();

                return instance;
            }
        }
    }
}