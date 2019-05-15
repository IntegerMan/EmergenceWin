using System;
using System.Collections.Generic;
using MattEland.Emergence.Model.Messages;

namespace MattEland.Emergence.Model.Entities
{
    public class Floor : WorldObject, IInteractive
    {
        public FloorType FloorType { get; }

        public Floor(Position pos, FloorType floorType) : base(pos, Guid.NewGuid())
        {
            FloorType = floorType;
        }

        public override char AsciiChar
        {
            get
            {
                switch (FloorType)
                {
                    case FloorType.QuadTile:
                        return ',';
                    case FloorType.Grate:
                        return '_';
                    case FloorType.Caution:
                        return '=';
                    case FloorType.LargeTile:
                    default:
                        return '.';
                }
            }
        }

        public override string ForegroundColor
        {
            get
            {
                switch (FloorType)
                {
                    case FloorType.LargeTile:
                        return GameColors.Gray;
                    case FloorType.Grate:
                        return GameColors.DarkGray;
                    case FloorType.Caution:
                        return GameColors.LightYellow;
                    default:
                        return GameColors.LightGray;
                }
    
            }
        }

        public override int ZIndex => 0;
        public IEnumerable<GameMessage> Interact(Actor actor)
        {
            yield return MoveObject(actor, Pos);
        }
    }
}