using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Definitions.Entities
{
    public class CommandPickup : GameObjectBase
    {
        public CommandPickup(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInteractive => true;
        public override void ApplyActiveEffects(ICommandContext context)
        {
            base.ApplyActiveEffects(context);

            // If this is an item placed in the game world without an ID specified, kill it and autogen loot
            if (ObjectId == null)
            {
                context.LootProvider.SpawnLoot(context, this, Rarity.Uncommon);
                context.Level.RemoveObject(this);
            }

        }

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor, IGameCell cell)
        {
            if (actor.IsPlayer)
            {
                context.Player.AttemptPickupItem(context, this);
                context.Level.RemoveObject(this);
            }

            return true;
        }

        public override int ZIndex => 10;
    }
}