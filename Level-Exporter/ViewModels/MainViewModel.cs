﻿// --------------------------------------------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// The main view model.
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
            this.OkCommand = new DelegateCommand(this.OnOkCommand, this.CanOkCommand);
            this.CloseCommand = new DelegateCommand<Window>(this.OnCloseCommand);
            this.OnReadLevelsCommand = new DelegateCommand(this.ReadMasterCamLevels);
        }

        #endregion

        private string _listItem;

        public List<string> LevelNames { get; set; }

        public string ListItem
        {
            get => _listItem;
            set
            {
                if (_listItem == value) return;
                _listItem = value;
                OnPropertyChanged(nameof(ListItem));
            }
        }

        #region Commands

        /// <summary>
        /// Gets the ok command.
        /// </summary>
        public ICommand OkCommand { get; }

        /// <summary>
        /// Gets the close command.
        /// </summary>
        public ICommand CloseCommand { get; }

        public ICommand OnReadLevelsCommand { get; }

        #endregion

        #region Public Properties

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

            DialogManager.OK(
                message.IsSuccess ? message.Value : "Ok Button Pressed",
                title.IsSuccess ? title.Value : "Mastercam");
        }

        /// <summary> Executes the close command action. </summary>
        ///
        /// <param name="view"> The view. </param>
        private void OnCloseCommand(Window view) => view?.Close();

        private void ReadMasterCamLevels()
        {
            this.ListItem = "test";
        }

        #endregion
    }

}
