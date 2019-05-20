using System;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;

namespace MattEland.Emergence.Definitions.Entities
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

        public override void OnInteract(CommandContext context, IActor actor) => context.MoveObject(actor, Pos);
    }
}