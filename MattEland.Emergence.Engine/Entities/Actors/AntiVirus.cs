using System.Linq;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class AntiVirus : Actor
    {
        public AntiVirus(Pos2D pos) : base(pos)
        {
        }

        public override string Name => "Anti-Virus";

        public override char AsciiChar => 'V';

        public override void ApplyActiveEffects(GameContext context)
        {
            base.ApplyActiveEffects(context);

            var scrubDelta = IsCorrupted ? -1 : 3; // Corrupt AV agents should make it more corrupt

            CorruptionHelper.CleanseNearby(context, this, Pos, scrubDelta);
        }
    }
}