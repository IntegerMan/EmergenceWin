using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class KernelWorker : Actor
    {
        public KernelWorker(Pos2D pos) : base(pos)
        {
        }

        public override int Strength => 0;
        public override int Defense => 0;
        public override int Accuracy => 20;
        public override int Evasion => 15;
        public override decimal LineOfSightRadius => 7;

        public override bool BlocksSight => false;

        public override Rarity LootRarity => Rarity.Common;

        protected override void InitializeProtected()
        {
            base.InitializeProtected();
            
            Team = Alignment.SystemCore;
            MaxOperations = 1;
            MaxStability = 1;
        }

        public override string Name => "Kernel Worker";
        public override char AsciiChar => 'k';
    }
}