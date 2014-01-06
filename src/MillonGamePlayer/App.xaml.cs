using System.Windows;
using System.Windows.Threading;

namespace MillionGamePlayer
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App
    {
        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private static void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Es ist ein Fehler aufgetreten. Das Programm muss leider geschlossen werden.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
