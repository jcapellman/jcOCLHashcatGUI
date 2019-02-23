using System.Windows;

using jcOCLHashcatGUI.Enums;
using jcOCLHashcatGUI.Objects;

namespace jcOCLHashcatGUI {
    public partial class App : Application {
        public App() {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(new Configurator().GetConfigValue<string>(ConfigOptions.LANGUAGE));
        }
    }
}