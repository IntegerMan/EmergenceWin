using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities.Obstacles
{
    public class TreasureTrove : OpenableGameObjectBase
    {
        public TreasureTrove(Pos2D pos) : base(pos, false)
        {

        }

        /// <inheritdoc />
        protected override void Open(GameContext context, GameObjectBase opener)
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

        public override void OnDestroyed(GameContext context, GameObjectBase attacker)
        {
            base.OnDestroyed(context, attacker);

            if (!IsOpen)
            {
                Open(context, attacker);
            }
        }

        private void SpawnNastySurprise(GameContext context)
        {
            switch (context.Randomizer.GetInt(0, 2))
            {
                case 0:
                    context.AddObject(GameObjectFactory.CreateObject(DTOs.Actors.LogicBomb, GameObjectType.Actor, Pos));
                    break;
                case 1:
                    context.AddObject(GameObjectFactory.CreateObject(DTOs.Actors.Bug, GameObjectType.Actor, Pos));
                    break;
                case 2:
                    context.AddObject(GameObjectFactory.CreateObject(DTOs.Actors.Feature, GameObjectType.Actor, Pos));
                    break;
            }
        }

        public override GameObjectType ObjectType => GameObjectType.Treasure;

        public override string Name => IsOpen ? "Empty Cache" : "Cache";

        public override int ZIndex => 4;
    }
}