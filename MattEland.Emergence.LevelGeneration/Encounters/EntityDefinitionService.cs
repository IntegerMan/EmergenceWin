using System.Collections.Generic;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.LevelGeneration.Encounters
{
    public sealed class EntityDefinitionService : IEntityDefinitionService
    {
        private readonly EntityDataProvider _entityDataProvider;

        public EntityDefinitionService()
        {
            _entityDataProvider = new EntityDataProvider();
            
        }

        public IEnumerable<EntityData> GetEntityDefinitions()
        {
            return _entityDataProvider.Items;
        }

        public EntityData GetEntity(string id)
        {
            return _entityDataProvider.GetItem(id);
        }
    }
}