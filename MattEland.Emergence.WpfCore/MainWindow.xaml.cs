using System.Windows;
using System.Windows.Input;
using MattEland.Emergence.Engine.Model;
using MattEland.Emergence.WpfCore.ViewModels;

namespace MattEland.Emergence.WpfCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
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

                case Key.Space:
                    _vm.Wait();
                    break;
                
                case Key.D1:
                    RunCommand(0);
                    break;
                
                case Key.D2:
                    RunCommand(1);
                    break;
                
                case Key.D3:
                    RunCommand(2);
                    break;
                
                case Key.D4:
                    RunCommand(3);
                    break;
                
                case Key.D5:
                    RunCommand(4);
                    break;
                
                case Key.D6:
                    RunCommand(5);
                    break;
                
                case Key.D7:
                    RunCommand(6);
                    break;
                    
                case Key.D8:
                    RunCommand(7);
                    break;
            }
        }

        private void RunCommand(int index)
        {
            if (_vm.Commands.Count < index) return;
            
            var vm = _vm.Commands[index];
            vm?.Execute();
        }

        private static T GetElementDataContext<T>(RoutedEventArgs e) => (T) ((FrameworkElement) e.Source).DataContext;

        private void OnCommandClicked(object sender, RoutedEventArgs e) 
            => GetElementDataContext<CommandViewModel>(e).Execute();

        private void OnTileClicked(object sender, MouseButtonEventArgs e) 
            => _vm.HandleTargetedCommandInput(GetElementDataContext<WorldObjectViewModel>(e).Source.Pos);
    }
}
