// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Main.cs" company="TODO: Company Name">
//   Copyright (c) 2022 TODO: Company Name
// </copyright>
// <summary>
//   If this project is helpful please take a short survey at ->
//   http://ux.mastercam.com/Surveys/APISDKSupport
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Level_Exporter.Properties;
using Level_Exporter.Services;
using Level_Exporter.ViewModels;
using Level_Exporter.Views;
using Mastercam.App;
using Mastercam.App.Exceptions;
using Mastercam.App.Types;
using Mastercam.IO;
using Mastercam.IO.Types;
using Mastercam.Support.UI;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace Level_Exporter
{
    /// <summary>
    /// The main class definition.
    /// </summary>
    public class Main : NetHook3App
    {
        #region Public Override Methods

        /// <summary> Initialize anything we need for the NET-Hook here. </summary>
        /// <param name="param"> System parameter. </param>
        /// <returns> A <c>MCamReturn</c> return type representing the outcome of your NetHook application. </returns>
        public override MCamReturn Init(int param)
        {
            // Wire up handler for any global exceptions not handled by the app
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
                this.HandleUnhandledException(args.ExceptionObject as Exception);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender, args) => this.HandleUnhandledException(args.Exception);

            if (Settings.Default.FirstTimeRunning)
            {
                var msg = ResourceReaderService.GetString("FirstTimeRunning");
                var assembly = Assembly.GetExecutingAssembly().FullName;
                EventManager.LogEvent(MessageSeverityType.InformationalMessage, assembly, msg.IsSuccess ? msg.Value : msg.Error);

                Settings.Default.FirstTimeRunning = false;
                Settings.Default.Save();
            }

            return base.Init(param);
        }

        /// <summary> The main entry point for your Level_Exporter. </summary>
        /// <param name="param"> System parameter. </param>
        /// <returns> A <c>MCamReturn</c> return type representing the outcome of your NetHook application. </returns>
        public override MCamReturn Run(int param)
        {
            // Create our view and assign the view model as its data context
            var view = new MainView
            {
                DataContext = new MainViewModel()
            };

            // Uncomment if you require a Modal dialog and remove all code
            // below up until the return statement.
            // var result = view.ShowDialog();

            // Get the handle to the non-WPF owner window
            var ownerWindowHandle = MastercamWindow.GetHandle().Handle;

            // Set the owned WPF window’s Owner property with the non-WPF owner window
            var _ = new System.Windows.Interop.WindowInteropHelper(view) { Owner = ownerWindowHandle };
            view.Show();

            return MCamReturn.NoErrors;
        }

        #endregion

        #region Public User Defined Methods

        /// <summary> The custom user function entry point for your Level_Exporter. </summary>
        /// <param name="param"> System parameter. </param>
        /// <returns> A <c>MCamReturn</c> return type representing the outcome of your NetHook application. </returns>
        public MCamReturn RunUserDefined(int param)
        {
            // read project resource strings
            var userMessage = ResourceReaderService.GetString("UserMessage");
            var title = ResourceReaderService.GetString("Title");

            DialogManager.OK(
                userMessage.IsSuccess ? userMessage.Value : userMessage.Error,
                title.IsSuccess ? title.Value : title.Error);
            return MCamReturn.NoErrors;
        }

        #endregion

        #region Private Methods

        /// <summary> Log exceptions and show a message. </summary>
        /// <param name="e"> The exception. </param>
        private void HandleUnhandledException(Exception e)
        {
            // Show the user
            DialogManager.Exception(new MastercamException(e.Message, e.InnerException));

            // Write to the event log
            var msg = e.InnerException != null ? e.InnerException.Message : e.Message;
            var assembly = Assembly.GetExecutingAssembly().FullName;
            EventManager.LogEvent(MessageSeverityType.ErrorMessage, assembly, msg);
        }

        #endregion
    }
}
