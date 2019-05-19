using System;
using System.Linq;
using MattEland.Emergence.LevelGeneration.Properties;

namespace MattEland.Emergence.LevelGeneration.Encounters
{
    /// <summary>
    /// A data provider that provides access to <see cref="EncounterSet"/> instances.
    /// </summary>
    public sealed class EncounterSetDataProvider : JsonDataProviderBase<EncounterSet>
    {
        /// <inheritdoc />
        protected override string SourceJson => Resources.EncounterSets;

        /// <inheritdoc />
        public override EncounterSet GetItem(string id)
        {
            return Items.FirstOrDefault(d => string.Equals(d.Id, id, StringComparison.OrdinalIgnoreCase));
        }        
    }
}