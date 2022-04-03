using System;
using System.Collections.Generic;
using System.Linq;

namespace Level_Exporter.Models
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Level_Exporter.Annotations;
    using Mastercam.Database;

    public class Level : INotifyPropertyChanged
    {
        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        #region Private Properties

        private bool _isSelected;

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets and sets Level Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets and Sets Level number
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets and Sets Entity count
        /// </summary>
        public int EntityCount { get; set; }

        /// <summary>
        /// Gets and Sets Geometries
        /// </summary>
        public Geometry[] Geometries { get; set; }

        /// <summary>
        /// Gets and Sets isSelected
        /// </summary>
        public bool IsSelected { 
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        #endregion
    }
}
