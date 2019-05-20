using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Definitions.Entities
{
    public class TreasureTrove : OpenableGameObjectBase
    {
        public TreasureTrove(OpenableDto dto) : base(dto)
        {

        }

        /// <inheritdoc />
        protected override void OnOpened(ICommandContext context, IGameObject opener)
        {
            if (IsCorrupted)
            {
                // If the treasure chest is corrupt, spawn something nasty nearby
                SpawnNastySurprise(context);
            }
            else
            {
                context.LootProvider.SpawnLoot(context, this, Rarity.Rare);
            }
        }

        public override char AsciiChar => 't';

        public override void OnDestroyed(ICommandContext context, IGameObject attacker)
        {
            base.OnDestroyed(context, attacker);

            if (!IsOpen)
            {
                OnOpened(context, attacker);
            }
        }

        private void SpawnNastySurprise(ICommandContext context)
        {
            switch (context.Randomizer.GetInt(0, 2))
            {
                case 0:
                    context.Level.AddObject(CreationService.CreateObject("ACTOR_LOGIC_BOMB", GameObjectType.Actor, Pos));
                    break;
                case 1:
                    context.Level.AddObject(CreationService.CreateObject("ACTOR_BUG", GameObjectType.Actor, Pos));
                    break;
                case 2:
                    context.Level.AddObject(CreationService.CreateObject("ACTOR_FEATURE", GameObjectType.Actor, Pos));
                    break;
            }
        }

        protected override string CustomName => IsOpen ? "Empty Cache" : "Cache";

        public override int ZIndex => 4;
    }
}