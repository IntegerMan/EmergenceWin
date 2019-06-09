using System;
using System.Collections.Generic;
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
                         });

            DefinePlayer(Actors.PlayerLogistics, "Logistics Hub",  
                         e =>
                         {
                             e.Commands = new List<string> { "MARK", "RECALL", "SWAP" };
                         });

            DefinePlayer(Actors.PlayerSearch, "Search AI",  
                         e =>
                         {
                             e.Commands = new List<string> { "SCAN", "ESCAPE", "SURGE" };
                             e.LineOfSightRadius = 5.75m;
                         });

            DefinePlayer(Actors.PlayerAntiVirus, "Anti-Virus AI",  
                         e =>
                         {
                             e.Commands = new List<string> { "ARMOR", "CLEANSE", "SWEEP" };
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
                             e.LineOfSightRadius = 4.75m;
//                             e.Strength = 3;
                         });

            DefinePlayer(Actors.PlayerMalware, "Malware AI",  
                         e =>
                         {
                             e.Commands = new List<string> { "OVERLOAD", "INFECT", "CORRUPT" };
                         });

            DefinePlayer(Actors.PlayerDebugger, "Debugging AI",  
                         e =>
                         {
                             e.Commands = new List<string> { "INFECT", "ESCAPE", "SWAP", "RESTORE", "OVERLOAD", "BURST", "BARRAGE", "CLEANSE", "SWEEP" };
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

    }
}