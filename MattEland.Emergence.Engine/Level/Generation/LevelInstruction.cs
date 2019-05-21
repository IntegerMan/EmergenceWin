namespace MattEland.Emergence.Engine.Level.Generation
{
    public class LevelInstruction
    {
        public string PrefabId { get; }
        public int X { get; }
        public int Y { get; }
        public string EncounterSet { get; }

        public LevelInstruction(string prefabId, int x, int y, string encounterSet)
        {
            PrefabId = prefabId;
            X = x;
            Y = y;
            EncounterSet = encounterSet;
        }
    }
}