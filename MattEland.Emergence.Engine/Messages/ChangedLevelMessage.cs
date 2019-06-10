using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Messages
{
    public class ChangedLevelMessage : GameMessage
    {
        public LevelType Level { get; }

        public ChangedLevelMessage(LevelType level)
        {
            Level = level;
        }
    }
}