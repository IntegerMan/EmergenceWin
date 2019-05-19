using System.Collections.Generic;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Services
{
    public interface IEntityDefinitionService
    {
        IEnumerable<EntityData> GetEntityDefinitions();
        EntityData GetEntity(string id);
    }
}