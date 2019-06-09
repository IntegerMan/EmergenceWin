using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.DTOs;

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
/*
                             e.Defense = 3;
                             e.Accuracy = 95;
                             e.Evasion = 15;
*/
                         });

            DefinePlayer(Actors.PlayerGame, "Game AI",  
                         e =>
                         {
                             e.Commands = new List<string> { "TARGETING", "SPIKE", "BURST" };
                             e.HelpText = "The game AI is all about dishing out damage to specific targets";
                             e.LineOfSightRadius = 4.75m;
//                             e.Strength = 3;
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
/*
                             e.Accuracy = 100;
                             e.Strength = 25;
                             e.Defense = 5;
*/
                             e.Hp = 50;
                             e.Op = 50;
                         });
        }

        private void DefinePlayer(string id, string name, Action<EntityData> configureAction = null)
        {
            var data = new EntityData
            {
                Id = id,
                Name = name,
                Team = Alignment.Player,
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

        [NotNull]
        public EntityData GetItem(string id)
        {
            if (!_items.ContainsKey(id)) throw new NotSupportedException($"The entity {id} was not defined in the database");

            return _items[id];
        }
    }
}