using MattEland.Emergence.Engine.Entities;

namespace MattEland.Emergence.Engine.Effects
{
    public class DestroyedEffect : EffectBase
    {
        public DestroyedEffect(GameObjectBase source) : base(source)
        {
        }

        public override string ToString() => $"Destroyed {Source}";
    }
}