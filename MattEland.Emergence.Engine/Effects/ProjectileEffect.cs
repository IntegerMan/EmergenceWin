using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Effects
{
    public class ProjectileEffect : EffectBase
    {
        public Pos2D EndPos { get; }

        public ProjectileEffect(GameObjectBase source, Pos2D endPos) : base(source)
        {
            EndPos = endPos;
        }
    
        public override string ForegroundColor => GameColors.LightYellow;

    }
}