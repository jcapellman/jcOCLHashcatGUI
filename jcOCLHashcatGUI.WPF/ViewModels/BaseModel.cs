using System.ComponentModel;
using System.Runtime.CompilerServices;

using jcOCLHashcatGUI.WPF.Objects;

namespace jcOCLHashcatGUI.WPF.ViewModels {
    public class BaseModel : INotifyPropertyChanged {
        internal Configurator Config;

        public BaseModel() {
            Config = new Configurator();    
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
    }
}