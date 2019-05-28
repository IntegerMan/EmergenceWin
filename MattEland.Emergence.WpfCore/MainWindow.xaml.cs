using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MattEland.Emergence.Engine.Model;
using MattEland.Emergence.WpfCore.ViewModels;

namespace MattEland.Emergence.WpfCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            _vm = new GameViewModel();
            DataContext = _vm;
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (_vm == null) return;

            switch (e.Key)
            {
                case Key.Left:
                case Key.NumPad4:
                    _vm.MovePlayer(MoveDirection.Left);
                    break;

                case Key.Up:
                case Key.NumPad8:
                    _vm.MovePlayer(MoveDirection.Up);
                    break;

                case Key.Right:
                case Key.NumPad6:
                    _vm.MovePlayer(MoveDirection.Right);
                    break;

                case Key.Down:
                case Key.NumPad2:
                    _vm.MovePlayer(MoveDirection.Down);
                    break;

            }
        }

        private void OnCommandClicked(object sender, RoutedEventArgs e)
        {
            Button b = (Button) e.Source;
            CommandViewModel vm = (CommandViewModel) b.DataContext;

            vm.Execute();
        }
    }
}
