// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseViewModel.cs" company="TODO: Company Name">
//   Copyright (c) 2022 TODO: Company Name
// </copyright>
// <summary>
//   The base view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Level_Exporter.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Level_Exporter.ViewModels
{
    /// <summary>
    /// The base view model.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The on property changed.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
