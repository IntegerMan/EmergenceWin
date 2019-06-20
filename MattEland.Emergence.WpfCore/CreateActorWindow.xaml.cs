using System;
using System.Windows;
using JetBrains.Annotations;
using MahApps.Metro.Converters;
using MattEland.Emergence.Engine.Actions;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.WpfCore.ViewModels;

namespace MattEland.Emergence.WpfCore
{
    public partial class CreateActorWindow : Window
    {
        [NotNull] 
        private readonly CreateObjectViewModel _vm;

        public CreateActorWindow([NotNull] CreateObjectViewModel vm)
        {
            _vm = vm;
            
            InitializeComponent();

            DataContext = vm;
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnCreateClick(object sender, RoutedEventArgs e)
        {
            var actorType = (ActorType) Enum.Parse(typeof(ActorType), _vm.SelectedActorType);
            _vm.Game.HandleAction(new CreateActorAction(actorType, _vm.WorldObject.Pos));
            
            this.Close();
        }
    }
}