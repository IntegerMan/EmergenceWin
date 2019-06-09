using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class Helpy : Actor
    {
        public Helpy(Pos2D pos) : base(pos)
        {
        }

        protected override void InitializeProtected()
        {
            base.InitializeProtected();

            MaxStability = 2;
            MaxOperations = 10;
            Team = Alignment.SystemCore;
        }

        public override int Strength => 1;
        public override int Defense => 1;
        public override int Accuracy => 20;
        public override int Evasion => 50;
        public override Rarity LootRarity => Rarity.Common;
        public override bool BlocksSight => false;

        public override string Name => "Helpy";
        public override char AsciiChar => '?';
    }
}