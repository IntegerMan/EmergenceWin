using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Effects
{
    public class CapturedEffect : EffectBase
    {
        public CapturedEffect(GameObjectBase source) : base(source)
        {
        }
        
        public override string ForegroundColor => GameColors.Green;
    }
}