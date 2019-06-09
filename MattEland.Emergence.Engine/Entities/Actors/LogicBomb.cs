using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class LogicBomb : VirusActorBase
    {
        public override string Name => "Logic Bomb";
        public override char AsciiChar => 'l';

        public LogicBomb(Pos2D pos) : base(pos)
        {
        }

        public override int Strength => 3;
        public override int Defense => 0;
        public override int Accuracy => 100;
        public override int Evasion => 20;

        public override bool BlocksSight => false;

        protected override void InitializeProtected()
        {
            base.InitializeProtected();

            Team = Alignment.Virus;
            MaxStability = 3;
            MaxOperations = 3;
        }

        public override Rarity LootRarity => Rarity.Uncommon;
    }
}