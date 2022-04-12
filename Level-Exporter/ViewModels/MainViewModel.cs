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
using Mastercam.IO;
using System.Windows;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Level_Exporter.Models;
using Mastercam.IO.Types;
using Mastercam.Support;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;


namespace Level_Exporter.ViewModels
{
    /// <summary>
    /// The main view model.
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        #region Construction
        /// <summary>
        /// Gets LevelInfoViewModel
        /// </summary>
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
            this.PreviewTextInputCommand = new DelegateCommand<TextCompositionEventArgs>(OnPreviewTextInput);

            this.DestinationDirectory = SettingsManager.CurrentDirectory;

            this.CadFormatChoices = new ObservableCollection<CadFormat>(GenerateCadChoiceList());
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

        /// <summary>
        /// Gets the Preview Text Input command
        /// </summary>
        public ICommand PreviewTextInputCommand { get; }

        #endregion

        #region Private Fields

        private CadFormat _cadFormatSelected;

        private ObservableCollection<CadFormat> _cadFormatChoiceChoices;

        private string _destinationDirectory;

        private int _nameIncrement;

        private double _stlResolution = 0.02;

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
                var chars = value.ToCharArray(); //TODO change to method
                var isValid = chars.Any(c => // Check string for invalid chars
                    c != '\"' || c != '<' || c != '>' || c != '|' || c != '*' || c != '?' || c > 32 || c != '+');

                if (chars.Length == 0 || !isValid)
                {
                    value = string.Empty;
                }

                _destinationDirectory = Path.GetFullPath(value);
                OnPropertyChanged(nameof(DestinationDirectory));
            }
        }

        /// <summary>
        /// Gets and sets STL resolution from textbox
        /// </summary>
        public double StlResolution
        {
            get => _stlResolution;
            set
            {
                _stlResolution = value;
                OnPropertyChanged(nameof(StlResolution));
            }
        }

        /// <summary>
        /// Gets and Sets cad format selected
        /// </summary>
        public CadFormat CadFormatSelected
        {
            get => _cadFormatSelected;
            set
            {
                _cadFormatSelected = value;
                OnPropertyChanged(nameof(CadFormatSelected));
            }
        }

        /// <summary>
        /// Gets and Sets Cad format choices for combobox
        /// </summary>
        public ObservableCollection<CadFormat> CadFormatChoices
        {
            get => _cadFormatChoiceChoices;
            set
            {
                _cadFormatChoiceChoices = value;
                OnPropertyChanged(nameof(CadFormatChoices));
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

        /// <summary>
        /// The can ok command. Add logic as required.
        /// </summary>
        /// <returns> The <see cref="bool"/> True if enabled, false otherwise. </returns>
        private bool CanOkCommand() => true;

        /// <summary>
        /// Executes OkCommand, button for exporting/saving level entities
        /// </summary>
        private void OnOkCommand()
        {
            //TODO Check if fields are null/blank? (is valid method?)
            // User confirm
            if (DialogManager.YesNoCancel(
                    $"Export selected levels as {this.CadFormatSelected.FileExtension} files to {this.DestinationDirectory}?",
                    "Confirm") != DialogReturnType.Yes) return;

            var cadExportHelper = 
                new CadExportHelper(this.DestinationDirectory, this.CadFormatSelected.FileExtension, this.StlResolution);

            // For checking if user has input duplicate level names
            var cachedNames = new Dictionary<string, int>();

            var isSuccess = false;

            foreach (var level in this.LevelInfoViewModel.Levels)
            {
                if (!level.IsSelected) continue;

                if (level.Name == string.Empty)
                    level.Name = $"level{level.Number}";

                if (cachedNames.ContainsKey(level.Name)) // If level name has been used, append a number to avoid duplicate file names
                    level.Name += _nameIncrement++.ToString();
                else 
                    cachedNames.Add(level.Name, 1); // Add level name to cached names

                // Mastercam select levels
                SearchManager.SelectAllGeometryOnLevel(level.Number, true);

                isSuccess = cadExportHelper.SaveLevelCad(level);
            }

            if (isSuccess)
                DialogManager.OK(
                    $"Level entities saved to {this.DestinationDirectory} as {this.CadFormatSelected.FileExtension} files",
                    "Success!");
        }

        /// <summary> Executes the close command action. </summary>
        ///
        /// <param name="view"> The view. </param>
        private void OnCloseCommand(Window view) => view?.Close();

        /// <summary>
        /// Executes the browse command action. Used for Browsing to output directory
        /// </summary>
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

        /// <summary>
        /// Command for handling OnPreviewTextInput event, check for valid characters
        /// </summary>
        /// <param name="e">Text composition event args from text box</param>
        private void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            var isTextAllowed = new Regex("[^0-9.]+").IsMatch(e.Text);

            if (isTextAllowed) e.Handled = true;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Create list of cad types from enum
        /// </summary>
        /// <returns>List of Cad formats</returns>
        private List<CadFormat> GenerateCadChoiceList()
        {
            // Get Values from CadTypes enum
            var fileExtensions = Enum.GetValues(typeof(CadTypes)).Cast<CadTypes>();

            return fileExtensions.Select(ext => new CadFormat(ext)).ToList();
        }

        #endregion
    }
}



