using jcOCLHashcatGUI.WPF.ViewModels;
using MahApps.Metro.Controls;

namespace jcOCLHashcatGUI.WPF {
    public partial class MainWindow : MetroWindow {
        private MainModel viewModel => (MainModel) DataContext;

        public MainWindow() {
            InitializeComponent();

            DataContext = new MainModel();

            viewModel.LoadData();
        }
    }
}