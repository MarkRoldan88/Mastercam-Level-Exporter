using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Level_Exporter.Commands;
using Level_Exporter.Models;
using Mastercam.IO;
using System;
using System.Collections.Generic;

namespace Level_Exporter.ViewModels
{
    /// <summary>
    /// Level info view model, for use in datagrid
    /// </summary>
    public class LevelInfoViewModel : BaseViewModel
    {
        #region Construction
        public LevelInfoViewModel()
        {
            ReadMastercamLevels = new DelegateCommand(OnReadMastercamLevels);
            SelectAll = new DelegateCommand(OnSelectAll);
            _levels = new ObservableCollection<Level>();
        }
        #endregion
        
        #region Private fields

        private readonly ObservableCollection<Level> _levels;
        private bool _isSelectAll;
        private bool _isSyncButton;
        private bool _isSelected;
        private string _name;

        private Action<Level> checkAllAction;
        private delegate Dictionary<int,string> LevelInfoHandler();
        #endregion

        #region Public Properties

        //TODO Only allow numbers and letters in level datagrid cell
        /// <summary>
        /// Gets and sets name property for level name
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;

                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Gets and sets IsSelectAll for datagrid column header
        /// </summary>
        public bool IsSelectAll
        {
            get => _isSelectAll;
            set
            {
                _isSelectAll = value;
                OnPropertyChanged(nameof(IsSelectAll));
            }
        }

        /// <summary>
        /// Gets and sets IsSelected for level checkboxes, mirrored for setting Levels class
        /// </summary>
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        /// <summary>
        ///  Gets and sets List of levels for view
        /// </summary>
        public IEnumerable<Level> Levels => _levels;

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
        /// Gets ICommand for checking all level checkboxes
        /// </summary>
        public ICommand SelectAll { get; }

        #endregion

        #region Private Methods

        /// <summary>
        /// Command for button to read Mc Levels
        /// </summary>
        private void OnReadMastercamLevels()
        {
            // Refresh Level manager in Mastercam to get rid of empty levels, named levels are not removed
            LevelsManager.RefreshLevelsManager();

            if (LevelsManager.GetLevelNumbersWithGeometry().Length == 0) return;

            // Get Level Info- Key: level num , Value: level name
            LevelInfoHandler levelInfo = LevelInfo;
            
            IsSyncButton = true;

            _levels.Clear(); // Clear instead of comparing and doing a 'proper sync'

            foreach (var level in levelInfo())
            {
                _levels.Add(new Level
                {
                    Name = level.Value,
                    Number = level.Key,
                    EntityCount = LevelsManager.GetLevelEntityCount(level.Key, false),
                });
            }
        }

        /// <summary>
        /// Command for Checkbox in header, sets IsSelected property of each level in levels collection
        /// </summary>
        private void OnSelectAll()
        {
            if (_levels.Count == 0) return;

            foreach (var lvl in _levels)
            {
                if (lvl.IsSelected == IsSelectAll) continue;

                lvl.IsSelected = IsSelectAll;
            }
        }

        /// <summary>
        /// Get level numbers that contain geometry and convert to dictionary
        /// </summary>
        /// <returns></returns>
        private static Dictionary<int, string> LevelInfo() => LevelsManager.GetLevelNumbersWithGeometry()
            .ToDictionary(n => n, LevelsManager.GetLevelName);
        #endregion
    }

}

