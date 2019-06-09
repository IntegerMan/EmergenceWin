using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public sealed class Bug : VirusActorBase
    {
        public Bug(Pos2D pos) : base(pos)
        {
        }

        public override int Strength => 1;
        public override int Defense => 1;
        public override int Accuracy => 45;
        public override int Evasion => 25;
        
        protected override void InitializeProtected()
        {
            base.InitializeProtected();

            Team = Alignment.Bug;
            MaxStability = 3;
            MaxOperations = 5;
        }

        public override Rarity LootRarity => Rarity.Common;

        public override bool BlocksSight => false;

        public override string Name => "Bug";
        public override char AsciiChar => 'b';
        
    }
}