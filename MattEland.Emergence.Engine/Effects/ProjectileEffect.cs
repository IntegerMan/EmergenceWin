using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Effects
{
    public class ProjectileEffect : EffectBase
    {
        public Pos2D EndPos { get; }

        public ProjectileEffect(GameObjectBase source, Pos2D endPos) : base(source)
        {
            EndPos = endPos;
        }
    
    }
}