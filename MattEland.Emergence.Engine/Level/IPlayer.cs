using System.Collections.Generic;
using MattEland.Emergence.Engine.Commands;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Level
{
    public interface IPlayer : IActor
    {
        void ClearKnownCells();

        IEnumerable<ICommandInstance> Commands { get; }
        bool AttemptPickupItem(CommandContext context, IGameObject item);
    }
}