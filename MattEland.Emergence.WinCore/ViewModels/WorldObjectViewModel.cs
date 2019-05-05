using System;
using System.Windows.Media;
using JetBrains.Annotations;
using LanguageExt;
using MattEland.Emergence.Domain;

namespace MattEland.Emergence.WinCore.ViewModels
{
    public class WorldObjectViewModel
    {
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
                            default:
                                throw new NotSupportedException($"The Obstacle {obstacle.ObstacleType:G} does not have a brush mapping");
                        }

                    case Domain.Void empty:
                        return Brushes.Black;

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
                    case Floors.Floor floor:
                        return ".";
                    
                    case Obstacles.Obstacle obstacle:
                        switch (obstacle.ObstacleType)
                        {
                            case Obstacles.ObstacleType.Wall:
                                return "#";
                            case Obstacles.ObstacleType.Column:
                                return "o";
                            default:
                                throw new NotSupportedException($"The Obstacle {obstacle.ObstacleType:G} does not have a content mapping");
                        }

                    case Domain.Void empty:
                        return string.Empty;

                    default:
                        throw new NotSupportedException($"Source {Source.GetType().Name} does not have a content mapping");
                }
            }
        }

        [UsedImplicitly] 
        public int X => Source.Position.X * Size; // TODO:  This won't work for view offsets
        
        [UsedImplicitly] 
        public int Y => Source.Position.Y * Size; // TODO:  This won't work for view offsets

        public WorldObjectViewModel(Some<WorldObject> source)
        {
            Source = source.Value;
        }
    }
}