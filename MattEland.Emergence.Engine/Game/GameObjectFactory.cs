﻿using System;
using System.Collections.Generic;
using MattEland.Emergence.Engine.Commands;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Entities.Items;
using MattEland.Emergence.Engine.Entities.Obstacles;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Game
{
    public static class GameObjectFactory
    {

        /// <summary>
        /// Creates a player object instance with stats from the defined <paramref name="playerType"/>.
        /// </summary>
        /// <returns>The player instance</returns>
        public static Player CreatePlayer(Pos2D pos, PlayerType playerType)
        {
            var player = new Player(pos, playerType);

            var commands = GetStartingCommandsForPlayer(playerType);

            InitializeCommandSlots(player, commands);
            
            return player;
            /*
            var entityDef = EntityService.GetEntity(playerId);

            // Position doesn't really matter since LevelBuilder will auto-set the position
            SetEntityStats(dto, entityDef, GameObjectType.Player, new Pos2D());

            dto.Hotbar = BuildCommandSlots(10);
            dto.StoredCommands = BuildCommandSlots(30);

            int commandIndex = 0;
            foreach (var command in entityDef.Commands)
            {
                CommandInfoDto commandInfoDto = null;

                if (command != null)
                {
                    var commandDef = CommandFactory.CreateCommand(command);

                    commandInfoDto = commandDef?.BuildDto(false);
                }

                dto.Hotbar[commandIndex++] = commandInfoDto;
            }

            return (Player)CreateFromDto(dto);
            */
        }

        private static List<GameCommand> GetStartingCommandsForPlayer(PlayerType playerType)
        {
            var commands = new List<GameCommand>();

            switch (playerType)
            {
                case PlayerType.Logistics:
                    commands.AddRange(new GameCommand[]
                    {
                        new MarkCommand(),
                        new RecallCommand(),
                        new SwapCommand()
                    });
                    break;
                case PlayerType.Forecast:
                    commands.AddRange(new GameCommand[]
                    {
                        new SpikeCommand(),
                        new RestoreCommand(),
                        new BarrageCommand()
                    });
                    break;
                case PlayerType.Game:
                    commands.AddRange(new GameCommand[]
                    {
                        new TargetingCommand(),
                        new SpikeCommand(),
                        new BurstCommand()
                    });
                    break;
                case PlayerType.Search:
                    commands.AddRange(new GameCommand[]
                    {
                        new ScanCommand(),
                        new EscapeCommand(),
                        new SurgeCommand()
                    });
                    break;
                case PlayerType.Malware:
                    commands.AddRange(new GameCommand[]
                    {
                        new OverloadCommand(),
                        new InfectCommand(),
                        new CorruptCommand()
                    });
                    break;
                case PlayerType.Debugger:
                    commands.AddRange(new GameCommand[]
                    {
                        new InfectCommand(),
                        new EscapeCommand(),
                        new SwapCommand(),
                        new RestoreCommand(),
                        new OverloadCommand(),
                        new BurstCommand(),
                        new BarrageCommand(),
                        new CleanseCommand(),
                        new VirusSweepCommand(),
                    });
                    break;
                case PlayerType.AntiVirus:
                    commands.AddRange(new GameCommand[]
                    {
                        new ArmorCommand(),
                        new CleanseCommand(),
                        new VirusSweepCommand()
                    });
                    break;
                default:
                    throw new NotSupportedException($"Cannot set commands for unknown player type {playerType:G}");
            }

            return commands;
        }
        
        const int NumCommandSlots = 8;

        private static void InitializeCommandSlots(Player player, List<GameCommand> commands)
        {
            for (int i = 0; i < NumCommandSlots; i++)
            {
                GameCommand command = null;

                if (commands.Count > i)
                {
                    command = commands[i];
                }

                player.HotbarCommands.Add(new CommandSlot(command));
            }
        }

        /*
        private static List<CommandInfoDto> BuildCommandSlots(int count)
        {
            var slots = new List<CommandInfoDto>(count);

            // Add in a bunch of empty slots that we can swap out actual commands in lieu of
            for (int i = 0; i < count; i++)
            {
                slots.Add(null);
            }

            return slots;
        }

        private static ActorDto SetEntityStats(ActorDto dto, string objectId, GameObjectType objectType, Pos2D position)
        {
            // The entity definition service is what defines what each actor starts with for stats
            var definition = EntityService.GetEntity(objectId);
            if (definition == null)
            {
                throw new InvalidOperationException($"Could not locate an actor entity definition for '{objectId}'");
            }

            return SetEntityStats(dto, definition, objectType, position);
        }

        private static ActorDto SetEntityStats(ActorDto dto, EntityData definition, GameObjectType objectType, Pos2D position)
        {
            // Set the basic properties
            dto.ObjectId = definition.Id;
            dto.Type = objectType;
            dto.Pos = position;

            // Copy over basic stats
            dto.MaxHp = definition.Hp;
            dto.HpUsed = 0;
            dto.MaxOp = definition.Op;
            dto.OpUsed = 0;
            dto.Name = definition.Name;
            dto.BlocksSight = definition.BlocksSight;
            dto.Accuracy = definition.Accuracy;
            dto.Evasion = definition.Evasion;
            dto.Strength = definition.Strength;
            dto.Defense = definition.Defense;
            dto.LineOfSightRadius = definition.LineOfSightRadius;
            dto.Team = definition.Team;
            dto.IsImmobile = definition.IsImmobile;
            dto.LootRarity = definition.LootRarity;

            return dto;
        }
        */

        private static ActorType GetActorType(string id)
        {
            switch (id)
            {
                case Actors.Glitch: return ActorType.Glitch;
                case Actors.Core: return ActorType.Core;
                case Actors.Feature: return ActorType.Feature;
                case Actors.Turret: return ActorType.Turret;
                case Actors.Worm: return ActorType.Worm;
                case Actors.AntiVirus: return ActorType.AntiVirus;
                case Actors.Bit: return ActorType.Bit;
                case Actors.Daemon: return ActorType.Daemon;
                case Actors.Defender: return ActorType.SystemDefender;
                case Actors.GarbageCollector: return ActorType.GarbageCollector;
                case Actors.Helpy: return ActorType.Helpy;
                case Actors.Inspector: return ActorType.Inspector;
                case Actors.Bug: return ActorType.Bug;
                case Actors.KernelWorker: return ActorType.KernelWorker;
                case Actors.LogicBomb: return ActorType.LogicBomb;
                case Actors.Virus: return ActorType.Virus;
                case Actors.Search: return ActorType.QueryAgent;
                case Actors.SecurityAgent: return ActorType.SecurityAgent;
/*

                case Actors.PlayerAntiVirus:
                case Actors.PlayerDebugger:
                case Actors.PlayerLogistics:
                case Actors.PlayerSearch:
                case Actors.PlayerMalware:
                case Actors.PlayerForecast:
                case Actors.PlayerGame: 
                    return ActorType.Player;
*/

                default: throw new NotSupportedException($"ActorType mapping not found for {id}");
            }
        }
        private static PlayerType GetPlayerType(string id)
        {
            switch (id)
            {
                case Actors.PlayerAntiVirus: return PlayerType.AntiVirus;
                case Actors.PlayerDebugger: return PlayerType.Debugger;
                case Actors.PlayerLogistics: return PlayerType.Logistics;
                case Actors.PlayerSearch: return PlayerType.Search;
                case Actors.PlayerMalware: return PlayerType.Malware;
                case Actors.PlayerForecast: return PlayerType.Forecast;
                case Actors.PlayerGame: return PlayerType.Game;

                default: throw new NotSupportedException($"PlayerType mapping not found for {id}");
            }
        }

        public static GameObjectBase CreateObject(string id, GameObjectType objType, Pos2D pos)
        {
            // Figure out which DTO to build
            switch (objType)
            {
                case GameObjectType.Turret:
                    return new Turret(pos);

                case GameObjectType.Core:
                    return CreateCore(pos);

                case GameObjectType.Actor:
                    return CreateActor(id, pos);

                case GameObjectType.Door:
                    return new Door(pos);

                case GameObjectType.Treasure:
                    return new TreasureTrove(pos);

                case GameObjectType.Divider:
                    return new Divider(pos);

                case GameObjectType.Cabling:
                    return new Cabling(pos);

                case GameObjectType.Firewall:
                    return new Firewall(pos);

                case GameObjectType.Exit:
                    return new LevelExit(pos);

                case GameObjectType.Entrance:
                    return new LevelEntrance(pos);

                case GameObjectType.Service:
                    return new LevelService(pos);

                case GameObjectType.DataStore:
                    return new DataStore(pos);

                case GameObjectType.Wall:
                    return new Wall(pos, false);

                case GameObjectType.Debris:
                    return new Debris(pos);

                case GameObjectType.CommandPickup:
                    return new CommandPickup(pos, id, "Command Pickup");

                case GameObjectType.Water:
                    return new Water(pos);

                case GameObjectType.Help:
                    return new HelpTile(pos, id);

                case GameObjectType.CharacterSelect:
                    return new CharacterSelectTile(pos, GetPlayerType(id));

                case GameObjectType.GenericPickup:
                    return CreatePickup(id, pos);

                case GameObjectType.Floor:
                case GameObjectType.Player:
                    throw new NotSupportedException($"{objType:G} / {id} cannot be instantiated using CreateObject");

                default:
                    throw new NotImplementedException($"{objType:G} / {id} is not supported for instantiation");
            }
        }

        private static GameObjectBase CreatePickup(string id, Pos2D pos)
        {
            switch (id)
            {
                case "GET_MAXHP":
                    return new MaxStabilityPickup(pos);
                case "GET_HP":
                    return new StabilityPickup(pos);
                case "GET_MAXOPS":
                    return new MaxOperationsPickup(pos);
                case "GET_OPS":
                    return new OperationsPickup(pos);
                default:
                    throw new NotSupportedException($"Pickup {id} cannot be instantiated");
            }
        }


        public static GameObjectBase CreateWall(Pos2D pos, bool isExterior)
        {
            var wall = new Wall(pos, isExterior);

            if (isExterior)
            {
                wall.SetInvulnerable();
            }

            return wall;
        }

        public static GameObjectBase CreateCore(Pos2D pos) => new LevelCore(pos);

        public static Actor CreateActor(string id, Pos2D pos)
        {
            ActorType actorType = GetActorType(id);
            switch (actorType)
            {
                case ActorType.AntiVirus: return new AntiVirus(pos);
                case ActorType.LogicBomb: return new LogicBomb(pos);
                case ActorType.Turret: return new Turret(pos);
                case ActorType.Core: return new LevelCore(pos);
                case ActorType.Player: return CreatePlayer(pos, GetPlayerType(id));
                case ActorType.Bug: return new Bug(pos);
                case ActorType.Virus: return new Virus(pos);
                case ActorType.Worm: return new Worm(pos);
                case ActorType.Helpy: return new Helpy(pos);
                case ActorType.Bit: return new Bit(pos);
                case ActorType.Daemon: return new Daemon(pos);
                case ActorType.SystemDefender: return new SystemDefender(pos);
                case ActorType.Inspector: return new Inspector(pos);
                case ActorType.SecurityAgent: return new SecurityAgent(pos);
                case ActorType.GarbageCollector: return new GarbageCollector(pos);
                case ActorType.QueryAgent: return new QueryAgent(pos);
                case ActorType.KernelWorker: return new KernelWorker(pos);
                case ActorType.Feature: return new Feature(pos);
                case ActorType.Glitch: return new Glitch(pos);
                default:
                    throw new NotImplementedException($"Actor Type {actorType:G} is not currently supported");
            }
        }
    }
}