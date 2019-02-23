using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

using jcOCLHashcatGUI.Enums;
using jcOCLHashcatGUI.Helpers;
using jcOCLHashcatGUI.Objects;

namespace jcOCLHashcatGUI.ViewModels {
    public class SettingsModel : BaseModel {
        private string _selectedHashcatLocation;

        public string SelectedHashcatLocation { get { return _selectedHashcatLocation; } set { _selectedHashcatLocation = value; OnPropertyChanged(); } }

        private KeyValuePair<string, SupportedLanguages> _selectedLanguage;

        public KeyValuePair<string, SupportedLanguages> SelectedLanguage {
            get => _selectedLanguage;
            set { _selectedLanguage = value; OnPropertyChanged(); }
        }

        private List<KeyValuePair<string, SupportedLanguages>> _selectableLanguages;

        public List<KeyValuePair<string, SupportedLanguages>> SelectableLanguages {
            get => _selectableLanguages;
            set { _selectableLanguages = value; OnPropertyChanged(); }
        }

        private ObservableCollection<DictionaryItem> _dictionaries;

        public ObservableCollection<DictionaryItem> Dictionaries {
            get => _dictionaries;
            set { _dictionaries = value; OnPropertyChanged(); }
        }  

        public OperationResult LoadData() {
            SelectableLanguages = EnumToKeyValuePair.ToKeyValuePair<SupportedLanguages>(replaceUnderscoreCharacter: "-");

            var selectedLanguage =
                SelectableLanguages.FirstOrDefault(a => a.Key == Config.GetConfigValue<string>(ConfigOptions.LANGUAGE));

            SelectedLanguage = !string.IsNullOrEmpty(selectedLanguage.Key) ? selectedLanguage : SelectableLanguages.FirstOrDefault();
            
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
            Config.UpdateConfigValue(ConfigOptions.DICTIONARIES, new DictionaryContainerItem { Dictionaries = new List<DictionaryItem>(Dictionaries.Where(a => a.IsChecked))});

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

            return duplicatesAttempted ? new OperationResult(ErrorTypes.DUPLICATE_DICTIONARIES) : new OperationResult(ErrorTypes.NONE);
        }
    }
}