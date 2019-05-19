using System.Collections.Generic;
using MattEland.Emergence.Definitions.Commands;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Definitions.Level
{
    public interface IPlayer : IActor
    {
        void ClearKnownCells();

        IEnumerable<ICommandInstance> Commands { get; }
        bool AttemptPickupItem(ICommandContext context, IGameObject item);
    }
}