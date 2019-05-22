using System.Collections.Generic;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Level.Generation.Encounters
{
    public sealed class EntityDefinitionService
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