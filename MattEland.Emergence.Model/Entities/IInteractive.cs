using System.Collections.Generic;
using MattEland.Emergence.Model.Messages;

namespace MattEland.Emergence.Model.Entities
{
    public interface IInteractive
    {
        void Interact(ICommandContext context);
    }
}