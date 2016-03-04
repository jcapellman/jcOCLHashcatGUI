using System.Windows;

using jcOCLHashcatGUI.WPF.Enums;
using jcOCLHashcatGUI.WPF.ViewModels;

using MahApps.Metro.Controls;

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
            viewModel.SaveSettings();

            fSettings.IsOpen = false;
        }
    }
}