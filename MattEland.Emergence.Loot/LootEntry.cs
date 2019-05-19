﻿using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Loot
{
    internal class LootEntry
    {
        public GameObjectType ObjectType { get; }
        public string ObjectId { get; }

        public Rarity Rarity { get; set; }

        public bool CanSpawnOnLevel(LevelType level)
        {
            return !MinLevel.HasValue || level >= MinLevel;
        }

        public LevelType? MinLevel { get; set; }

        public LootType LootType { get; set; }
        public string Name { get; set; }

        public LootEntry(LootType lootType,
                         GameObjectType objectType,
                         string objectId,
                         Rarity rarity,
                         LevelType? minLevel = null)
        {
            ObjectType = objectType;
            ObjectId = objectId;
            Rarity = rarity;
            MinLevel = minLevel;
            LootType = lootType;
        }
    }
}