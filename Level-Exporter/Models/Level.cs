using System;
using System.Collections.Generic;
using System.Linq;

namespace Level_Exporter.Models
{
    using Mastercam.Database;

    public class Level
    {
        #region Public Properties
        /// <summary>
        /// Gets and sets Level Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets and Sets Level number
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets and Sets Entity count
        /// </summary>
        public int EntityCount { get; set; }

        /// <summary>
        /// Gets and Sets Geometries
        /// </summary>
        public Geometry[] Geometries { get; set; }

        #endregion
    }
}
