using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Level.Generation.Encounters;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Game
{
    public static class GameObjectFactory
    {
        private static EntityDefinitionService _entityService;

        private static EntityDefinitionService EntityService => _entityService ?? (_entityService = new EntityDefinitionService());

        /// <summary>
        /// Creates a level object from a data transmission object.
        /// </summary>
        /// <param name="dto">The data transmission object.</param>
        /// <returns>The constructed level object.</returns>
        /// <exception cref="ArgumentException">Thrown if the algorithm does not support the type of object that <paramref name="dto"/> is.</exception>
        private static GameObjectBase CreateFromDto(GameObjectDto dto)
        {
            switch (dto.Type)
            {
                case GameObjectType.Player:
                    return new Player((PlayerDto)dto);

                case GameObjectType.Core:
                    return new LevelCore((ActorDto)dto);

                case GameObjectType.Turret:
                case GameObjectType.Actor:
                    return BuildActor((ActorDto) dto);

                case GameObjectType.Wall:
                    return new Wall(dto);

                case GameObjectType.Door:
                    return new Door((OpenableDto)dto);

                case GameObjectType.Cabling:
                    return new Cabling(dto);

                case GameObjectType.Water:
                    return new Water(dto);

                case GameObjectType.Firewall:
                    return new Firewall(dto);

                case GameObjectType.Entrance:
                    return new LevelEntrance(dto);

                case GameObjectType.Exit:
                    return new LevelExit(dto);

                case GameObjectType.Service:
                    return new LevelService(dto);

                case GameObjectType.DataStore:
                    return new DataStore(dto);

                case GameObjectType.Divider:
                    return new Divider(dto);

                case GameObjectType.Debris:
                    return new Debris(dto);

                case GameObjectType.CommandPickup:
                    return new CommandPickup(dto);

                case GameObjectType.GenericPickup:
                    switch (dto.ObjectId)
                    {
                        case "GET_HP":
                            return new StabilityPickup(dto);

                        case "GET_MAXHP":
                            return new MaxStabilityPickup(dto);

                        case "GET_OPS":
                            return new OperationsPickup(dto);

                        case "GET_MAXOPS":
                            return new MaxOperationsPickup(dto);

                        default:
                            throw new ArgumentOutOfRangeException($"No pickup handler present for ID {dto.ObjectId}");
                    }

                case GameObjectType.Treasure:
                    return new TreasureTrove((OpenableDto)dto);

                case GameObjectType.Help:
                    return new HelpTile(dto);

                case GameObjectType.CharacterSelect:
                    return new CharacterSelectTile(dto);

                default:
                    throw new ArgumentException($"Unsupported object type {dto.Type}", nameof(dto));
            }
        }

        private static GameObjectBase BuildActor(ActorDto dto)
        {
            switch (dto.ObjectId)
            {
                case Actors.AntiVirus:
                   return new AntiVirus(dto);

                case Actors.Virus:
                   return new Virus(dto);

                case Actors.Worm:
                   return new Worm(dto);

                case Actors.Turret:
                   return new Turret(dto);

                case Actors.LogicBomb:
                   return new LogicBomb(dto);

                case Actors.Bug:
                case Actors.Feature:
                case Actors.Glitch:
                    return new Bug(dto);

                default:
                    return new Actor(dto);
            }
        }

        /// <summary>
        /// Creates a player object instance with stats from the defined <paramref name="playerId"/>.
        /// </summary>
        /// <param name="playerId">The player identifier. This cannot be null or empty.</param>
        /// <returns>The player instance</returns>
        public static Player CreatePlayer([NotNull] string playerId)
        {
            if (string.IsNullOrWhiteSpace(playerId))
            {
                throw new ArgumentException("playerId is required", nameof(playerId));
            }

            var dto = new PlayerDto();

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
                    var commandDef = CreationService.CreateCommand(command);

                    commandInfoDto = commandDef?.BuildDto(false);
                }

                dto.Hotbar[commandIndex++] = commandInfoDto;
            }

            return (Player)CreateFromDto(dto);
        }

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
            dto.Pos = position.SerializedValue;

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

        public static GameObjectBase CreateFromObjectType(string id, GameObjectType objectType, Pos2D position, Action<GameObjectDto> configure = null)
        {

            // Figure out which DTO to build
            GameObjectDto dto;
            switch (objectType)
            {
                case GameObjectType.Player:
                    dto = new PlayerDto();
                    break;

                case GameObjectType.Turret:
                    return CreateFromDto(SetEntityStats(new ActorDto(ActorType.Turret), Actors.Turret, objectType, position));

                case GameObjectType.Core:
                    return CreateFromDto(SetEntityStats(new ActorDto(ActorType.Core), Actors.Core, objectType, position));

                case GameObjectType.Actor:
                    return CreateFromDto(SetEntityStats(new ActorDto(GetActorType(id)), id, objectType, position));

                case GameObjectType.Door:
                case GameObjectType.Treasure:
                    dto = new OpenableDto();
                    break;

                default:
                    dto = new GameObjectDto();
                    break;
            }

            // Set common properties on dto
            dto.Type = objectType;
            dto.ObjectId = id;
            dto.Pos = position.SerializedValue;
            dto.HpUsed = 0;
            dto.MaxHp = 10;

            configure?.Invoke(dto);

            return CreateFromDto(dto);
        }

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

                case Actors.PlayerAntiVirus:
                case Actors.PlayerDebugger:
                case Actors.PlayerLogistics:
                case Actors.PlayerSearch:
                case Actors.PlayerMalware:
                case Actors.PlayerForecast:
                case Actors.PlayerGame: 
                    return ActorType.Player;

                default: throw new NotSupportedException($"ActorType mapping not found for actor {id}");
            }
        }

        public static GameObjectBase CreateWall(Pos2D pos, bool isExterior)
        {
            var hp = 3;
            var dto = new GameObjectDto
            {
                Pos = pos.SerializedValue,
                Type = GameObjectType.Wall,
                MaxHp = hp,
                HpUsed = 0,
                ObjectId = null,
                Team = Alignment.Neutral,
                Name = "Wall",
                State = isExterior ? "External" : null
            };
            var wall = new Wall(dto);

            if (isExterior)
            {
                wall.SetInvulnerable();
            }

            return wall;
        }
    }
}