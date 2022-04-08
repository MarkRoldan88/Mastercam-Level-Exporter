using System;
using System.IO;
using Level_Exporter.Resources;
using Mastercam.App.Exceptions;
using Mastercam.IO;

namespace Level_Exporter.Models
{
    /// <summary>
    /// Class for exporting/saving CAD
    /// </summary>
    public class CadExportHelper
    {
        #region Constructor

        public CadExportHelper(string destination, string cadFormat)
        {
            _destination = destination;
            _cadFormat = cadFormat;
        }

        public CadExportHelper(string destination, double stlResolution)
        {
            _destination = destination;
            _stlResolution = stlResolution;
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
        private readonly double _stlResolution = 0.75;

        #endregion

        #region Public Method

        /// <summary>
        /// Saves Cad entities within level, based on CAD format
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool SaveLevelCad(Level level)
        {
            return !string.Equals(_cadFormat, WindowStrings.CadTypeStl, StringComparison.CurrentCultureIgnoreCase)
                ? SaveAsCadFormat(level)
                : SaveAsStl(level);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Helper method for saving level CAD as STL
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private bool SaveAsStl(Level level)
        {
            try
            {
                return FileManager.WriteSTL(Path.Combine(_destination, $"{level.Name}.{_cadFormat}"), 0,
                    _stlResolution, false, true, true, true, false);
            }
            catch (Exception e)
            {
                DialogManager.Exception(new MastercamException(
        $"Error Saving Level {level.Number} files to directory, double check path and make sure level names do not contain any symbols"));
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Helper method for saving level CAD as every format, except STL
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private bool SaveAsCadFormat(Level level)
        {
            try
            {
                return FileManager.SaveSome(Path.Combine(_destination, $"{level.Name}.{_cadFormat}"), true);
            }
            catch (Exception e)
            {
                DialogManager.Exception(new MastercamException(
                    $"Error Saving Level {level.Number} files to directory, double check path and make sure level names do not contain any symbols"));
                Console.WriteLine(e);
                throw;
            }
        }
    }
    #endregion
}

