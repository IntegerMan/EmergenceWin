using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class Virus : Bug
    {
        public Virus(Pos2D pos) : base(pos)
        {
        }

        public override int Strength => 2;
        public override int Defense => 1;
        public override int Accuracy => 50;
        public override int Evasion => 25;
        protected override void InitializeProtected()
        {
            base.InitializeProtected();

            Team = Alignment.Virus;
            MaxStability = 5;
            MaxOperations = 10;
        }

        public override Rarity LootRarity => Rarity.Common;
        public override bool BlocksSight => false;

        public override string Name => "Virus";
        public override char AsciiChar => 'v';

        public override DamageType AttackDamageType => DamageType.Corruption;

    }
}