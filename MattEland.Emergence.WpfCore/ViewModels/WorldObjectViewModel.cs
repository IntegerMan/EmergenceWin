using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using JetBrains.Annotations;
using LanguageExt;
using MattEland.Emergence.Domain;
using MattEland.Emergence.GameLoop;

namespace MattEland.Emergence.WinCore.ViewModels
{
    [DebuggerDisplay("{Source.GetType().Name} at ({Source.Position.X}, {Source.Position.Y}) rendering at ({X}, {Y})")]
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
                    case Floors.Floor floor:
                        switch (floor.FloorType)
                        {
                            case Floors.FloorType.LargeTile:
                                return Brushes.Gray;
                            case Floors.FloorType.Grate:
                                return Brushes.DarkGray;
                            case Floors.FloorType.Caution:
                                return Brushes.LightYellow;
                            default:
                                return Brushes.LightGray;
                        }

                    case LogicObject lob:
                        switch (lob.ObjectType)
                        {
                            case LogicObjectType.Core:
                                return Brushes.Aqua;
                            case LogicObjectType.CharacterSelect:
                                return Brushes.MediumAquamarine;
                            case LogicObjectType.StairsUp:
                            case LogicObjectType.StairsDown:
                                return Brushes.White;
                            case LogicObjectType.Help:
                                return Brushes.DodgerBlue;
                            default:
                                return Brushes.Teal;
                        }

                    case Obstacles.Obstacle obstacle:
                        switch (obstacle.ObstacleType)
                        {
                            case Obstacles.ObstacleType.Wall:
                                return Brushes.DarkSlateGray;
                            case Obstacles.ObstacleType.Service:
                                return Brushes.Orange;
                            case Obstacles.ObstacleType.Data:
                                return Brushes.Purple;
                            case Obstacles.ObstacleType.ThreadPool:
                                return Brushes.CornflowerBlue;
                            default:
                                return Brushes.DimGray;
                        }

                    case Doors.Door _:
                        return Brushes.LightYellow;

                    case Actors.Actor _:
                        return Brushes.LimeGreen;

                    case Firewall firewall:
                        return firewall.IsOpen ? Brushes.YellowGreen : Brushes.OrangeRed;

                    default:
                        throw new NotSupportedException($"Source {Source.GetType().Name} does not have a brush mapping");
                }
            }
        }

        public string Content => Source.AsciiCharacter.ToString();

        [UsedImplicitly] 
        public int X => (Source.Position.X + _gameVM.XOffset) * Size;
        
        [UsedImplicitly] 
        public int Y => (Source.Position.Y + _gameVM.YOffset) * Size;

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

            Source = message.Object;

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