using System.Linq;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Commands
{
    public class MoveCommand : GameCommand
    {
        public override string Id => "MOVE";
        public override string Name => "Move Command";
        public override string Description => "Moves in a given direction";
        public override int ActivationCost => 0;
        public override string IconId => string.Empty;
        public override Rarity Rarity => Rarity.None;

        public override bool IsSilent => true;

        public override CommandActivationType ActivationType => CommandActivationType.Simple;
        public override void ApplyEffect(CommandContext context, Actor executor, Pos2D pos)
        {
            base.ApplyEffect(context, executor, pos);

            // Interact with all objects in the tile
            foreach (var obj in context.Level.Objects.Where(o => o.Pos == pos).OrderByDescending(o => o.ZIndex))
            {
                if (obj.OnActorAttemptedEnter(context, executor))
                {
                    break;
                }
            }

        }
    }
}