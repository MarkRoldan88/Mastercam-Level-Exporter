using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Level_Exporter.Annotations;

namespace Level_Exporter.Models
{
    public class Level : INotifyPropertyChanged
    {
        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Private Properties

        private bool _isSelected;
        private string _name;

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets and sets Level Name
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                var chars = value.ToCharArray();
                var isValid = chars.Any(c => // Check string for invalid path characters
                    c != '\"' || c != '<' || c != '>' || c != '|' || c != '*' || c != '?' || c > 32 || c != '+' ||
                    c != '/');

                if (chars.Length == 0 || !isValid)
                {
                    value = string.Empty;
                }

                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Gets and Sets Level number
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Gets and Sets Entity count
        /// </summary>
        public int EntityCount { get; set; }

        /// <summary>
        /// Gets and Sets isSelected
        /// </summary>
        public bool IsSelected { 
            get => _isSelected;
            set
            {
                if (value == _isSelected) return;

                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        #endregion
    }
}
