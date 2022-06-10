using System;
using System.IO;
using Mastercam.App.Exceptions;
using Mastercam.IO;
using System.Collections.Generic;

namespace Level_Exporter.Models
{
    /// <summary>
    /// Helper Class for exporting/saving CAD
    /// </summary>
    public class CadExportHelper
    {
        #region Constructor

        public CadExportHelper(string destination, string cadFormat, double stlResolution, IEnumerable<Level> levels)
        {
            _destination = destination;
            _cadFormat = cadFormat;
            _levels = levels;

            if (stlResolution >= 0.0 && stlResolution < 5.0)
            {
                _stlResolution = stlResolution;
            }
            else _stlResolution = 0.75;
        }

        #endregion

        #region Private fields

        /// <summary>
        /// Destination Directory
        /// </summary>
        private readonly string _destination;

        /// <summary>
        /// Cad Format (file extension)
        /// </summary>
        private readonly string _cadFormat;

        /// <summary>
        /// STL resolution
        /// </summary>
        private readonly double _stlResolution;

        /// <summary>
        /// Full path created from level name and cad format (file extension)
        /// </summary>
        private string _fullPath;

        /// <summary>
        /// Levels pulled from Mastercam
        /// </summary>
        private readonly IEnumerable<Level> _levels;

        #endregion

        #region Public Methods

        /// <summary>
        /// Saves Cad entities within level, based on CAD format
        /// </summary>
        /// <param name="level"></param>
        /// <returns>Bool indicating successful operation</returns>
        public bool SaveLevelCad(Level level)
        {
            try
            {
                if (string.IsNullOrEmpty(_destination) || string.IsNullOrWhiteSpace(_destination))
                    return FileManager.SaveSome(string.Empty, true);

                _fullPath = Path.Combine(_destination, $"{level.Name}{_cadFormat}");

                return _cadFormat.Contains(CadTypes.Stl.ToString().ToLower())
                    ? SaveAsStl(level)
                    : FileManager.SaveSome(_fullPath, true);
            }
            catch (Exception e)
            {
                DialogManager.Exception(new MastercamException(
                    $"Error Saving Level {level.Number} files to directory, double check path and make sure level names do not contain any symbols"));
                Console.WriteLine(e);
                Console.WriteLine(e.InnerException);
                throw;
            }
        }
        #endregion

        /// <summary>
        /// Toggles visibility of levels when exporting as STL. 
        /// Levels that are not selected must be hidden in order for Mastercam Write stl method to work.
        /// </summary>
        /// <param name="levelToExport"></param>
        private void ToggleLevelVisibility(Level levelToExport)
        {
            foreach (var level in _levels)
            {
                if (level.Number != levelToExport.Number)
                {
                    _ = LevelsManager.SetLevelVisible(level.Number, false);
                    continue;
                }

                _ = LevelsManager.SetLevelVisible(levelToExport.Number, true);
            }
        }

        /// <summary>
        /// Exports level as an STL
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private bool SaveAsStl(Level level)
        {
            ToggleLevelVisibility(level);
            return FileManager.WriteSTL(_fullPath, 0, _stlResolution, false, false, true, false, false);
        }
    }

}

