using jcOCLHashcatGUI.WPF.Enums;
using jcOCLHashcatGUI.WPF.Properties;

namespace jcOCLHashcatGUI.WPF.Objects {
    public class OperationResult {
        public ErrorTypes ErrorType;

        public string LocalizedErrorString { get; private set; }

        public OperationResult(ErrorTypes argErrorType) {
            ErrorType = argErrorType;

            switch (ErrorType) {
                case ErrorTypes.OCLHASHCAT_NOT_FOUND_AT_PATH:
                    LocalizedErrorString = Resources.ERROR_OCLHASHCAT_NOT_FOUND;
                    break;
                case ErrorTypes.LANGUAGE_CHANGED:
                    LocalizedErrorString = Resources.MAINWINDOW_SETTINGS_LANGUAGE_CHANGE;
                    break;
                case ErrorTypes.DUPLICATE_DICTIONARIES:
                    LocalizedErrorString = Resources.ERROR_SETTINGS_DUPLICATE_DICTIONARY;
                    break;
            }
        }        
    }
}