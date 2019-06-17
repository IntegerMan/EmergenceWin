using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Effects
{
    public class NoDamageEffect : EffectBase
    {
        public NoDamageEffect(GameObjectBase source) : base(source)
        {
        }

        public override string ForegroundColor => GameColors.Gray;
    }
}