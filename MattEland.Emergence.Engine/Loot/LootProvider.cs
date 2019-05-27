using System.Collections.Generic;
using System.Linq;
using MattEland.Emergence.Engine.Commands;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Loot
{
    public class LootProvider
    {
        private readonly IList<LootEntry> _entries;

        public LootProvider()
        {
            _entries = new List<LootEntry>();

            BuildLootTable();
        }

        private void BuildLootTable()
        {
            // Add entries for common types of objects
            _entries.Add(new LootEntry(LootType.Operations, GameObjectType.GenericPickup, "GET_OPS", Rarity.Common));
            _entries.Add(new LootEntry(LootType.MaxOperations, GameObjectType.GenericPickup, "GET_MAXOPS", Rarity.Epic));
            _entries.Add(new LootEntry(LootType.Stability, GameObjectType.GenericPickup, "GET_HP", Rarity.Common));
            _entries.Add(new LootEntry(LootType.MaxStability, GameObjectType.GenericPickup, "GET_MAXHP", Rarity.Epic));
            _entries.Add(new LootEntry(LootType.Experience, GameObjectType.GenericPickup, "BONUS_XP", Rarity.Common));

            foreach (var command in CommandFactory.RegisteredCommands)
            {
                var entry = new LootEntry(LootType.Command, GameObjectType.CommandPickup, command.Id, command.Rarity, command.MinLevel)
                {
                    Name = command.Name
                };

                _entries.Add(entry);
            }

        }

        public void SpawnLootAsNeeded(CommandContext context, GameObjectBase source, Rarity rarity)
        {
            if (rarity >= Rarity.Rare || context.Randomizer.GetInt(1, 6) >= 5)
            {
                // Okay, loot is coming. Let's see how good it's gunna be

                if (context.Randomizer.GetInt(1, 6) >= 5)
                {
                    // Sweet! Loot upgrade!
                    SpawnLoot(context, source, rarity.Upgrade());
                }
                else
                {
                    // Cool. You've earned some loot.
                    SpawnLoot(context, source, rarity);
                }
            }
        }

        public void SpawnLoot(CommandContext context, GameObjectBase source, Rarity rarity)
        {

            if (rarity == Rarity.None)
            {
                return;
            }

            // Filter out disallowed items
            var eligible = _entries.Where(e => e.Rarity <= rarity && e.CanSpawnOnLevel(context.Level.Id));

            // Randomly order things by tier and create an evaluation queue based on the results
            eligible = eligible.OrderByDescending(e => ((int) e.Rarity * 1000) + context.Randomizer.GetDouble());
            var possibleEntries = new Queue<LootEntry>(eligible);

            // Check count prior to attempting to Peek or Dequeueue
            while (possibleEntries.Count > 0)
            {
                var entry = possibleEntries.Dequeue();

                switch (entry.LootType)
                {
                    case LootType.Command when LootAlreadyExists(context, entry):
                        continue;
                    case LootType.Experience: // Just not supported yet
                        continue;
                }

                // Actually create the entry
                var obj = CreationService.CreateObject(entry.ObjectId, entry.ObjectType, source.Pos, dto =>
                {
                    if (!string.IsNullOrWhiteSpace(entry.Name))
                    {
                        dto.Name = entry.Name;
                    }
                });


                context.Level.AddObject(obj);

                return;
            }
        }

        private static bool LootAlreadyExists(CommandContext context, LootEntry entry)
        {
            foreach (var cell in context.Level.Cells)
            {
                if (cell.Objects.Any(o => o.ObjectId == entry.ObjectId))
                {
                    return true;
                }

            }

            if (context.Player != null)
            {
                foreach (var instance in context.Player.Commands)
                {
                    if (instance?.Command?.Id == entry.ObjectId)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}