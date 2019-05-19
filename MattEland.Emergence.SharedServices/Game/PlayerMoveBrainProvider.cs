using MattEland.Emergence.AI;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Services.Game
{
    public class PlayerMoveBrainProvider : IBrainProvider
    {
        private readonly IBrainProvider _fallbackProvider;
        private readonly GameCommandDTO _command;

        public PlayerMoveBrainProvider(IBrainProvider fallbackProvider,
                                       GameCommandDTO command)
        {
            _fallbackProvider = fallbackProvider;
            _command = command;
        }

        public IBrain GetBrainForActor(IActor actor)
        {
            return actor.IsPlayer 
                ? new PlayerCommandBrain(_command, actor) 
                : _fallbackProvider.GetBrainForActor(actor);
        }
    }
}