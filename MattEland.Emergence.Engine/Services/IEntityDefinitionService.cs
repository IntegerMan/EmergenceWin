using System.Collections.Generic;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Services
{
    public interface IEntityDefinitionService
    {
        IEnumerable<EntityData> GetEntityDefinitions();
        EntityData GetEntity(string id);
    }
}