using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Level_Exporter.Annotations;
using System.Text.RegularExpressions;

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
                if (!IsLevelNameValid(value))
                {
                    _name = "Level";
                    OnPropertyChanged(nameof(Name));
                    return;
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

        /// <summary>
        /// Check if string contains unconventional characters (e.g. $%#)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static bool IsLevelNameValid(string s)
        {
            if (new Regex(@"[^0-9a-z.\w\s()]+").IsMatch(s)) return false;

            return s.ToCharArray().Any(c => // Check string for invalid path characters
                c > 32 || c != '\"' || c != '<' || c != '>' || c != '|' || c != '*' || c != '?' || c != '+' ||
                c != '/');
        }
    }
}
