using System.Linq;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities
{
    public class AntiVirus : Actor
    {
        public AntiVirus(ActorDto dto) : base(dto)
        {
        }

        public override void ApplyActiveEffects(CommandContext context)
        {
            base.ApplyActiveEffects(context);

            var scrubDelta = IsCorrupted ? 1 : -3; // Corrupt AV agents should make it more corrupt

            var neighbors = context.Level.GetCellsInSquare(Pos, 1).ToList();
            foreach (var cell in neighbors)
            {
                if (scrubDelta < 0 && cell.Corruption > 0 && context.CanPlayerSee(cell.Pos))
                {
                    context.AddEffect(new CleanseEffect(cell.Pos, -scrubDelta));
                }

                cell.Corruption += scrubDelta;

                foreach (var obj in cell.Objects.Where(o => o.IsCorruptable && o != this))
                {
                    obj.ApplyCorruptionDamage(context, this, scrubDelta);
                }
            }
        }
    }
}