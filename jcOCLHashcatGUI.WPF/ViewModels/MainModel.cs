﻿using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using jcOCLHashcatGUI.WPF.Enums;
using jcOCLHashcatGUI.WPF.Helpers;
using jcOCLHashcatGUI.WPF.Objects;

namespace jcOCLHashcatGUI.WPF.ViewModels {
    public class MainModel : BaseModel {
        private List<KeyValuePair> _hashTypes;

        public List<KeyValuePair> SelectableHashTypes {
            get { return _hashTypes; }
            set { _hashTypes = value; OnPropertyChanged(); }
        }
        
        private KeyValuePair _selectedHashType;

        public KeyValuePair SelectedHashType {
            get { return _selectedHashType; }
            set { _selectedHashType = value; OnPropertyChanged(); }
        }

        private string _selectedHashfile;

        public string SelectedHashFile {
            get { return _selectedHashfile; }
            set { _selectedHashfile = value; OnPropertyChanged(); }
        }

        private bool _buttonRunHashcat;

        public bool ButtonRunHashcat {
            get {  return _buttonRunHashcat;}
            set { _buttonRunHashcat = value; OnPropertyChanged(); }
        }

        private string _selectedDictionary;

        public string SelectedDictionary {
            get { return _selectedDictionary; }
            set { _selectedDictionary = value; OnPropertyChanged(); }
        }

        private List<string> _dictionaries;

        public List<string> Dictionaries {
            get { return _dictionaries; }
            set { _dictionaries = value; OnPropertyChanged(); }
        }

        private string _hashcatOutput;

        public string HashcatOutput { get { return _hashcatOutput; } set { _hashcatOutput = value; OnPropertyChanged(); } }

        public OperationResult LoadData() {
            ButtonRunHashcat = false;

            SelectableHashTypes = EnumToKeyValuePair.ToKeyValuePair<HashTypes>();

            if (SelectableHashTypes != null) {
                SelectedHashType = SelectableHashTypes.FirstOrDefault();
            }

            if (!File.Exists(Config.GetConfigValue<string>(ConfigOptions.OCLHASHCAT_LOCATION))) {
                return new OperationResult(ErrorTypes.OCLHASHCAT_NOT_FOUND_AT_PATH);
            }

            Dictionaries =
                Config.GetConfigValue<DictionaryContainerItem>(ConfigOptions.DICTIONARIES)
                    .Dictionaries.Select(a => a.FilePath)
                    .ToList();

            if (Dictionaries.Any()) {
                SelectedDictionary = Dictionaries.FirstOrDefault();
            }

            ButtonRunHashcat = true;
            
            return new OperationResult(ErrorTypes.NONE);
        }
        
        public async Task<OperationResult> RunHashcat() {
            ButtonRunHashcat = false;

            var procInfo = new ProcessStartInfo(Config.GetConfigValue<string>(ConfigOptions.OCLHASHCAT_LOCATION)) {
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false,
                Arguments = $"-m {SelectedHashType.Value} {SelectedHashFile} {SelectedDictionary}"
            };

            var proc = new Process {
                StartInfo = procInfo
            };

            proc.Start();

            HashcatOutput = await proc.StandardOutput.ReadToEndAsync();

            ButtonRunHashcat = true;

            return new OperationResult(ErrorTypes.NONE);
        }
    }
}