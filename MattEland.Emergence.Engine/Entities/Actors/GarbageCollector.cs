using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class GarbageCollector : Actor
    {
        public GarbageCollector(Pos2D pos) : base(pos)
        {
        }

        public override string Name => "Garbage Collector";
        public override char AsciiChar => 'G';

        public override int Strength => 5;
        public override int Defense => 1;
        public override int Accuracy => 90;
        public override int Evasion => 0;
        public override decimal LineOfSightRadius => 7;
        public override Rarity LootRarity => Rarity.Epic;

        protected override void InitializeProtected()
        {
            base.InitializeProtected();

            Team = Alignment.SystemSecurity;
            MaxStability = 10;
            MaxOperations = 10;
        }
    }
}