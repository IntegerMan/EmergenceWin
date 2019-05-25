using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities
{
    public class CommandPickup : GameObjectBase
    {
        public CommandPickup(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInteractive => true;
        public override char AsciiChar => 'p';

        public override void ApplyActiveEffects(CommandContext context)
        {
            base.ApplyActiveEffects(context);

            // If this is an item placed in the game world without an ID specified, kill it and autogen loot
            if (ObjectId == null)
            {
                context.LootProvider.SpawnLoot(context, this, Rarity.Uncommon);
                context.RemoveObject(this);
            }

        }

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(CommandContext context, Actor actor)
        {
            if (actor.IsPlayer)
            {
                context.Player.AttemptPickupItem(context, this);
                context.RemoveObject(this);
            }

            return true;
        }

        public override int ZIndex => 10;
    }
}