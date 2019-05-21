using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Services
{
    public interface ILootProvider
    {
        void SpawnLoot(CommandContext context, IGameObject source, Rarity rarity);

        void SpawnLootAsNeeded(CommandContext context, IGameObject source, Rarity rarity);
    }
}