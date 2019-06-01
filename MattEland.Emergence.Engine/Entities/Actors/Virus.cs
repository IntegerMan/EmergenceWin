using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class Virus : Bug
    {
        public Virus(Pos2D pos) : base(pos)
        {
        }

        public override string Name => "Virus";
        public override char AsciiChar => 'v';

        public override DamageType AttackDamageType => DamageType.Corruption;

    }
}