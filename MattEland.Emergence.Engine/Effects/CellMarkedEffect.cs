using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Effects
{
    public class CellMarkedEffect : EffectBase
    {
        public Pos2D Pos { get; }

        public CellMarkedEffect(Pos2D pos) : base(null)
        {
            Pos = pos;
        }
    }
}