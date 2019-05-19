using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Services.AI
{
    public interface ICommandContextGenerator
    {
        ICommandContext Generate(LevelType levelType);
    }
}