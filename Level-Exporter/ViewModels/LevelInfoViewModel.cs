using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Level_Exporter.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using System.Windows.Navigation;
    using Level_Exporter.Commands;
    using Level_Exporter.Models;
    using Mastercam.IO;

    public class LevelInfoViewModel : BaseViewModel
    {

        #region Constructor
        public LevelInfoViewModel()
        {
            ReadMastercamLevels = new DelegateCommand(OnReadMastercamLevels);
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

        #endregion

        #region Private Methods

        /// <summary>
        /// Command for button to read Mc Levels
        /// </summary>
        private void OnReadMastercamLevels()
        {
            if (GetLevelNameAndNumber().Count == 0) return;
            
            IsSyncButton = true; 
            Levels.Clear(); // Clear instead of comparing and doing a 'proper sync'

            foreach (var level in GetLevelNameAndNumber())
            {
                Levels.Add(new Level() { Name = level.Value, Number = level.Key });
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Get Dictionary of Level name and number
        /// </summary>
        /// <param name="levelNumbers"></param>
        /// <returns></returns>
        private static Dictionary<int, string> GetLevelNameAndNumber()
        {
            return LevelsManager.GetLevelNumbersWithGeometry().ToDictionary(n => n, LevelsManager.GetLevelName);
        }


        #endregion
    }

}

