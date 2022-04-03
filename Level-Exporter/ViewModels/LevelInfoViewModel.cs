namespace Level_Exporter.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Data;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Navigation;
    using Level_Exporter.Annotations;
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
            // Refresh Level manager in Mastercam to get rid of empty levels, named levels are not removed
            LevelsManager.RefreshLevelsManager();

            if (LevelInfo().Count == 0) return;

            IsSyncButton = true;
            Levels.Clear(); // Clear instead of comparing and doing a 'proper sync'
            
            foreach (var level in LevelInfo())
            {
                Levels.Add(new Level
                {
                    Name = level.Value,
                    Number = level.Key,
                    EntityCount = LevelsManager.GetLevelEntityCount(level.Key, false),
                    Geometries = SearchManager.GetGeometry(level.Key),
                });
            }
        }

        private void OnSelectAll()
        {
            // TODO Logic for switching isSelected bool in levels class
        }

        #endregion

        #region Helpers

        /// <summary>
        ///  Gets Level name, number, and entity count
        /// </summary>
        /// <returns> Returns a Tuple containing two dictionaries
        /// </returns>
        private static Dictionary<int, string> LevelInfo()
        {
            return LevelsManager.GetLevelNumbersWithGeometry().ToDictionary(n => n, LevelsManager.GetLevelName);
        }

        #endregion
    }

}

