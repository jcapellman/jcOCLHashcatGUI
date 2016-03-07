using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using jcOCLHashcatGUI.WPF.Enums;
using jcOCLHashcatGUI.WPF.Objects;
using jcOCLHashcatGUI.WPF.ViewModels;

using Microsoft.Win32;

namespace jcOCLHashcatGUI.WPF.Controls {

    public partial class SettingsControl : UserControl {
        private SettingsModel viewModel => (SettingsModel) DataContext;

        public EventHandler ReadyForClose;

        public SettingsControl() {
            InitializeComponent();

            DataContext = new SettingsModel();

            viewModel.LoadData();
        }

        private void btnBrowseForHashCat_OnClick(object sender, RoutedEventArgs e) {
            var fileDialog = new OpenFileDialog {
                DefaultExt = ".exe",
                Filter = "Hashcat(.exe)|*.exe"
            };

            var result = fileDialog.ShowDialog();

            if (result == false) {
                return;
            }

            viewModel.SelectedHashcatLocation = fileDialog.FileName;
        }

        private void btnClosePopup_OnClick(object sender, RoutedEventArgs e) {
            var result = viewModel.Save();

            if (result.ErrorType != ErrorTypes.NONE) {
                MessageBox.Show(result.LocalizedErrorString);

                return;
            }

            ReadyForClose(this, null);
        }

        private void btnBrowseForDictionaries(object sender, RoutedEventArgs e) {
            var fileDialog = new OpenFileDialog {
                Multiselect = true
            };

            var result = fileDialog.ShowDialog();

            if (result == false) {
                return;
            }
            
            viewModel.Dictionaries.AddRange(fileDialog.FileNames.Select(a => new DictionaryItem {FilePath = a, IsChecked = true}).ToList());

            viewModel.Dictionaries = viewModel.Dictionaries;
        }
    }
}