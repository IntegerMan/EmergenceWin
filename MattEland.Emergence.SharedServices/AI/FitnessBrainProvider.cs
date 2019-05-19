using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Services.AI
{
    public class FitnessBrainProvider : IBrainProvider
    {
        private readonly IBrain _brain;
        private readonly string _actorId;
        private readonly IBrainProvider _fallbackProvider;

        public FitnessBrainProvider(IBrain brain, string actorId, IBrainProvider fallbackProvider)
        {
            _brain = brain;
            _actorId = actorId;
            _fallbackProvider = fallbackProvider;
        }

        public IBrain GetBrainForActor(IActor actor)
        {
            if (actor.ObjectId == _actorId)
            {
                return _brain;
            }

            return _fallbackProvider.GetBrainForActor(actor);
        }
    }
}