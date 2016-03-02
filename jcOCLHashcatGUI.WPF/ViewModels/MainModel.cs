using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

using jcOCLHashcatGUI.WPF.Enums;
using jcOCLHashcatGUI.WPF.Helpers;
using jcOCLHashcatGUI.WPF.Objects;

namespace jcOCLHashcatGUI.WPF.ViewModels {
    public class MainModel : INotifyPropertyChanged {
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

        public void LoadData() {
            SelectableHashTypes = EnumToKeyValuePair.ToKeyValuePair<HashTypes>();

            if (SelectableHashTypes != null) {
                SelectedHashType = SelectableHashTypes.FirstOrDefault();
            }
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
  }
}