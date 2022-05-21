// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResourceReaderService.cs" company="MarkRoldan88@github">
//   Copyright (c) 2022 MarkRoldan88@github
// </copyright>
// <summary>
//   If this project is helpful please take a short survey at ->
//   http://ux.mastercam.com/Surveys/APISDKSupport
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Level_Exporter.Models;
using System.Drawing;
using NETHookResources = Level_Exporter.Properties.Resources;

namespace Level_Exporter.Services
{
    /// <summary>
    /// The resource reader service.
    /// </summary>
    public class ResourceReaderService : SingletonBehaviour<ResourceReaderService>
    {
        /// <summary>
        /// Gets the resource string from our resources
        /// </summary>
        /// <param name="name">The resource name</param>
        /// <returns>The value of the resource</returns>
        public static Result<string> GetString(string name)
        {
            try
            {
                // Gets the localized string via the alias
                return Result.Ok(NETHookResources.ResourceManager.GetString(name));
            }
            catch
            {
                return Result.Fail<string>($"Missing resource {name} ");
            }
        }

        /// <summary>
        /// The get image.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="Result"/>.
        /// </returns>
        public static Result<Image> GetImage(string name)
        {
            try
            {
                return Result.Ok(NETHookResources.ResourceManager.GetObject(name) as Image);
            }
            catch
            {
                return Result.Fail<Image>($"Missing image resource {name} ");
            }
        }
    }
}