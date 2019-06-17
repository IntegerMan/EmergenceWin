using System.Reflection;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Model;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Effects
{
    public class DamagedEffect : EffectBase
    {
        public int Amount { get; }
        public DamageType DamageType { get; }

        public DamagedEffect(GameObjectBase source, int amount, DamageType damageType) : base(source)
        {
            Amount = amount;
            DamageType = damageType;
            Target = source;
        }

        [NotNull]
        public GameObjectBase Target { get; }

        public override string ToString() => $"Damaged {Target.Name}: {Amount} {DamageType:G}";
        
        public override string ForegroundColor => GameColors.Red;
    }
}