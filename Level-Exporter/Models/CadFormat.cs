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
            FileExtension = cadType.ToString();
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
                case CadTypes.dwg:
                    return "AutoCAD Drawing File";

                case CadTypes.dxf:
                    return "AutoCAD DXF File";

                case CadTypes.emcam:
                    return "Mastercam Educ File";

                case CadTypes.igs:
                case CadTypes.iges:
                    return "Surface, IGES File";

                case CadTypes.mcam:
                    return "Mastercam File";

                case CadTypes.sat:
                    return "ACIS Kernel SAT File";

                case CadTypes.stp:
                case CadTypes.step:
                    return "STEP File";

                case CadTypes.stl:
                    return "StereoLithography File";

                case CadTypes.x_b:
                    return "Parasolid Binary File";

                case CadTypes.x_t:
                case CadTypes.xmt_txt:
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
    public enum CadTypes // ReSharper disable InconsistentNaming
    {
        dwg,
        dxf,
        emcam,
        iges,
        igs,
        mcam,
        sat,
        step,
        stl,
        stp,
        x_b,
        xmt_txt,
        x_t
    }
    #endregion
    
}
