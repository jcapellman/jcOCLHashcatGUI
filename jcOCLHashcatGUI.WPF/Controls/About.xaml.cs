using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace jcOCLHashcatGUI.WPF.Controls {
    public partial class About : UserControl {
        public About() {
            InitializeComponent();
        }

        private void hl_RequestNavigate(object sender, RequestNavigateEventArgs e) {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}