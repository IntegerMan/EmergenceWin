using MattEland.Emergence.Engine.Entities;

namespace MattEland.Emergence.Engine.Services
{
    public interface IBrainProvider
    {
        IBrain GetBrainForActor(Actor actor);
    }
}