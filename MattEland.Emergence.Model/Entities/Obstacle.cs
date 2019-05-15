using System;
using System.Collections.Generic;
using MattEland.Emergence.Model.Messages;

namespace MattEland.Emergence.Model.Entities
{
    public class Obstacle : WorldObject, IInteractive
    {
        public ObstacleType ObstacleType { get; }

        public Obstacle(Position pos, ObstacleType obstacleType) : base(pos, Guid.NewGuid())
        {
            ObstacleType = obstacleType;
        }

        public override string ForegroundColor
        {
            get
            {
                switch (ObstacleType)
                {
                    case ObstacleType.Wall:
                        return GameColors.SlateBlue;
                    case ObstacleType.Service:
                        return GameColors.Orange;
                    case ObstacleType.Data:
                        return GameColors.Purple;
                    case ObstacleType.ThreadPool:
                        return GameColors.Blue;
                    default:
                        return GameColors.DarkGray;
                }                
            }
        }

        public override char AsciiChar
        {
            get
            {
                switch (ObstacleType)
                {
                    case ObstacleType.Wall:
                        return '#';
                    case ObstacleType.Column:
                        return 'O';
                    case ObstacleType.Service:
                        return '*';
                    case ObstacleType.Data:
                        return 'd';
                    case ObstacleType.ThreadPool:
                        return '~';
                    case ObstacleType.Barrier:
                        return 'X';
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public override int ZIndex => 30;
        public IEnumerable<GameMessage> Interact(Actor actor)
        {
            yield return new DisplayTextMessage("You can't go that way.");
        }
    }
}