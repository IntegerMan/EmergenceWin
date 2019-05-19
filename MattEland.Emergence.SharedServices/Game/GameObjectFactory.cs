using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;
using MattEland.Emergence.EntityLogic;
using MattEland.Emergence.LevelGeneration.Encounters;

namespace MattEland.Emergence.Services.Game
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
        public static GameObjectBase CreateFromDto(GameObjectDto dto)
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
                    switch (dto.Id)
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
                            throw new ArgumentOutOfRangeException($"No pickup handler present for ID {dto.Id}");
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
            switch (dto.Id)
            {
                case "ACTOR_ANTI_VIRUS":
                   return new AntiVirus(dto);

                case "ACTOR_VIRUS":
                   return new Virus(dto);

                case "ACTOR_WORM":
                   return new Worm(dto);

                case "ACTOR_TURRET":
                   return new Turret(dto);

                case "ACTOR_LOGIC_BOMB":
                   return new LogicBomb(dto);

                case "ACTOR_BUG":
                case "ACTOR_FEATURE":
                case "ACTOR_GLITCH":
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
        public static IPlayer CreatePlayer([NotNull] string playerId)
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

            return (IPlayer)CreateFromDto(dto);
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
            dto.Id = definition.Id;
            dto.Type = objectType;
            dto.Pos = position.SerializedValue;

            // Copy over basic stats
            dto.MaxHP = definition.HP;
            dto.HPUsed = 0;
            dto.MaxOP = definition.OP;
            dto.OPUsed = 0;
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

        public static GameObjectBase CreateFromObjectType(string id, GameObjectType objectType, Pos2D position)
        {

            // Figure out which DTO to build
            GameObjectDto dto;
            switch (objectType)
            {
                case GameObjectType.Player:
                    dto = new PlayerDto();
                    break;

                case GameObjectType.Turret:
                    return CreateFromDto(SetEntityStats(new ActorDto(), "ACTOR_TURRET", objectType, position));

                case GameObjectType.Core:
                    return CreateFromDto(SetEntityStats(new ActorDto(), "ACTOR_CORE", objectType, position));

                case GameObjectType.Actor:
                    return CreateFromDto(SetEntityStats(new ActorDto(), id, objectType, position));

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
            dto.Id = id;
            dto.Pos = position.SerializedValue;
            dto.HPUsed = 0;
            dto.MaxHP = 10;

            return CreateFromDto(dto);
        }

        public static IGameObject CreateWall(Pos2D pos, bool isExterior)
        {
            var hp = 3;
            var dto = new GameObjectDto
            {
                Pos = pos.SerializedValue,
                Type = GameObjectType.Wall,
                MaxHP = hp,
                HPUsed = 0,
                Id = null,
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