﻿using System;
using System.Linq;
using MattEland.Emergence.Engine.Properties;

namespace MattEland.Emergence.Engine.Level.Generation.Prefabs
{
    /// <summary>
    /// Provides prefab data for the service layer via a static JSON file.
    /// </summary>
    public sealed class PrefabDataProvider : JsonDataProviderBase<PrefabData>
    {
        /// <inheritdoc />
        protected override string SourceJson => Resources.Prefabs;

        /// <inheritdoc />
        public override PrefabData GetItem(string id)
        {
            return Items.FirstOrDefault(d => string.Equals(d.Id, id, StringComparison.OrdinalIgnoreCase));
        }

    }
}