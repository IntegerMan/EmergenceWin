using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Services
{
    public interface ILootProvider
    {
        void SpawnLoot(ICommandContext context, IGameObject source, Rarity rarity);

        void SpawnLootAsNeeded(ICommandContext context, IGameObject source, Rarity rarity);
    }
}