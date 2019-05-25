using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities
{
    public class TreasureTrove : OpenableGameObjectBase
    {
        public TreasureTrove(OpenableDto dto) : base(dto)
        {

        }

        /// <inheritdoc />
        protected override void OnOpened(CommandContext context, GameObjectBase opener)
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

        public override void OnDestroyed(CommandContext context, GameObjectBase attacker)
        {
            base.OnDestroyed(context, attacker);

            if (!IsOpen)
            {
                OnOpened(context, attacker);
            }
        }

        private void SpawnNastySurprise(CommandContext context)
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
        
        public override string Name => IsOpen ? "Empty Cache" : "Cache";

        public override int ZIndex => 4;
    }
}