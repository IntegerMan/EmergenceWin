using System.Linq;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class Worm : VirusActorBase
    {

        public override int Strength => 1;
        public override int Defense => 0;
        public override int Accuracy => 35;
        public override int Evasion => 25;
        
        protected override void InitializeProtected()
        {
            base.InitializeProtected();

            Team = Alignment.Virus;
            MaxStability = 3;
            MaxOperations = 5;
        }

        public override Rarity LootRarity => Rarity.Common;
        public override bool BlocksSight => false;

        public Worm(Pos2D pos) : base(pos)
        {
        }

        public override string Name => "Worm";
        public override char AsciiChar => 'w';

        public override DamageType AttackDamageType => DamageType.Corruption;

        public override void ApplyActiveEffects(GameContext context)
        {
            base.ApplyActiveEffects(context);

            var cell = context.Level.GetCell(Pos);

            if (cell != null)
            {
                cell.Corruption += 1;
                foreach (var cellObject in cell.Objects.Where(o => o.IsCorruptable))
                {
                    cellObject.ApplyCorruptionDamage(context, this, 1);
                }
            }
        }

        public override bool IsCorruptable => false;
    }
}