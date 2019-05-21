using System;
using System.Linq;
using MattEland.Emergence.Engine.Properties;

namespace MattEland.Emergence.Engine.Level.Generation.Encounters
{
    /// <summary>
    /// Provides encounter data for the service layer via a static JSON file.
    /// </summary>
    public sealed class EncounterDataProvider : JsonDataProviderBase<EncounterData>
    {
        /// <inheritdoc />
        protected override string SourceJson => Resources.Encounters;

        /// <inheritdoc />
        public override EncounterData GetItem(string id)
        {
            return Items.FirstOrDefault(d => string.Equals(d.Id, id, StringComparison.OrdinalIgnoreCase));
        }

    }
}