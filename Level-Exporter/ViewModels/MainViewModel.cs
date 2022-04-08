// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="TODO: Company Name">
//   Copyright (c) 2022 TODO: Company Name
// </copyright>
// <summary>
//   Defines the MainViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Level_Exporter.Annotations;
using Level_Exporter.Commands;
using Level_Exporter.Services;
using Mastercam.IO;
using System.Windows;
using System.Windows.Input;


namespace Level_Exporter.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Forms;
    using Level_Exporter.Resources;
    using Mastercam.App.Exceptions;
    using Mastercam.Support;

    /// <summary>
    /// The main view model.
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        #region Construction

        public LevelInfoViewModel LevelInfoViewModel { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            this.LevelInfoViewModel = new LevelInfoViewModel();

            this.OkCommand = new DelegateCommand(OnOkCommand, CanOkCommand);
            this.CloseCommand = new DelegateCommand<Window>(OnCloseCommand);
            this.BrowseCommand = new DelegateCommand(OnBrowseCommand);

            this.DestinationDirectory = SettingsManager.CurrentDirectory;
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets the ok command.
        /// </summary>
        public ICommand OkCommand { get; }

        /// <summary>
        /// Gets the close command.
        /// </summary>
        public ICommand CloseCommand { get; }

        /// <summary>
        /// Gets the Browse button command
        /// </summary>
        public ICommand BrowseCommand { get; }

        #endregion

        #region Private Fields

        private ComboBoxItem _cadFormatSelected;
        private string _destinationDirectory;
        private int _nameIncrement;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or Sets Default File directory path for destination text box
        /// </summary>
        public string DestinationDirectory
        {
            get => _destinationDirectory;
            set
            {
                var chars = value.ToCharArray();
                var isValid = chars.Any(c =>
                    c != '\"' || c != '<' || c != '>' || c != '|' || c != '*' || c != '?' || c > 32 || c != '+');

                if (chars.Length == 0 || !isValid)
                {
                    value = string.Empty;
                }

                _destinationDirectory = Path.GetFullPath(value);
                OnPropertyChanged(nameof(DestinationDirectory));
            }
        }

        public ComboBoxItem CadFormatSelected
        {
            get => _cadFormatSelected;
            set
            {
                _cadFormatSelected = value;
                OnPropertyChanged(nameof(CadFormatSelected));
            }
        }

        /// <summary>
        /// The ok image resource name.
        /// </summary>
        [UsedImplicitly]
        public string OkResource => AppConstants.OkImage;

        /// <summary>
        /// The cancel image resource name.
        /// </summary>
        [UsedImplicitly]
        public string CancelResource => AppConstants.CancelImage;

        #endregion

        #region Private Methods

        /// <summary> The can ok command. Add logic as required. </summary>
        ///
        /// <returns> The <see cref="bool"/> True if enabled, false otherwise. </returns>
        private bool CanOkCommand() => true;

        /// <summary> Executes the ok command action. </summary>
        private void OnOkCommand()
        {
            // Gets our localized strings
            var title = ResourceReaderService.GetString("Title");
            var message = ResourceReaderService.GetString("OkButtonMessage");

            //DialogManager.YesNoCancel();
            DialogManager.OK(
                message.IsSuccess ? message.Value : "Ok Button Pressed",
                title.IsSuccess ? title.Value : "Mastercam");

            this.ExportHelper();
            

        }

        /// <summary> Executes the close command action. </summary>
        ///
        /// <param name="view"> The view. </param>
        private void OnCloseCommand(Window view) => view?.Close();

        private void OnBrowseCommand()
        {
            using (var folderDialog = new FolderBrowserDialog
            { Description = "Select Folder", SelectedPath = this.DestinationDirectory })
            {
                DialogResult result = folderDialog.ShowDialog();

                if (result != DialogResult.OK) return;

                this.DestinationDirectory = folderDialog.SelectedPath;
            }
        }

        private void ExportHelper()
        {
            // For checking if user has input duplicate level names
            var cachedNames = new Dictionary<string, int>();

            foreach (var level in this.LevelInfoViewModel.Levels)
            {
                if (!level.IsSelected) continue;

                if (cachedNames.ContainsKey(level.Name))
                    level.Name += _nameIncrement++.ToString();
                
                else cachedNames.Add(level.Name, 1);
                
                SearchManager.SelectAllGeometryOnLevel(level.Number, true);

                try
                {
                    if (CadFormatSelected.Content.ToString() == WindowStrings.CadTypeStl)
                    {
                        FileManager.WriteSTL(Path.Combine(DestinationDirectory, $"{level.Name}.{CadFormatSelected.Content}"), 0,
                            0.5, false,true, true, true, false);
                    }

                    if (CadFormatSelected.Content.ToString() != "STL" && FileManager.SaveSome(
                            Path.Combine(DestinationDirectory, $"{level.Name}.{CadFormatSelected.Content}"), true))
                    {
                        DialogManager.OK($"Levels exported to {DestinationDirectory} as {CadFormatSelected} files", "Success");
                    }
                }
                catch (Exception e)
                {
                    DialogManager.Exception(new MastercamException(
                        "Error Saving files to directory, double check path and make sure level names do not contain any symbols"));

                    Console.WriteLine(e);
                    throw;
                }





            }
        }

        #endregion
    }

}
