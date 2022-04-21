using System.Collections.ObjectModel;
using System.Windows.Input;
using Level_Exporter.Commands;
using Level_Exporter.Models;
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
            ReadMastercamLevels = new DelegateCommand(OnReadMastercamLevels, CanReadMastercamLevels);
            SelectAll = new DelegateCommand(OnSelectAll, CanSelectAll);
        }

        #endregion

        #region Private fields

        private bool _isSelectAll;
        private bool _isSyncButton;
        private bool _isSelected;
        private string _name;

        private readonly ObservableCollection<Level> _levels = LevelInfoHelper.Levels;

        private delegate int EntityHandler(int n);
        private delegate Dictionary<int, string> LevelInfoHandler();
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets and sets Interface for getting Mastercam level info
        /// </summary>
        public static ILevelInfo LevelInfoHelper { get; set; } = new LevelInfoHelper();

        /// <summary>
        ///  Gets and sets List of levels for view
        /// </summary>
        public IEnumerable<Level> Levels => _levels;

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
        /// Can Read Mastercam levels command, checks to do before execution.
        /// </summary>
        /// <returns></returns>
        private bool CanReadMastercamLevels()
        {
            // Refresh Level manager in Mastercam to get rid of empty levels, named levels are not removed
            LevelInfoHelper.RefreshLevelsManager();

            return LevelInfoHelper.GetLevelsWithGeometry().Count != 0;
        }

        /// <summary>
        /// Command for button to read Mc Levels
        /// </summary>
        private void OnReadMastercamLevels()
        {
            // Get Level Info- Key: level num , Value: level name
            LevelInfoHandler levels = LevelInfoHelper.GetLevelsWithGeometry;
            EntityHandler entities = LevelInfoHelper.GetLevelEntityCount;

            IsSyncButton = true;

            _levels.Clear(); // Clear instead of comparing and doing a 'proper sync'/compare

            foreach (KeyValuePair<int, string> lvl in levels())
            {
                _levels.Add(new Level
                {
                    Name = lvl.Value,
                    Number = lvl.Key,
                    EntityCount = entities(lvl.Key)
                });
            }
        }

        /// <summary>
        /// Can Select All command bool, checks done before execution.
        /// </summary>
        /// <returns>bool indicating if corresponding command can execute</returns>
        private bool CanSelectAll() => _levels.Count != 0;

        /// <summary>
        /// Command for Checkbox in header, sets IsSelected property of each level in levels collection
        /// </summary>
        private void OnSelectAll()
        {
            foreach (var lvl in _levels)
            {
                if (lvl.IsSelected != IsSelectAll)
                    lvl.IsSelected = IsSelectAll;
            }
        }
        #endregion
    }
}

