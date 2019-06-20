using System;
using System.Windows;
using System.Windows.Input;
using MattEland.Emergence.Engine.Actions;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;
using MattEland.Emergence.WpfCore.ViewModels;

namespace MattEland.Emergence.WpfCore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private GameViewModel _gameVM;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            _gameVM = new GameViewModel();
            DataContext = _gameVM;
        }

        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (_gameVM == null) return;

            switch (e.Key)
            {
                case Key.Left:
                case Key.NumPad4:
                    _gameVM.MovePlayer(MoveDirection.Left);
                    break;

                case Key.Up:
                case Key.NumPad8:
                    _gameVM.MovePlayer(MoveDirection.Up);
                    break;

                case Key.Right:
                case Key.NumPad6:
                    _gameVM.MovePlayer(MoveDirection.Right);
                    break;

                case Key.Down:
                case Key.NumPad2:
                    _gameVM.MovePlayer(MoveDirection.Down);
                    break;

                case Key.Space:
                    _gameVM.Wait();
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
            if (_gameVM.Commands.Count < index || _gameVM.UIState != UIState.ReadyForInput) return;
            
            var vm = _gameVM.Commands[index];
            vm?.Execute();
        }

        private static T GetElementDataContext<T>(RoutedEventArgs e) => (T) ((FrameworkElement) e.Source).DataContext;

        private void OnCommandClicked(object sender, RoutedEventArgs e) 
            => GetElementDataContext<CommandViewModel>(e).Execute();

        private void OnTileClicked(object sender, MouseButtonEventArgs e) 
            => _gameVM.HandleTargetedCommandInput(GetElementDataContext<WorldObjectViewModel>(e).Source.Pos);

        private void OnCreateActorClick(object sender, RoutedEventArgs e)
        {
            var objectVm = GetDataContextFromFrameworkElement<WorldObjectViewModel>(sender);
            
            var createVm = new CreateObjectViewModel(objectVm, _gameVM);

            var window = new CreateActorWindow(createVm);
            window.ShowDialog();
        }

        private void OnDestroyClick(object sender, RoutedEventArgs e)
        {
            var vm = GetDataContextFromFrameworkElement<WorldObjectViewModel>(sender);
            
            _gameVM.HandleAction(new DeleteObjectAction(vm.Source));
        }

        private static T GetDataContextFromFrameworkElement<T>(object sender) where T : class
        {
            var fe = sender as FrameworkElement;
            var dc = fe?.DataContext as T;

            return dc;
        }
    }
}
