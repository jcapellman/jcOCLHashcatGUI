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

            if (result == ErrorTypes.NONE) {
                return;
            }

            MessageBox.Show(result.ToString());
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e) { var result = await viewModel.RunHashcat(); }
    }
}