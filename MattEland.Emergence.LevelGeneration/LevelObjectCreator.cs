using MattEland.Emergence.Model;
using MattEland.Emergence.Model.Entities;

namespace MattEland.Emergence.LevelGeneration
{
    public static class LevelObjectCreator
    {

        public static WorldObject GetObject(char mapChar, Position pos)
        {
            switch (mapChar)
            {
                // Special Tiles
                case '+': return new Door(pos);
                case '|': return new Firewall(pos);
                case '<': return new Stairs(pos, false);
                case '>': return new Stairs(pos, true);
                case 'C': return new Core(pos);
                case '?': return new HelpTile(pos, "Hello World");

                // Character Select
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                    return new CharacterSelect(pos);

                // Obstacles
                case '#': return new Obstacle(pos, ObstacleType.Wall);
                case 'd': return new Obstacle(pos, ObstacleType.Data);
                case '*': return new Obstacle(pos, ObstacleType.Service);
                case 'X': return new Obstacle(pos, ObstacleType.Barrier);
                case '~': return new Obstacle(pos, ObstacleType.ThreadPool);

                // Floor
                case ',': return new Floor(pos, FloorType.LargeTile);
                case '=': return new Floor(pos, FloorType.Grate);
                case '.': return new Floor(pos, FloorType.LargeTile);
                case '_': return new Floor(pos, FloorType.Caution);
                case 't': return new Floor(pos, FloorType.QuadTile);
                case '$': return new Floor(pos, FloorType.QuadTile);


                default:
                    return new Placeholder(pos, mapChar);
            }
        }

    }
}