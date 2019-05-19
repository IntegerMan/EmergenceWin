using System.Collections.Generic;
using MattEland.Emergence.Model;

namespace MattEland.Emergence.LevelGeneration
{
    public class RoomData
    {
        public string Id { get; set; }
        public List<string> Data { get; set; }
        public bool IsInvulnerable { get; set; }

        public char GetCharacterAtPosition(Position pos)
        {
            if (pos.X < 0 || pos.Y < 0 || pos.Y >= Data.Count) return ' ';

            var row = Data[pos.Y];

            if (row == null || pos.X >= row.Length) return ' ';

            return row[pos.X];
        }
    }
}
