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

        public override int ZIndex => 0;
        public IEnumerable<GameMessage> Interact(Actor actor)
        {
            actor.Pos = Pos;
            yield return new ObjectUpdatedMessage(actor);
        }
    }
}