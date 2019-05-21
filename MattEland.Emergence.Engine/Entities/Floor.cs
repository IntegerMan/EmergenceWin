using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities
{
    public class Floor : GameObjectBase
    {
        public Floor(GameObjectDto dto, FloorType floorType) : base(dto)
        {
            FloorType = floorType;
        }

        public FloorType FloorType { get; }

        public override char AsciiChar
        {
            get
            {
                switch (FloorType)
                {
                    case FloorType.DecorativeTile:
                        return '\'';
                    case FloorType.Walkway:
                        return '_';
                    case FloorType.CautionMarker:
                        return '=';
                    default:
                        return '.';
                }
            }
        }

        public override string ForegroundColor {
            get
            {
                switch (FloorType)
                {
                    case FloorType.DecorativeTile: return GameColors.LightGray;
                    case FloorType.CautionMarker: return GameColors.LightYellow;
                    case FloorType.Walkway: return GameColors.White;
                    default: return GameColors.Gray;
                }
            }
        }

        public override bool OnActorAttemptedEnter(CommandContext context, IActor actor)
        {
            context.MoveObject(actor, Pos);

            return true;
        }
    }
}