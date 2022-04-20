using System.Collections.Generic;
using System.Linq;
using Mastercam.IO;
using System.Collections.ObjectModel;

namespace Level_Exporter.Models
{
    public class LevelInfoHelper : ILevelInfo
    {
        /// <summary>
        /// Observable collection for level data grid
        /// </summary>
        public ObservableCollection<Level> Levels { get; set; } = new ObservableCollection<Level>();

        /// <summary>
        /// Get level numbers that contain geometry and convert to dictionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetLevelsWithGeometry()
        {
            return LevelsManager.GetLevelNumbersWithGeometry().ToDictionary(n => n, LevelsManager.GetLevelName);
        }

        /// <summary>
        /// Get the amount of entities in a level
        /// </summary>
        /// <param name="num"></param>
        /// <returns>Amount of entities within a level that are NOT 'blanked'</returns>
        public int GetLevelEntityCount(int num) => LevelsManager.GetLevelEntityCount(num, false);

        /// <summary>
        /// Refreshes mastercam level manager (removes empty levels from mastercam list)
        /// </summary>
        public void RefreshLevelsManager() => LevelsManager.RefreshLevelsManager();
    }

    public interface ILevelInfo
    {
        Dictionary<int, string> GetLevelsWithGeometry();
        int GetLevelEntityCount(int num);
        void RefreshLevelsManager();
        ObservableCollection<Level> Levels { get; set; }
    }
}
