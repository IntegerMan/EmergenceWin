using System.Collections.Generic;
using MattEland.Emergence.Model;
using MattEland.Emergence.Model.Entities;

namespace MattEland.Emergence.LevelGeneration
{
    public class WorldGenerationResult
    {
        private readonly LevelData _levelData;
        public IEnumerable<WorldObject> Objects { get; }
        
        public WorldGenerationResult(LevelData levelData, IEnumerable<WorldObject> objects)
        {
            _levelData = levelData;
            Objects = objects;
        }

        public string Name => _levelData.Name;

        public Position PlayerStart => _levelData.PlayerStart;

    }
}