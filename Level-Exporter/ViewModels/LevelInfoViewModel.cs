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
        }

        #endregion

        #region Private properties
        private ObservableCollection<string> _levelNames;
        private ObservableCollection<int> _levelNumbers;
        private bool _levelListIsPopulated;

        #endregion

        #region Public Properties
        public ObservableCollection<string> LevelNames
        {
            get => _levelNames;
            set
            {
                _levelNames = value;
                OnPropertyChanged(nameof(LevelNames));
            }
        }

        public ObservableCollection<int> LevelNumbers
        {
            get => _levelNumbers;
            set
            {
                _levelNumbers = value;
                OnPropertyChanged(nameof(LevelNumbers));
            }
        }

        public bool LevelListIsPopulated
        {
            get => _levelListIsPopulated;
            set
            {
                _levelListIsPopulated = LevelNames?.Count > 0 || value;
                OnPropertyChanged(nameof(LevelListIsPopulated));
            }
        }

        #endregion

        #region Public Commands

        public ICommand ReadMastercamLevels { get; }

        #endregion

        #region Private Methods

        private void OnReadMastercamLevels()
        {
            if (LevelsManager.GetLevelNumbersWithGeometry().Length > 0)
            {
                LevelNumbers = new ObservableCollection<int>(LevelsManager.GetLevelNumbersWithGeometry());
                LevelNames = new ObservableCollection<string>(LevelsManager.GetLevelNumbersWithGeometry()
                    .Select(LevelsManager.GetLevelName));

                LevelListIsPopulated = true;
            }
        }

        #endregion
    }
}
