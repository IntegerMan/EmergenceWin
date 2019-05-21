using MattEland.Emergence.Engine.Commands;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine
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