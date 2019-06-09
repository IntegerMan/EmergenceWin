using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class Turret : Actor
    {
        public Turret(Pos2D pos) : base(pos)
        {
        }

        public override int Strength => 2;
        public override int Defense => 2;
        public override int Accuracy => 42;
        public override int Evasion => 0;
        public override decimal LineOfSightRadius => 7M;

        public override Rarity LootRarity => Rarity.Rare;

        protected override void InitializeProtected()
        {
            base.InitializeProtected();

            MaxStability = 3;
            MaxOperations = 30;
            Team = Alignment.SystemSecurity;
        }

        public override bool IsImmobile => true;

        public override string Name => "Turret";
        public override char AsciiChar => 'T';
    }
}