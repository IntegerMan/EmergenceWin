using System.Globalization;
using MattEland.Emergence.Engine.Entities;

namespace MattEland.Emergence.Engine.Effects
{
    public class OpsChangedEffect : EffectBase
    {
        public decimal Amount { get; }

        public OpsChangedEffect(GameObjectBase source, decimal amount) : base(source)
        {
            Amount = amount;
        }
     
    }
}