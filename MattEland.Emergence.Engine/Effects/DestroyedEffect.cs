using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Effects
{
    public class DestroyedEffect : EffectBase
    {
        public DestroyedEffect(GameObjectBase source) : base(source)
        {
        }

        public override string ToString() => $"Destroyed {Source}";

        public override string ForegroundColor => GameColors.Red;
    }
}