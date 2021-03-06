﻿using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities.Obstacles
{
    public class Floor : WalkableObject
    {
        public Floor(Pos2D pos, FloorType floorType) : base(pos)
        {
            FloorType = floorType;
        }

        public override GameObjectType ObjectType => GameObjectType.Floor;
        public override bool IsInvulnerable => true;

        public override string Name => "Floor";

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

    }
}