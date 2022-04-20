using System.Collections.Generic;
using System.Linq;
using Mastercam.IO;
using System.Collections.ObjectModel;

namespace Level_Exporter.Models
{
    public class LevelInfoHelper : ILevelInfo
    {
        public ObservableCollection<Level> Levels { get; set; } = new ObservableCollection<Level>();

        /// <summary>
        /// Get level numbers that contain geometry and convert to dictionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetLevelsWithGeometry()
        {
            return LevelsManager.GetLevelNumbersWithGeometry().ToDictionary(n => n, LevelsManager.GetLevelName);
        }

        public int GetLevelEntityCount(int num) => LevelsManager.GetLevelEntityCount(num, false);

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
