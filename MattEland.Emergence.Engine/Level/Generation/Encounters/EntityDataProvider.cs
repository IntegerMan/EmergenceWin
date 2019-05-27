using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.DTOs;
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
            DefinePlayer(Actors.PlayerForecast, "Forecaster AI",  
                         e =>
                         {
                             e.Commands = new List<string> {"SPIKE", "RESTORE", "BARRAGE"};
                             e.HelpText = "The forecast AI is a flexible choice that is capable in many different roles";
                         });

            DefinePlayer(Actors.PlayerLogistics, "Logistics Hub",  
                         e =>
                         {
                             e.Commands = new List<string> { "MARK", "RECALL", "SWAP" };
                             e.HelpText = "The logistics hub excels at tactical movement, but is weaker than other AIs";
                         });

            DefinePlayer(Actors.PlayerSearch, "Search AI",  
                         e =>
                         {
                             e.Commands = new List<string> { "SCAN", "ESCAPE", "SURGE" };
                             e.HelpText = "The search AI is geared towards gathering knowledge and avoiding encounters they can't handle";
                             e.LineOfSightRadius = 5.75m;
                         });

            DefinePlayer(Actors.PlayerAntiVirus, "Anti-Virus AI",  
                         e =>
                         {
                             e.Commands = new List<string> { "ARMOR", "CLEANSE", "SWEEP" };
                             e.HelpText = "The anti-virus AI may not hit the hardest, but it can take on many threats at once";
                             e.LineOfSightRadius = 4.5m;
                             e.Defense = 3;
                             e.Accuracy = 95;
                             e.Evasion = 15;
                         });

            DefinePlayer(Actors.PlayerGame, "Game AI",  
                         e =>
                         {
                             e.Commands = new List<string> { "TARGETING", "SPIKE", "BURST" };
                             e.HelpText = "The game AI is all about dishing out damage to specific targets";
                             e.LineOfSightRadius = 4.75m;
                             e.Strength = 3;
                         });

            DefinePlayer(Actors.PlayerMalware, "Malware AI",  
                         e =>
                         {
                             e.Commands = new List<string> { "OVERLOAD", "INFECT", "CORRUPT" };
                             e.HelpText = "Malware aims to cause as much chaos as possible, laying waste to anything in its path";
                         });

            DefinePlayer(Actors.PlayerDebugger, "Debugging AI",  
                         e =>
                         {
                             e.Commands = new List<string> { "INFECT", "ESCAPE", "SWAP", "RESTORE", "OVERLOAD", "BURST", "BARRAGE", "CLEANSE", "SWEEP" };
                             e.HelpText = "The Debugger is intended to excise bugs and make things ready for release.";
                             e.Accuracy = 100;
                             e.Strength = 25;
                             e.Defense = 5;
                             e.Hp = 50;
                             e.Op = 50;
                         });

            DefineActor(Actors.AntiVirus, "Anti-Virus Agent", Alignment.SystemAntiVirus,
                         e =>
                         {
                             e.Accuracy = 50;
                             e.Evasion = 30;
                             e.Strength = 2;
                         });

            DefineActor(Actors.Daemon, "Daemon", Alignment.SystemSecurity,
                         e =>
                         {
                             e.Accuracy = 70;
                             e.Evasion = 5;
                             e.Strength = 3;
                         });

            DefineActor(Actors.Defender, "System Defender", Alignment.SystemAntiVirus,
                         e =>
                         {
                             e.Accuracy = 50;
                             e.Evasion = 30;
                             e.Strength = 3;
                             e.Defense = 2;
                             e.Hp = 5;
                             e.Op = 15;
                         });

            DefineActor(Actors.Inspector, "Inspector", Alignment.SystemAntiVirus,
                         e =>
                         {
                             e.Accuracy = 95;
                             e.Evasion = 15;
                             e.Strength = 1;
                             e.Defense = 0;
                             e.Hp = 3;
                             e.Op = 5;
                             e.BlocksSight = false;
                             e.LootRarity = Rarity.Common;
                         });

            DefineActor(Actors.SecurityAgent, "Security Agent", Alignment.SystemSecurity,
                         e =>
                         {
                             e.Accuracy = 80;
                             e.Evasion = 15;
                             e.Strength = 1;
                             e.Defense = 0;
                             e.Hp = 3;
                             e.Op = 3;
                             e.BlocksSight = false;
                             e.LootRarity = Rarity.Common;
                         });

            DefineActor(Actors.GarbageCollector, "Garbage Collector", Alignment.SystemSecurity,
                         e =>
                         {
                             e.Accuracy = 90;
                             e.Evasion = 0;
                             e.Strength = 5;
                             e.Defense = 1;
                             e.LineOfSightRadius = 7;
                             e.Hp = 10;
                             e.Op = 10;
                             e.LootRarity = Rarity.Epic;
                         });

            DefineActor(Actors.Bit, "Bit", Alignment.SystemCore,
                         e =>
                         {
                             e.Accuracy = 20;
                             e.Evasion = 0;
                             e.Strength = 0;
                             e.Defense = 0;
                             e.Hp = 1;
                             e.Op = 1;
                             e.LootRarity = Rarity.None;
                         });

            DefineActor(Actors.Turret, "Turret", Alignment.SystemSecurity,
                         e =>
                         {
                             e.Accuracy = 42;
                             e.Evasion = 0;
                             e.Strength = 2;
                             e.Defense = 2;
                             e.Hp = 3;
                             e.Op = 30;
                             e.LineOfSightRadius = 7;
                             e.IsImmobile = true;
                             e.LootRarity = Rarity.Rare;
                         });

            DefineActor(Actors.Core, "System Core", Alignment.SystemCore,
                         e =>
                         {
                             e.Accuracy = 20;
                             e.Evasion = 40;
                             e.Strength = 2;
                             e.Defense = 0;
                             e.Hp = 5;
                             e.Op = 15;
                             e.IsImmobile = true;
                             e.LootRarity = Rarity.None;
                         });

            DefineActor(Actors.Helpy, "Helpy", Alignment.SystemCore,
                         e =>
                         {
                             e.Accuracy = 20;
                             e.Evasion = 50;
                             e.Strength = 1;
                             e.Defense = 1;
                             e.Hp = 2;
                             e.Op = 10;
                             e.BlocksSight = false;
                             e.LootRarity = Rarity.Common;
                         });

            DefineActor(Actors.Search, "Query Agent", Alignment.SystemCore,
                         e =>
                         {
                             e.Accuracy = 20;
                             e.Evasion = 25;
                             e.Strength = 0;
                             e.Defense = 0;
                             e.Hp = 2;
                             e.Op = 10;
                             e.LineOfSightRadius = 7;
                             e.BlocksSight = false;
                             e.LootRarity = Rarity.Common;
                         });

            DefineActor(Actors.KernelWorker, "Kernel Worker", Alignment.SystemCore,
                         e =>
                         {
                             e.Accuracy = 20;
                             e.Evasion = 15;
                             e.Strength = 0;
                             e.Defense = 0;
                             e.Hp = 1;
                             e.Op = 1;
                             e.LineOfSightRadius = 7;
                             e.BlocksSight = false;
                             e.LootRarity = Rarity.Common;
                         });

            DefineActor(Actors.LogicBomb, "Logic Bomb", Alignment.Virus,
                         e =>
                         {
                             e.Accuracy = 100;
                             e.Evasion = 20;
                             e.Strength = 3;
                             e.Defense = 0;
                             e.Hp = 3;
                             e.Op = 3;
                             e.BlocksSight = false;
                             e.LootRarity = Rarity.Uncommon;
                         });

            DefineActor(Actors.Virus, "Virus", Alignment.Virus,
                         e =>
                         {
                             e.Accuracy = 50;
                             e.Evasion = 25;
                             e.Strength = 2;
                             e.Defense = 1;
                             e.Hp = 5;
                             e.Op = 10;
                             e.BlocksSight = false;
                             e.LootRarity = Rarity.Common;
                         });

            DefineActor(Actors.Worm, "Worm", Alignment.Virus,
                         e =>
                         {
                             e.Accuracy = 35;
                             e.Evasion = 25;
                             e.Strength = 1;
                             e.Defense = 0;
                             e.Hp = 3;
                             e.Op = 5;
                             e.BlocksSight = false;
                             e.LootRarity = Rarity.Common;
                         });

            DefineActor(Actors.Feature,  "\"Feature\"", Alignment.Virus,
                         e =>
                         {
                             e.Accuracy = 60;
                             e.Evasion = 35;
                             e.Strength = 2;
                             e.Defense = 1;
                             e.Hp = 5;
                             e.Op = 10;
                             e.BlocksSight = false;
                             e.LootRarity = Rarity.Uncommon;
                         });

            DefineActor(Actors.Bug,  "Bug", Alignment.Bug,
                         e =>
                         {
                             e.Accuracy = 45;
                             e.Evasion = 25;
                             e.Strength = 1;
                             e.Defense = 1;
                             e.Hp = 3;
                             e.Op = 5;
                             e.BlocksSight = false;
                             e.LootRarity = Rarity.Common;
                         });

            DefineActor(Actors.Glitch,  "Glitch", Alignment.Bug,
                         e =>
                         {
                             e.Accuracy = 55;
                             e.Evasion = 45;
                             e.Strength = 2;
                             e.Defense = 0;
                             e.Hp = 5;
                             e.Op = 10;
                             e.BlocksSight = false;
                             e.LootRarity = Rarity.None; // Should not be farmable
                         });
        }

        private void DefinePlayer(string id, string name, Action<EntityData> configureAction = null)
        {
            var data = new EntityData
            {
                Id = id,
                Name = name,
                Team = Alignment.Player,
                BlocksSight = false,
                Accuracy = 90,
                Evasion = 20,
                Strength = 2,
                Defense = 1,
                Hp = 10,
                Op = 10,
                LineOfSightRadius = 5.25m,
                Commands = new List<string> {"SPIKE", "RESTORE", "BARRAGE"}
            };

            configureAction?.Invoke(data);

            DefineEntity(id, data);
        }

        private void DefineEntity(string id, EntityData data)
        {
            if (_items.ContainsKey(id)) throw new InvalidOperationException($"Entity {id} has already been defined");
            _items[id] = data;
        }

        private void DefineActor(string id, string name, Alignment alignment, Action<EntityData> configureAction = null)
        {
            var data = new EntityData
            {
                Id = id,
                Name = name,
                Team = alignment,
                BlocksSight = true,
                Accuracy = 50,
                Evasion = 10,
                Strength = 1,
                Defense = 1,
                Hp = 3,
                Op = 5,
                LineOfSightRadius = 5,
                LootRarity = Rarity.Uncommon
            };

            configureAction?.Invoke(data);

            DefineEntity(id, data);
        }

        [NotNull]
        public EntityData GetItem(string id)
        {
            if (!_items.ContainsKey(id)) throw new NotSupportedException($"The entity {id} was not defined in the database");

            return _items[id];
        }
    }
}