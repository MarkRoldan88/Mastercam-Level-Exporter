using System.Collections.Generic;
using System.Linq;
using Mastercam.IO;
using System.Collections.ObjectModel;

namespace Level_Exporter.Models
{
    public class LevelInfoHelper : ILevelInfo
    {
        public static int[] CachedVisibleLevelNumbers { get; private set; } = LevelsManager.GetVisibleLevelNumbers();

        /// <summary>
        /// Observable collection for level data grid
        /// </summary>
        public ObservableCollection<Level> Levels { get; set; } = new ObservableCollection<Level>();

        /// <summary>
        /// Get level numbers that contain geometry and convert to dictionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetLevelsWithGeometry() => LevelsManager.GetLevelNumbersWithGeometry()
            .ToDictionary(n => n, LevelsManager.GetLevelName);

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

        public void UpdateCachedVisibleLevels()
        {
            CachedVisibleLevelNumbers = LevelsManager.GetVisibleLevelNumbers();
        }
    }

    public interface ILevelInfo
    {
        int GetLevelEntityCount(int num);

        void RefreshLevelsManager();
        void UpdateCachedVisibleLevels();

        Dictionary<int, string> GetLevelsWithGeometry();

        ObservableCollection<Level> Levels { get; set; }
    }
}
