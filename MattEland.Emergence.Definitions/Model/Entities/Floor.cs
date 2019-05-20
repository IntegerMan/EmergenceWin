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
                    case FloorType.DecorativeTile:
                        return ',';
                    case FloorType.Walkway:
                        return '_';
                    case FloorType.CautionMarker:
                        return '=';
                    case FloorType.Normal:
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
                    case FloorType.Walkway:
                        return GameColors.DarkGray;
                    case FloorType.CautionMarker:
                        return GameColors.LightYellow;
                    case FloorType.Normal:
                    default:
                        return GameColors.LightGray;
                }
    
            }
        }

        public override int ZIndex => 0;
        
        public void Interact(ICommandContext context) => context.MoveExecutingActor(Pos);
    }
}