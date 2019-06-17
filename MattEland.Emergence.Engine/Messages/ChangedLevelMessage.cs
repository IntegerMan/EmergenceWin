using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Messages
{
    public class ChangedLevelMessage : GameMessage
    {
        public LevelType Level { get; }

        public ChangedLevelMessage(LevelType level)
        {
            Level = level;
        }

        public override string ForegroundColor => GameColors.Blue;
    }
}