using System;
using System.Windows.Media;
using JetBrains.Annotations;
using LanguageExt;
using MattEland.Emergence.Domain;

namespace MattEland.Emergence.WinCore.ViewModels
{
    public class WorldObjectViewModel
    {
        [NotNull]
        private readonly GameViewModel _gameVM;

        [NotNull, UsedImplicitly]
        public WorldObject Source { get; }

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
                            case Floors.FloorType.QuadTile:
                                return Brushes.LightGray;
                            case Floors.FloorType.Grate:
                                return Brushes.DarkGray;
                            case Floors.FloorType.Caution:
                                return Brushes.Yellow;
                            default:
                                throw new NotSupportedException($"The FloorType {floor.FloorType:G} does not have a brush mapping");
                        }
                    
                    case Obstacles.Obstacle obstacle:
                        switch (obstacle.ObstacleType)
                        {
                            case Obstacles.ObstacleType.Wall:
                                return Brushes.DarkSlateGray;
                            case Obstacles.ObstacleType.Column:
                                return Brushes.DimGray;
                            case Obstacles.ObstacleType.Service:
                                return Brushes.Orange;
                            case Obstacles.ObstacleType.Data:
                                return Brushes.Purple;
                            default:
                                throw new NotSupportedException($"The Obstacle {obstacle.ObstacleType:G} does not have a brush mapping");
                        }

                    case Domain.Void _:
                        return Brushes.Black;

                    case Doors.Door _:
                        return Brushes.LightYellow;

                    default:
                        throw new NotSupportedException($"Source {Source.GetType().Name} does not have a brush mapping");
                }
            }
        }
        public string Content
        {
            get
            {
                switch (Source)
                {
                    case Floors.Floor _:
                        return ".";
                    
                    case Obstacles.Obstacle obstacle:
                        switch (obstacle.ObstacleType)
                        {
                            case Obstacles.ObstacleType.Wall:
                                return "#";
                            case Obstacles.ObstacleType.Column:
                                return "o";
                            case Obstacles.ObstacleType.Service:
                                return "*";
                            case Obstacles.ObstacleType.Data:
                                return "d";
                            default:
                                throw new NotSupportedException($"The Obstacle {obstacle.ObstacleType:G} does not have a content mapping");
                        }

                    case Domain.Void _:
                        return string.Empty;
                    
                    case Doors.Door door:
                        return door.IsOpen ? "." : "+";

                    default:
                        throw new NotSupportedException($"Source {Source.GetType().Name} does not have a content mapping");
                }
            }
        }

        [UsedImplicitly] 
        public int X => (Source.Position.X * Size) + _gameVM.XOffset;
        
        [UsedImplicitly] 
        public int Y => (Source.Position.Y * Size) + _gameVM.YOffset;

        public WorldObjectViewModel(Some<WorldObject> source, Some<GameViewModel> gameViewModel)
        {
            _gameVM = gameViewModel;
            Source = source.Value;
        }
    }
}