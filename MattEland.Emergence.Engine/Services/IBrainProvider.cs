using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Services
{
    public interface IBrainProvider
    {
        IBrain GetBrainForActor(Actor actor);
    }
}