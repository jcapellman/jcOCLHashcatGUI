using System;
using System.Collections.Generic;
using System.IO;

using jcOCLHashcatGUI.WPF.Enums;
using Newtonsoft.Json;

namespace jcOCLHashcatGUI.WPF.Objects {
    public class Configurator {
        private readonly Dictionary<ConfigOptions, object> _options;

        private const string DEFAULT_SETTINGS_FILE = "settings.json";
        private const string DEFAULT_OCLHASHCAT_LOCATION = "oclhashcat64.exe";

        private void LoadDefaults() {
            _options.Add(ConfigOptions.OCLHASHCAT_LOCATION, DEFAULT_OCLHASHCAT_LOCATION);
        }

        private void writeSettings() => File.WriteAllText(DEFAULT_SETTINGS_FILE, JsonConvert.SerializeObject(_options));

        public Configurator() {
            _options = new Dictionary<ConfigOptions, object>();

            if (!File.Exists(DEFAULT_SETTINGS_FILE)) {
                LoadDefaults();

                writeSettings();

                return;
            }

            var data = File.ReadAllText(DEFAULT_SETTINGS_FILE);

            _options = JsonConvert.DeserializeObject<Dictionary<ConfigOptions, object>>(data);
        }

        public T GetConfigValue<T>(ConfigOptions configOption) {
            if (_options.ContainsKey(configOption)) {
                return (T) _options[configOption];
            }

            switch (configOption) {
                case ConfigOptions.OCLHASHCAT_LOCATION:
                    return (T)Convert.ChangeType(DEFAULT_OCLHASHCAT_LOCATION, typeof(T));
            }

            return default(T);
        }

        public void UpdateConfigValue<T>(ConfigOptions configOption, T value) {
            if (_options.ContainsKey(configOption)) {
                _options[configOption] = value;
            } else {
                _options.Add(configOption, value);
            }

            writeSettings();
        }
    }
}