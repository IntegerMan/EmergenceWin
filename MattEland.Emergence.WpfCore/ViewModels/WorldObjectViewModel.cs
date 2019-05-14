using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using JetBrains.Annotations;
using LanguageExt;
using MattEland.Emergence.Model.Entities;
using MattEland.Emergence.Model.Messages;

namespace MattEland.Emergence.WinCore.ViewModels
{
    [DebuggerDisplay("{Source.GetType().Name} at ({Source.Pos.X}, {Source.Pos.Y}) rendering at ({X}, {Y})")]
    public class WorldObjectViewModel : INotifyPropertyChanged
    {
        [NotNull]
        private readonly GameViewModel _gameVM;

        [NotNull, UsedImplicitly]
        public WorldObject Source { get; private set; }

        public int Size => 24;

        public Brush Brush
        {
            get
            {
                switch (Source)
                {
                    case Floor floor:
                        switch (floor.FloorType)
                        {
                            case FloorType.LargeTile:
                                return Brushes.Gray;
                            case FloorType.Grate:
                                return Brushes.DarkGray;
                            case FloorType.Caution:
                                return Brushes.LightYellow;
                            default:
                                return Brushes.LightGray;
                        }

                    case StairsUp _:
                    case StairsDown _:
                        return Brushes.White;

                    case Obstacle obstacle:
                        switch (obstacle.ObstacleType)
                        {
                            case ObstacleType.Wall:
                                return Brushes.DarkSlateGray;
                            case ObstacleType.Service:
                                return Brushes.Orange;
                            case ObstacleType.Data:
                                return Brushes.Purple;
                            case ObstacleType.ThreadPool:
                                return Brushes.CornflowerBlue;
                            default:
                                return Brushes.DimGray;
                        }

                    case Core core:
                        return core.IsCaptured ? Brushes.LimeGreen : Brushes.Yellow;

                    case CharacterSelect _:
                        return Brushes.MediumAquamarine;

                    case HelpTile _:
                        return Brushes.DodgerBlue;

                    case Door _:
                        return Brushes.LightYellow;

                    case Actor _:
                        return Brushes.LimeGreen;

                    case Firewall firewall:
                        return firewall.IsOpen ? Brushes.Yellow : Brushes.OrangeRed;

                    default:
                        throw new NotSupportedException($"Source {Source.GetType().Name} does not have a brush mapping");
                }
            }
        }

        public string Content => Source.AsciiChar.ToString();

        [UsedImplicitly] 
        public int X => (Source.Pos.X + _gameVM.XOffset) * Size;
        
        [UsedImplicitly] 
        public int Y => (Source.Pos.Y + _gameVM.YOffset) * Size;

        [UsedImplicitly]
        public int ZIndex => Source.ZIndex;

        public Guid Id => Source.Id;

        public WorldObjectViewModel(Some<WorldObject> source, Some<GameViewModel> gameViewModel)
        {
            _gameVM = gameViewModel;
            Source = source.Value;
        }

        public void UpdateFrom([NotNull] ObjectUpdatedMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            Source = message.Source;

            // Notify all properties changed
            OnPropertyChanged(string.Empty);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}