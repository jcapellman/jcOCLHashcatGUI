using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

using jcOCLHashcatGUI.WPF.Enums;
using jcOCLHashcatGUI.WPF.Helpers;
using jcOCLHashcatGUI.WPF.Objects;

namespace jcOCLHashcatGUI.WPF.ViewModels {
    public class SettingsModel : BaseModel {
        private string _selectedHashcatLocation;

        public string SelectedHashcatLocation { get { return _selectedHashcatLocation; } set { _selectedHashcatLocation = value; OnPropertyChanged(); } }

        private KeyValuePair _selectedLanguage;

        public KeyValuePair SelectedLanguage {
            get { return _selectedLanguage; }
            set { _selectedLanguage = value; OnPropertyChanged(); }
        }

        private List<KeyValuePair> _selectableLanguages;

        public List<KeyValuePair> SelectableLanguages {
            get { return _selectableLanguages; }
            set { _selectableLanguages = value; OnPropertyChanged(); }
        }

        private ObservableCollection<DictionaryItem> _dictionaries;

        public ObservableCollection<DictionaryItem> Dictionaries {
            get {  return _dictionaries; }
            set { _dictionaries = value; OnPropertyChanged(); }
        }  

        public OperationResult LoadData() {
            SelectableLanguages = EnumToKeyValuePair.ToKeyValuePair<SupportedLanguages>(replaceUnderscoreCharacter: "-");

            SelectedLanguage = SelectableLanguages.FirstOrDefault(a => a.Key == Config.GetConfigValue<string>(ConfigOptions.LANGUAGE)) ?? SelectableLanguages.FirstOrDefault();

            SelectedHashcatLocation = Config.GetConfigValue<string>(ConfigOptions.OCLHASHCAT_LOCATION);

            Dictionaries = new ObservableCollection<DictionaryItem>(Config.GetConfigValue<DictionaryContainerItem>(ConfigOptions.DICTIONARIES).Dictionaries);

            return new OperationResult(ErrorTypes.NONE);
        }

        public OperationResult Save() {
            var result = new OperationResult(ErrorTypes.NONE);

            var currentLanguage = Config.GetConfigValue<string>(ConfigOptions.LANGUAGE);

            if (currentLanguage != SelectedLanguage.Key) {  
                result = new OperationResult(ErrorTypes.LANGUAGE_CHANGED);
            }

            if (!File.Exists(SelectedHashcatLocation)) {
                return new OperationResult(ErrorTypes.OCLHASHCAT_NOT_FOUND_AT_PATH);
            }

            Config.UpdateConfigValue(ConfigOptions.LANGUAGE, SelectedLanguage.Key);
            Config.UpdateConfigValue(ConfigOptions.OCLHASHCAT_LOCATION, SelectedHashcatLocation);
            Config.UpdateConfigValue(ConfigOptions.DICTIONARIES, new DictionaryContainerItem { Dictionaries = new List<DictionaryItem>(Dictionaries)});

            return result;
        }

        public OperationResult AddDictionaries(string[] fileNames) {
            var duplicatesAttempted = false;

            foreach (var item in fileNames) {
                if (Dictionaries.Any(a => a.FilePath == item)) {
                    duplicatesAttempted = true;
                    continue;
                }

                Dictionaries.Add(new DictionaryItem {
                    FilePath = item,
                    IsChecked = true
                });
            }

            if (duplicatesAttempted) {
                return new OperationResult(ErrorTypes.DUPLICATE_DICTIONARIES);
            }

            return new OperationResult(ErrorTypes.NONE);
        }
    }
}