using System.Windows;

using Microsoft.Win32;

namespace MillionGameEditor
{
    using System;

    public sealed partial class EditorWindow
    {
        public EditorWindow()
        {
            InitializeComponent();
        }

        private void OnLoadGameClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
                                     {
                                         Filter = "EMG-Dateien (*.emg)|*.emg|Alle Dateien (*.*)|*.*", 
                                         CheckFileExists = true, 
                                         Multiselect = false
                                     };

            if (dlg.ShowDialog() == true)
            {
                this.DataContext = new Game(dlg.FileName);
                this.LevelList.SelectedIndex = 0;
            }
        }

        private void OnNewGameClick(object sender, RoutedEventArgs e)
        {
            this.DataContext = new Game();
            this.LevelList.SelectedIndex = 0;
        }

        private void OnCloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnSaveGameClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = "Easy-Million-Game-Dateien (*.emg)|*.emg|Alle Dateien (*.*)|*.*",
                OverwritePrompt = true
            };

            if (dlg.ShowDialog() == true)
            {
                ((Game) this.DataContext).Save(dlg.FileName);
            }
        }

        private void OnInfoClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                string.Format(
                    "Easy Million Game (Editor){0}{0}" +
                    "© 2009, Ulrich Kaiser und Andreas Helmberger{0}" +
                    "https://github.com/musikisum/EasyMillionGame",
                    Environment.NewLine),
                "Hinweis",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}
