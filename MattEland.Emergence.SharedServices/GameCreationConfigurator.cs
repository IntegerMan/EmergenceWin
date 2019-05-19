using MattEland.Emergence.Commands;
using MattEland.Emergence.Definitions.Services;
using MattEland.Emergence.Services.Game;
using MattEland.Emergence.Services.Levels;

namespace MattEland.Emergence.Services
{
    public static class GameCreationConfigurator
    {
        public static void ConfigureObjectCreation()
        {
            CreationService.LevelCreationFunction = (id, name, pos) => new LevelData
            {
                Id = id,
                Name = name,
                PlayerStart = pos
            };

            CreationService.ObjectCreationFunction = GameObjectFactory.CreateFromObjectType;

            CreationService.WallCreationFunction = GameObjectFactory.CreateWall;

            CreationService.PlayerCreationFunction = GameObjectFactory.CreatePlayer;

            CreationService.CommandCreationFunction = CommandFactory.CreateCommand;

            CreationService.CommandReferenceCreationFunction = CommandFactory.CreateCommandReference;
        }
    }
}