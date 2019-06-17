using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Effects
{
    public class CellMarkedEffect : EffectBase
    {
        public Pos2D Pos { get; }

        public CellMarkedEffect(Pos2D pos) : base(null)
        {
            Pos = pos;
        }
        
        public override string ForegroundColor => GameColors.SlateBlue;
    }
}