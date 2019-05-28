using MattEland.Emergence.Engine.Entities;
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
        }

        public override string ToString() => $"Damaged: {Amount} {DamageType:G}";
    }
}