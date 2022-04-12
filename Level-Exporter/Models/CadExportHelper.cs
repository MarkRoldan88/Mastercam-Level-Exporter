using System;
using System.IO;
using Mastercam.App.Exceptions;
using Mastercam.IO;

namespace Level_Exporter.Models
{
    /// <summary>
    /// Helper Class for exporting/saving CAD
    /// </summary>
    public class CadExportHelper
    {
        #region Constructor

        public CadExportHelper(string destination)
        {
            _destination = destination;
        }

        public CadExportHelper(string destination, string cadFormat) : this(destination)
        {
            _destination = destination;
            _cadFormat = cadFormat;
        }

        public CadExportHelper(string destination, string cadFormat, double stlResolution) : this(destination,
            cadFormat)
        {
            if (stlResolution != 0.0)
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
        private readonly string _cadFormat = "stl";

        /// <summary>
        /// STL resolution
        /// </summary>
        private readonly double _stlResolution = 0.75;

        /// <summary>
        /// Full path created from level name and cad format (file extension)
        /// </summary>
        private string _fullPath;

        #endregion

        #region Public Method

        /// <summary>
        /// Saves Cad entities within level, based on CAD format
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool SaveLevelCad(Level level)
        {
            try
            {
                if (_destination == string.Empty || _destination is null)
                    return FileManager.SaveSome(SettingsManager.CurrentDirectory, true);

                _fullPath = Path.Combine(_destination, $"{level.Name}{_cadFormat}");

                return _cadFormat == CadTypes.stl.ToString() ? SaveAsStl() : SaveAsCadFormat();
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

        #region Private Methods

        /// <summary>
        /// Helper method for saving level CAD as STL
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private bool SaveAsStl() => 
            FileManager.WriteSTL(_fullPath, 0, _stlResolution, false, true, true, true, false);

        /// <summary>
        /// Helper method for saving level CAD as every format, except STL
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private bool SaveAsCadFormat() => FileManager.SaveSome(_fullPath, true);
    }
    #endregion
}

