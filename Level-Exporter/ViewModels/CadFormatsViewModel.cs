using System.Collections.Generic;
using System.Collections.ObjectModel;
using Level_Exporter.Models;

namespace Level_Exporter.ViewModels
{
    public class CadFormatsViewModel
    {
        public CadFormatsViewModel()
        {
            CadTypesHandler choices = CadFormat.GenerateCadChoiceList;

            _cadFormatChoiceChoices = new ObservableCollection<CadFormat>(choices());
        }

        private readonly ObservableCollection<CadFormat> _cadFormatChoiceChoices;

        private delegate List<CadFormat> CadTypesHandler();

        /// <summary>
        /// Gets and Sets Cad format choices for combobox
        /// </summary>
        public IEnumerable<CadFormat> CadFormats => _cadFormatChoiceChoices;
    }
}
