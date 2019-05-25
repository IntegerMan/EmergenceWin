using System.Collections.Generic;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Level.Generation.Encounters
{

    /// <summary>
    /// Provides encounter data for the service layer via a static JSON file.
    /// </summary>
    public sealed class EntityDataProvider
    {
        private readonly IDictionary<string, EntityData> _items;

        public EntityDataProvider()
        {
            _items = new Dictionary<string, EntityData>();

            LoadData();
        }

        public IEnumerable<EntityData> Items => _items.Values;

        private void LoadData()
        {
            _items["ACTOR_PLAYER_FORECAST"] = new EntityData
            {
                Id = "ACTOR_PLAYER_FORECAST",
                Name = "Forecaster AI",
                Team = Alignment.Player,
                BlocksSight = false,
                Accuracy = 90,
                Evasion = 20,
                Strength = 2,
                Defense = 1,
                Hp = 10,
                Op = 10,
                HelpText = "The forecast AI is a flexible choice that is capable in many different roles",
                LineOfSightRadius = 5.25m,
                Commands = new List<string> { "SPIKE", "RESTORE", "BARRAGE"}
            };
            _items["ACTOR_PLAYER_LOGISTICS"] = new EntityData
            {
                Id = "ACTOR_PLAYER_LOGISTICS",
                Name = "Logistics Hub",
                Team = Alignment.Player,
                BlocksSight = false,
                Accuracy = 90,
                Evasion = 20,
                Strength = 2,
                Defense = 1,
                Hp = 10,
                Op = 10,
                HelpText = "The logistics hub excels at tactical movement, but is weaker than other AIs",
                LineOfSightRadius = 5.25m,
                Commands = new List<string> { "MARK", "RECALL", "SWAP" }
            };
            _items["ACTOR_PLAYER_SEARCH"] = new EntityData
            {
                Id = "ACTOR_PLAYER_SEARCH",
                Name = "Search AI",
                Team = Alignment.Player,
                BlocksSight = false,
                Accuracy = 90,
                Evasion = 20,
                Strength = 2,
                Defense = 1,
                LineOfSightRadius = 5.75m,
                HelpText = "The search AI is geared towards gathering knowledge and avoiding encounters they can't handle",
                Hp = 10,
                Op = 10,
                Commands = new List<string> { "SCAN", "ESCAPE", "SURGE" }
            };
            _items["ACTOR_PLAYER_ANTIVIRUS"] = new EntityData
            {
                Id = "ACTOR_PLAYER_ANTIVIRUS",
                Name = "Anti-Virus AI",
                Team = Alignment.Player,
                BlocksSight = false,
                Accuracy = 95,
                Evasion = 15,
                Strength = 2,
                Defense = 3,
                Hp = 10,
                Op = 10,
                HelpText = "The anti-virus AI may not hit the hardest, but it can take on many threats at once",
                LineOfSightRadius = 4.5m,
                Commands = new List<string> { "ARMOR", "CLEANSE", "SWEEP"}
            };
            _items["ACTOR_PLAYER_GAME"] = new EntityData
            {
                Id = "ACTOR_PLAYER_GAME",
                Name = "Game AI",
                Team = Alignment.Player,
                BlocksSight = false,
                Accuracy = 90,
                Evasion = 20,
                Strength = 3,
                Defense = 2,
                Hp = 10,
                Op = 10,
                HelpText = "The game AI is all about dishing out damage to specific targets",
                LineOfSightRadius = 4.75m,
                Commands = new List<string> { "TARGETING", "SPIKE", "BURST" }
            };
            _items["ACTOR_PLAYER_MALWARE"] = new EntityData
            {
                Id = "ACTOR_PLAYER_MALWARE",
                Name = "Malware AI",
                Team = Alignment.Player,
                BlocksSight = false,
                Accuracy = 90,
                Evasion = 20,
                Strength = 2,
                Defense = 1,
                Hp = 10,
                Op = 10,
                HelpText = "Malware aims to cause as much chaos as possible, laying waste to anything in its path",
                LineOfSightRadius = 5.25m,
                Commands = new List<string> { "OVERLOAD", "INFECT", "CORRUPT" }
            };
            _items["ACTOR_PLAYER_DEBUGGER"] = new EntityData
            {
                Id = "ACTOR_PLAYER_DEBUGGER",
                Name = "Debugging AI",
                Team = Alignment.Player,
                BlocksSight = false,
                Accuracy = 100,
                Evasion = 20,
                Strength = 25,
                Defense = 5,
                LineOfSightRadius = 5.25m,
                HelpText = "The Debugger is intended to excise bugs and make things ready for release.",
                Hp = 50,
                Op = 50,
                Commands = new List<string> { "INFECT", "ESCAPE", "SWAP", "RESTORE", "OVERLOAD", "BURST", "BARRAGE", "CLEANSE", "SWEEP"}
            };
            _items["ACTOR_ANTI_VIRUS"] = new EntityData
            {
                Id = "ACTOR_ANTI_VIRUS",
                Name = "Anti-Virus Agent",
                Team = Alignment.SystemAntiVirus,
                BlocksSight = true,
                Accuracy = 50,
                Evasion = 30,
                Strength = 2,
                Defense = 1,
                LineOfSightRadius = 5,
                Hp = 3,
                Op = 5,
                LootRarity = Rarity.Uncommon
            };
            _items["ACTOR_DAEMON"] = new EntityData
            {
                Id = "ACTOR_DAEMON",
                Name = "Daemon",
                Team = Alignment.SystemSecurity,
                BlocksSight = true,
                Accuracy = 70,
                Evasion = 5,
                Strength = 3,
                Defense = 1,
                LineOfSightRadius = 5,
                Hp = 3,
                Op = 5,
                LootRarity = Rarity.Uncommon
            };
            _items["ACTOR_DEFENDER"] = new EntityData
            {
                Id = "ACTOR_DEFENDER",
                Name = "System Defender",
                Team = Alignment.SystemAntiVirus,
                BlocksSight = true,
                Accuracy = 50,
                Evasion = 30,
                Strength = 3,
                Defense = 2,
                LineOfSightRadius = 5,
                Hp = 5,
                Op = 15,
                LootRarity = Rarity.Uncommon
            };
            _items["ACTOR_INSPECTOR"] = new EntityData
            {
                Id = "ACTOR_INSPECTOR",
                Name = "Inspector",
                Team = Alignment.SystemAntiVirus,
                BlocksSight = false,
                Accuracy = 95,
                Evasion = 15,
                Strength = 1,
                Defense = 0,
                LineOfSightRadius = 5,
                Hp = 3,
                Op = 5,
                LootRarity = Rarity.Common
            };
            _items["ACTOR_SEC_AGENT"] = new EntityData
            {
                Id = "ACTOR_SEC_AGENT",
                Name = "Security Agent",
                Team = Alignment.SystemSecurity,
                BlocksSight = false,
                Accuracy = 80,
                Evasion = 15,
                Strength = 1,
                Defense = 0,
                LineOfSightRadius = 5,
                Hp = 3,
                Op = 3,
                LootRarity = Rarity.Common
            };
            _items["ACTOR_GARBAGE_COLLECTOR"] = new EntityData
            {
                Id = "ACTOR_GARBAGE_COLLECTOR",
                Name = "Garbage Collector",
                Team = Alignment.SystemSecurity,
                BlocksSight = true,
                Accuracy = 90,
                Evasion = 0,
                Strength = 5,
                Defense = 1,
                LineOfSightRadius = 7,
                Hp = 10,
                Op = 10,
                LootRarity = Rarity.Epic
            };
            _items["ACTOR_BIT"] = new EntityData
            {
                Id = "ACTOR_BIT",
                Name = "Bit",
                Team = Alignment.SystemCore,
                BlocksSight = false,
                Accuracy = 20,
                Evasion = 0,
                Strength = 0,
                Defense = 0,
                LineOfSightRadius = 5,
                Hp = 1,
                Op = 1,
                LootRarity = Rarity.None
            };
            _items["ACTOR_TURRET"] = new EntityData
            {
                Id = "ACTOR_TURRET",
                Name = "Turret",
                Team = Alignment.SystemSecurity,
                BlocksSight = true,
                Accuracy = 42,
                Evasion = 0,
                Strength = 2,
                Defense = 2,
                LineOfSightRadius = 7,
                Hp = 3,
                Op = 30,
                IsImmobile = true,
                LootRarity = Rarity.Rare
            };
            _items["ACTOR_CORE"] = new EntityData
            {
                Id = "ACTOR_CORE",
                Name = "System Core",
                Team = Alignment.SystemCore,
                BlocksSight = true,
                Accuracy = 20,
                Evasion = 40,
                Strength = 2,
                LineOfSightRadius = 5,
                Defense = 0,
                Hp = 5,
                Op = 15,
                IsImmobile = true,
                LootRarity = Rarity.None
            };
            _items["ACTOR_HELP"] = new EntityData
            {
                Id = "ACTOR_HELP",
                Name = "Helpy",
                Team = Alignment.SystemCore,
                BlocksSight = false,
                Accuracy = 20,
                Evasion = 50,
                Strength = 1,
                Defense = 1,
                LineOfSightRadius = 5,
                Hp = 2,
                Op = 10,
                LootRarity = Rarity.Common
            };
            _items["ACTOR_SEARCH"] = new EntityData
            {
                Id = "ACTOR_SEARCH",
                Name = "Query Agent",
                Team = Alignment.SystemCore,
                BlocksSight = false,
                Accuracy = 20,
                Evasion = 25,
                LineOfSightRadius = 7,
                Strength = 0,
                Defense = 0,
                Hp = 2,
                Op = 10,
                LootRarity = Rarity.Common
            };
            _items["ACTOR_KERNEL_WORKER"] = new EntityData
            {
                Id = "ACTOR_KERNEL_WORKER",
                Name = "Kernel Worker",
                Team = Alignment.SystemCore,
                BlocksSight = false,
                Accuracy = 20,
                Evasion = 15,
                LineOfSightRadius = 5,
                Strength = 0,
                Defense = 0,
                Hp = 1,
                Op = 1,
                LootRarity = Rarity.Common
            };
            _items["ACTOR_LOGIC_BOMB"] = new EntityData
            {
                Id = "ACTOR_LOGIC_BOMB",
                Name = "Logic Bomb",
                Team = Alignment.Virus,
                BlocksSight = false,
                Accuracy = 100,
                Evasion = 20,
                LineOfSightRadius = 5,
                Strength = 3,
                Defense = 0,
                Hp = 3,
                Op = 5,
                LootRarity = Rarity.Uncommon
            };
            _items["ACTOR_VIRUS"] = new EntityData
            {
                Id = "ACTOR_VIRUS",
                Name = "Virus",
                Team = Alignment.Virus,
                BlocksSight = false,
                Accuracy = 50,
                Evasion = 25,
                LineOfSightRadius = 5,
                Strength = 2,
                Defense = 1,
                Hp = 5,
                Op = 10,
                LootRarity = Rarity.Common
            };
            _items["ACTOR_WORM"] = new EntityData
            {
                Id = "ACTOR_WORM",
                Name = "Worm",
                Team = Alignment.Virus,
                BlocksSight = false,
                Accuracy = 35,
                Evasion = 25,
                LineOfSightRadius = 5,
                Strength = 1,
                Defense = 0,
                Hp = 3,
                Op = 5,
                LootRarity = Rarity.Common
            };
            _items["ACTOR_FEATURE"] = new EntityData
            {
                Id = "ACTOR_FEATURE",
                Name = "\"Feature\"",
                Team = Alignment.Virus, // Only so it's not targeted by the system
                BlocksSight = false,
                Accuracy = 60,
                Evasion = 35,
                LineOfSightRadius = 5,
                Strength = 2,
                Defense = 1,
                Hp = 5,
                Op = 10,
                LootRarity = Rarity.Uncommon
            };
            _items["ACTOR_BUG"] = new EntityData
            {
                Id = "ACTOR_BUG",
                Name = "Bug",
                Team = Alignment.Bug,
                BlocksSight = false,
                Accuracy = 45,
                Evasion = 25,
                LineOfSightRadius = 5,
                Strength = 1,
                Defense = 1,
                Hp = 3,
                Op = 5,
                LootRarity = Rarity.Common
            };
            _items["ACTOR_GLITCH"] = new EntityData
            {
                Id = "ACTOR_GLITCH",
                Name = "Glitch",
                Team = Alignment.Bug,
                BlocksSight = false,
                Accuracy = 55,
                Evasion = 45,
                LineOfSightRadius = 5,
                Strength = 2,
                Defense = 0,
                Hp = 5,
                Op = 10,
                LootRarity = Rarity.None // Never reward glitch farming
            };
        }

        public EntityData GetItem(string id)
        {
            if (!_items.ContainsKey(id))
            {
                return null;
            }

            return _items[id];
        }
    }
}