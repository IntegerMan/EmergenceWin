using System.Windows;
using System.Windows.Threading;

namespace MattEland.Emergence.WpfCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            DispatcherUnhandledException += OnUnhandledException;
        }

        private void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            MessageBox.Show(e.Exception.Message, "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
