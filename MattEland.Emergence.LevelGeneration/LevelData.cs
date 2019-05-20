using System.Collections.Generic;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model;
using MattEland.Emergence.Model;
using Newtonsoft.Json;

namespace MattEland.Emergence.LevelGeneration
{
    public class LevelData
    {
        public string Name { get; }
        public IEnumerable<LevelInstruction> Instructions { get; }
        public Pos2D PlayerStart { get; }

        public LevelData(string name, Pos2D start, IEnumerable<LevelInstruction> instructions)
        {
            Name = name;
            Instructions = instructions;
            PlayerStart = start;
        }

        public static LevelData LoadFromJson(string json) => JsonConvert.DeserializeObject<LevelData>(json);
    }

}