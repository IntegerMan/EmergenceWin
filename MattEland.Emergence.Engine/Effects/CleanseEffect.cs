using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Effects
{
    public class CleanseEffect : EffectBase
    {
        public decimal Amount { get; }
        public Pos2D Pos { get; }

        public CleanseEffect(Pos2D position, int amount) : base(null)
        {
            Amount = amount;
            Pos = position;
        }
        
        public override string ForegroundColor => GameColors.SlateBlue;
    }
}