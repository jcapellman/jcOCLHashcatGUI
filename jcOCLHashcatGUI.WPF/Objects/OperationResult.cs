using jcOCLHashcatGUI.WPF.Enums;
using jcOCLHashcatGUI.WPF.Properties;

namespace jcOCLHashcatGUI.WPF.Objects {
    public class OperationResult {
        public ErrorTypes ErrorType;

        public string LocalizedErrorString { get; private set; }

        public OperationResult(ErrorTypes argErrorType) {
            ErrorType = argErrorType;

            LocalizedErrorString = Resources.ERROR_OCLHASHCAT_NOT_FOUND;
        }        
    }
}