using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Services
{
    public interface IBrainProvider
    {
        IBrain GetBrainForActor(IActor actor);
    }
}