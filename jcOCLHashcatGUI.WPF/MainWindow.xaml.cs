﻿using System.Windows;

using jcOCLHashcatGUI.Controls;
using jcOCLHashcatGUI.Enums;
using jcOCLHashcatGUI.ViewModels;

using MahApps.Metro.Controls;

using Microsoft.Win32;

namespace jcOCLHashcatGUI {
    public partial class MainWindow : MetroWindow {
        private MainModel viewModel => (MainModel) DataContext;

        public MainWindow() {
            InitializeComponent();

            settingsControl.ReadyForClose += (sender, args) => fSettings.IsOpen = false;

            DataContext = new MainModel();

            var result = viewModel.LoadData();

            if (result.ErrorType == ErrorTypes.NONE) {
                return;
            }

            MessageBox.Show(result.LocalizedErrorString);
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e) { var result = await viewModel.RunHashcat(); }

        private void btnSettings_OnClick(object sender, RoutedEventArgs e) {
            settingsControl = new SettingsControl();

            fSettings.IsOpen = true;
        }

        private void btnBrowseForHashfile_OnClick(object sender, RoutedEventArgs e) {
            var fileDialog = new OpenFileDialog {
                DefaultExt = "*.*",
                Multiselect = false
            };

            var result = fileDialog.ShowDialog();

            if (result == false) {
                return;
            }

            viewModel.SelectedHashFile = fileDialog.FileName;
        }

        private void btnAbout_OnClick(object sender, RoutedEventArgs e) { fAbout.IsOpen = true; }

        private void btnStopHashCat_OnClick(object sender, RoutedEventArgs e) {
            viewModel.StopHashcat();
        }
    }
}