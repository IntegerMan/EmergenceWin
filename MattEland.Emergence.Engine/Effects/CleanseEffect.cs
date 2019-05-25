using MattEland.Emergence.Engine.Level;

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
    }
}