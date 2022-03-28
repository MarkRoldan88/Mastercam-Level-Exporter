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
        private bool _isSyncButton;
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

        public ICommand ReadMastercamLevels { get; }

        #endregion

        #region Private Methods

        private void OnReadMastercamLevels()
        {
            IsSyncButton = true;
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
