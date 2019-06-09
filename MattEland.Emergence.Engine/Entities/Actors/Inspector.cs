using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class Inspector : Actor
    {
        public Inspector(Pos2D pos) : base(pos)
        {
        }

        public override string Name => "Inspector";
        public override char AsciiChar => 'i';
        
        public override int Strength => 1;
        public override int Defense => 0;
        public override int Accuracy => 95;
        public override int Evasion => 15;
        protected override void InitializeProtected()
        {
            base.InitializeProtected();

            Team = Alignment.SystemCore;
            MaxStability = 3;
            MaxOperations = 5;
        }

        public override Rarity LootRarity => Rarity.Common;
        public override bool BlocksSight => false;

    }
}