using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class Feature : VirusActorBase
    {
        public Feature(Pos2D pos) : base(pos)
        {
            Team = Alignment.Bug;
        }

        public override string Name => "Feature";
        public override char AsciiChar => 'f';

        public override int Strength => 2;
        public override int Defense => 1;
        public override int Accuracy => 60;
        public override int Evasion => 35;
        protected override void InitializeProtected()
        {
            base.InitializeProtected();

            Team = Alignment.Virus;
            MaxStability = 5;
            MaxOperations = 10;
        }

        public override Rarity LootRarity => Rarity.Uncommon;
        public override bool BlocksSight => false;

    }
}