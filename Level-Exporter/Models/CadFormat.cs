using System;
using System.Collections.Generic;
using System.Linq;

namespace Level_Exporter.Models
{
    /// <summary>
    /// Cad Format class, has a file extension and description.
    /// </summary>
    public class CadFormat
    {
        #region Constructor
        public CadFormat(CadTypes cadType)
        {
            FileExtension = cadType.ToString().ToLower();
            Description = $"{GenerateDescription(cadType)} (*{FileExtension})";
        }
        #endregion

        #region Private Field

        private string _fileExtension;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets CAD Format Description
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets and Sets file extension, file extension is prepended with period character '.' // Example => .stl
        /// </summary>
        public string FileExtension
        {
            get => _fileExtension;
            private set => _fileExtension = $".{value}";
        }

        #endregion

        #region Helper Method

        /// <summary>
        /// Generates CAD format description, based on cad type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GenerateDescription(CadTypes type)
        {
            switch (type)
            {
                case CadTypes.Dwg:
                    return "AutoCAD Drawing File";

                case CadTypes.Dxf:
                    return "AutoCAD DXF File";

                case CadTypes.Emcam:
                    return "Mastercam Educ File";

                case CadTypes.Igs:
                case CadTypes.Iges:
                    return "Surface, IGES File";

                case CadTypes.Mcam:
                    return "Mastercam File";

                case CadTypes.Sat:
                    return "ACIS Kernel SAT File";

                case CadTypes.Stp:
                case CadTypes.Step:
                    return "STEP File";

                case CadTypes.Stl:
                    return "StereoLithography File";

                case CadTypes.Xb:
                    return "Parasolid Binary File";

                case CadTypes.X_t:
                case CadTypes.XmtTxt:
                    return "Parasolid Text File";

                default: return string.Empty;
            }
        }
        #endregion

        /// <summary>
        /// Create list of cad types from enum
        /// </summary>
        /// <returns>List of Cad formats</returns>
        public static List<CadFormat> GenerateCadChoiceList()
        {
            // Get Values from CadTypes enum
            var fileExtensions = Enum.GetValues(typeof(CadTypes)).Cast<CadTypes>();

            return fileExtensions.Select(ext => new CadFormat(ext)).ToList();
        }
    }

    #region CAD Type Enum
    /// <summary>
    /// Enum for CAD types or file extension
    /// </summary>
    public enum CadTypes
    {
        Dwg, Dxf, Emcam, Iges, Igs,
        Mcam, Sat, Step, Stl, Stp,
        Xb, XmtTxt, X_t
    }
    #endregion
    
}
