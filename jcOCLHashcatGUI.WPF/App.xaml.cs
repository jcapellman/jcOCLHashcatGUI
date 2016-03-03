using System.Windows;

using jcOCLHashcatGUI.WPF.Enums;
using jcOCLHashcatGUI.WPF.Objects;

namespace jcOCLHashcatGUI.WPF {
    public partial class App : Application {
        public App() {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(new Configurator().GetConfigValue<string>(ConfigOptions.LANGUAGE));
        }
    }
}