using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class SecurityAgent : Actor
    {
        public SecurityAgent(Pos2D pos) : base(pos)
        {
        }

        public override string Name => "Security Agent";
        public override char AsciiChar => 's';

        public override int Strength => 1;
        public override int Defense => 0;
        public override int Accuracy => 80;
        public override int Evasion => 15;
        public override Rarity LootRarity => Rarity.Common;
        public override bool BlocksSight => false;

        protected override void InitializeProtected()
        {
            base.InitializeProtected();

            Team = Alignment.SystemSecurity;
            MaxStability = 3;
            MaxOperations = 3;
        }


    }
}