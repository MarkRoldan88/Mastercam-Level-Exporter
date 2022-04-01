namespace Level_Exporter.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using System.Windows.Navigation;
    using Level_Exporter.Commands;
    using Level_Exporter.Models;
    using Mastercam.IO;
    using Mastercam.Support;

    public class LevelInfoViewModel : BaseViewModel
    {

        #region Constructor
        public LevelInfoViewModel()
        {
            ReadMastercamLevels = new DelegateCommand(OnReadMastercamLevels);
            ExportLevelGeometry = new DelegateCommand(OnExportLevelGeometry);
            Levels = new ObservableCollection<Level>();
        }

        #endregion

        #region Private properties
        private ObservableCollection<Level> _levels;
        private bool _isSyncButton;

        #endregion

        #region Public Properties

        /// <summary>
        ///  Gets and sets List of levels for view
        /// </summary>
        public ObservableCollection<Level> Levels
        {
            get => _levels;
            set
            {
                _levels = value;
                OnPropertyChanged(nameof(Levels)); // For MvvM Event
            }
        }

        /// <summary>
        ///  Gets and Sets bool for button state
        /// </summary>
        public bool IsSyncButton
        {
            get => _isSyncButton;
            set
            {
                _isSyncButton = value;
                OnPropertyChanged(nameof(IsSyncButton));
            }
        }

        #endregion

        #region Public Commands

        /// <summary>
        /// Gets ICommand for read mastercam levels button command
        /// </summary>
        public ICommand ReadMastercamLevels { get; }

        /// <summary>
        /// Gets ICommand for export levels geometry button command
        /// </summary>
        public ICommand ExportLevelGeometry { get; }

        #endregion

        #region Private Methods

        /// <summary>
        /// Command for button to read Mc Levels
        /// </summary>
        private void OnReadMastercamLevels()
        {
            if (LevelInfo().Item1.Count == 0) return;

            IsSyncButton = true;
            Levels.Clear(); // Clear instead of comparing and doing a 'proper sync'

            // Loop through mastercam level info and add new levels to observable collection
            for (int num = 1; num < LevelInfo().Item1.Count + 1; num++)
            {
                if (!LevelInfo().Item1.ContainsKey(num)) continue;

                Levels.Add(new Level
                {
                    Name = LevelInfo().Item1[num],
                    Number = num,
                    EntityCount = LevelInfo().Item2[num],
                    Geometries = SearchManager.GetGeometry(num),
                });
            }
        }

        private void OnExportLevelGeometry()
        {

        }

        #endregion

        #region Helpers

        /// <summary>
        ///  Gets Level name, number, and entity count
        /// </summary>
        /// <returns> Returns a Tuple containing two dictionaries
        /// </returns>
        private static Tuple<Dictionary<int, string>, Dictionary<int,int>> LevelInfo()
        {
            Dictionary<int, string> levelNameAndNum = LevelsManager.GetLevelNumbersWithGeometry()
                .ToDictionary(n => n, LevelsManager.GetLevelName);

            Dictionary<int,int> entityCount = 
                LevelsManager.GetLevelsEntityCounts(false)
                .Where(level => level.Value > 0) //Filter out levels with no entities
                .ToDictionary(n => n.Key, n => n.Value);

            return Tuple.Create(levelNameAndNum, entityCount);
        }

        #endregion
    }

}

