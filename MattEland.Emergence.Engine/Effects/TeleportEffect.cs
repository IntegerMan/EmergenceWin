using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

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

        public override string ToString() => $"Teleport from {StartPos} to {EndPos}";
        
        public override string ForegroundColor => GameColors.LightYellow;

    }
}