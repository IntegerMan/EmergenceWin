using System;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Model.Entities
{
    public class Actor : WorldObject
    {
        public Actor(Pos2D pos, ActorType entityType) : base(pos, Guid.NewGuid())
        {
            ActorType = entityType;
        }

        public override string ForegroundColor => (ActorType == ActorType.Player) ? GameColors.Green : GameColors.Yellow;

        public override char AsciiChar
        {
            get
            {
                switch (ActorType)
                {
                    case ActorType.Bit:
                        return '1';
                    case ActorType.Daemon:
                        return 'D';
                    case ActorType.AntiVirus:
                        return 'V';
                    case ActorType.SystemDefender:
                        return 'S';
                    case ActorType.Inspector:
                        return 'I';
                    case ActorType.SecurityAgent:
                        return 'A';
                    case ActorType.GarbageCollector:
                        return 'G';
                    case ActorType.Helpy:
                        return '?';
                    case ActorType.QueryAgent:
                        return 'Q';
                    case ActorType.KernelWorker:
                        return 'w';
                    case ActorType.LogicBomb:
                        return 'L';
                    case ActorType.Turret:
                        return 'T';
                    case ActorType.Core:
                        return 'C';
                    case ActorType.Player:
                        return '@';
                    case ActorType.Bug:
                        return 'B';
                    case ActorType.Feature:
                        return 'F';
                    case ActorType.Virus:
                        return 'v';
                    case ActorType.Worm:
                        return 'W';
                    case ActorType.Glitch:
                        return 'G';
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public override int ZIndex => 90;
        public ActorType ActorType { get; }
    }
}