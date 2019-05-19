using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Services
{
    public interface IBrainProvider
    {
        IBrain GetBrainForActor(IActor actor);
    }
}