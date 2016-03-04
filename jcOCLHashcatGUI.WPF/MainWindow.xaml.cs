using System.Windows;

using jcOCLHashcatGUI.WPF.Enums;
using jcOCLHashcatGUI.WPF.ViewModels;

using MahApps.Metro.Controls;
using Microsoft.Win32;

namespace jcOCLHashcatGUI.WPF {
    public partial class MainWindow : MetroWindow {
        private MainModel viewModel => (MainModel) DataContext;

        public MainWindow() {
            InitializeComponent();

            DataContext = new MainModel();

            var result = viewModel.LoadData();

            if (result.ErrorType == ErrorTypes.NONE) {
                return;
            }

            MessageBox.Show(result.LocalizedErrorString);
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e) { var result = await viewModel.RunHashcat(); }

        private void btnSettings_OnClick(object sender, RoutedEventArgs e) { fSettings.IsOpen = true; }

        private void btnClosePopup_OnClick(object sender, RoutedEventArgs e) {
            var result = viewModel.SaveSettings();

            if (result.ErrorType != ErrorTypes.NONE) {
                MessageBox.Show(result.LocalizedErrorString);
            }

            fSettings.IsOpen = false;
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
    }
}