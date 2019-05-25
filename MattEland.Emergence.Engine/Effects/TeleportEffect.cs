using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Effects
{
    public class TeleportEffect : EffectBase
    {
        public Pos2D StartPos { get; }
        public Pos2D EndPos { get; }

        public TeleportEffect(Pos2D startPos, Pos2D endPos) : base(null)
        {
            StartPos = startPos;
            EndPos = endPos;
        }
     
    }
}