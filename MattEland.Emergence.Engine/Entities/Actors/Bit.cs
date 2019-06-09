using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class Bit : Actor
    {
        public Bit(Pos2D pos) : base(pos)
        {
        }

        public override string Name => "Bit";
        public override char AsciiChar => '0';

        public override int Strength => 0;
        public override int Defense => 0;
        public override int Accuracy => 20;
        public override int Evasion => 0;
        public override decimal LineOfSightRadius => 3;
        protected override void InitializeProtected()
        {
            base.InitializeProtected();

            Team = Alignment.SystemCore;
            MaxStability = 1;
            MaxOperations = 1;
        }

        public override Rarity LootRarity => Rarity.None;
        public override bool BlocksSight => false;
    }
}