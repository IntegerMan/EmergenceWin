using System;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;

namespace MattEland.Emergence.Definitions.Model.Entities
{
    public class Floor : WorldObject, IInteractive
    {
        public FloorType FloorType { get; }

        public Floor(Pos2D pos, FloorType floorType) : base(pos, Guid.NewGuid())
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
        
        public void Interact(ICommandContext context) => context.MoveExecutingActor(Pos);
    }
}