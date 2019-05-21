using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.AI
{
    public interface ICommandContextGenerator
    {
        ICommandContext Generate(LevelType levelType);
    }
}