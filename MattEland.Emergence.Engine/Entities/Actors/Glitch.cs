using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public sealed class Glitch : VirusActorBase
    {
        public Glitch(Pos2D pos) : base(pos)
        {
        }

        public override int Strength => 2;
        public override int Defense => 0;
        public override int Accuracy => 55;
        public override int Evasion => 45;
        protected override void InitializeProtected()
        {
            base.InitializeProtected();

            Team = Alignment.Bug;
            MaxStability = 5;
            MaxOperations = 10;
        }

        public override Rarity LootRarity => Rarity.None; // Should not be farmable
        public override bool BlocksSight => false;

        public override string Name => "Glitch";
        public override char AsciiChar => 'g';
    }
}