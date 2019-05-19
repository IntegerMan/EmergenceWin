using System.Collections;
using System.Collections.Generic;
using MattEland.Emergence.Model;
using Newtonsoft.Json;

namespace MattEland.Emergence.LevelData
{
    public class LevelData
    {
        public string Name { get; }
        public IEnumerable<LevelInstruction> Instructions { get; }
        public Position PlayerStart { get; }

        public LevelData(string name, Position start, IEnumerable<LevelInstruction> instructions)
        {
            Name = name;
            Instructions = instructions;
            PlayerStart = start;
        }

        public static LevelData LoadFromJson(string json) => JsonConvert.DeserializeObject<LevelData>(json);
    }

}