using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class QueryAgent : Actor
    {
        public QueryAgent(Pos2D pos) : base(pos)
        {
        }

        protected override void InitializeProtected()
        {
            base.InitializeProtected();

            Team = Alignment.SystemCore;
            MaxStability = 2;
            MaxOperations = 10;
        }

        public override int Strength => 0;
        public override int Defense => 0;
        public override int Accuracy => 20;
        public override int Evasion => 25;
        public override decimal LineOfSightRadius => 7M;

        public override bool BlocksSight => false;

        public override Rarity LootRarity => Rarity.Common;

        public override string Name => "Query Agent";
        public override char AsciiChar => 'q';
    }
}