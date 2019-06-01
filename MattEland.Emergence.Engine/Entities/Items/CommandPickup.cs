using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities.Items
{
    public class CommandPickup : GameObjectBase
    {
        [CanBeNull] private readonly string _commandId;

        public CommandPickup(Pos2D pos, [CanBeNull] string commandId, [NotNull] string name) : base(pos)
        {
            _commandId = commandId;
            Name = name;
        }

        public override char AsciiChar => 'p';

        public override GameObjectType ObjectType => GameObjectType.CommandPickup;

        public override string Name { get; }

        public override void ApplyActiveEffects(GameContext context)
        {
            base.ApplyActiveEffects(context);

            // If this is an item placed in the game world without an ID specified, kill it and autogen loot
            if (_commandId == null)
            {
                context.LootProvider.SpawnLoot(context, this, Rarity.Uncommon);
                context.RemoveObject(this);
            }

        }

        [CanBeNull]
        public string CommandId => _commandId;

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(GameContext context, Actor actor)
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