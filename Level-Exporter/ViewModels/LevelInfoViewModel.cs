namespace Level_Exporter.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Level_Exporter.Commands;
    using Level_Exporter.Models;
    using Mastercam.IO;

    public class LevelInfoViewModel : BaseViewModel
    {

        #region Construction
        public LevelInfoViewModel()
        {
            ReadMastercamLevels = new DelegateCommand(OnReadMastercamLevels);
            SelectAll = new DelegateCommand(OnSelectAll);
            Levels = new ObservableCollection<Level>();
        }
        #endregion
        
        #region Private fields

        private ObservableCollection<Level> _levels;
        private bool _isSelectAll;
        private bool _isSyncButton;
        private bool _isSelected;
        private string _name;
        #endregion

        #region Public Properties


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
            var levelInfo = LevelsManager.GetLevelNumbersWithGeometry()
                .ToDictionary(n => n, LevelsManager.GetLevelName);

            IsSyncButton = true;

            Levels.Clear(); // Clear instead of comparing and doing a 'proper sync'

            foreach (var level in levelInfo)
            {
                Levels.Add(new Level
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
            if (Levels.Count == 0) return;

            foreach (var lvl in Levels)
            {
                if (lvl.IsSelected == IsSelectAll) continue;

                lvl.IsSelected = IsSelectAll;
            }

        }

        #endregion

        #region Helpers

        #endregion
    }

}

