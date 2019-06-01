using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Commands;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Entities.Items;
using MattEland.Emergence.Engine.Entities.Obstacles;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Level.Generation.Encounters;

namespace MattEland.Emergence.Engine.Game
{
    public static class GameObjectFactory
    {
        private static EntityDefinitionService _entityService;

        private static EntityDefinitionService EntityService => _entityService ?? (_entityService = new EntityDefinitionService());


        /// <summary>
        /// Creates a player object instance with stats from the defined <paramref name="playerId"/>.
        /// </summary>
        /// <returns>The player instance</returns>
        public static Player CreatePlayer(Pos2D pos, ActorType playerType)
        {
            return new Player(pos, playerType);
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

        public static GameObjectBase CreateObject(string id, GameObjectType objType, Pos2D pos, Action<GameObjectDto> configure = null)
        {
            // Figure out which DTO to build
            GameObjectDto dto;
            switch (objType)
            {
                case GameObjectType.Player:
                    dto = new PlayerDto();
                    break;

                case GameObjectType.Turret:
                    return CreateFromDto(SetEntityStats(new ActorDto(ActorType.Turret), Actors.Turret, objType, pos));

                case GameObjectType.Core:
                    return CreateCore(pos);

                case GameObjectType.Actor:
                    return CreateFromDto(SetEntityStats(new ActorDto(GetActorType(id)), id, objType, pos));

                case GameObjectType.Door:
                case GameObjectType.Treasure:
                    dto = new OpenableDto();
                    break;

                default:
                    dto = new GameObjectDto();
                    break;
            }

            // Set common properties on dto
            dto.Type = objType;
            dto.ObjectId = id;
            dto.Pos = pos;
            dto.HpUsed = 0;
            dto.MaxHp = 10;

            configure?.Invoke(dto);

            return CreateFromDto(dto);
        }


        public static GameObjectBase CreateWall(Pos2D pos, bool isExterior)
        {
            var hp = 3;
            var dto = new GameObjectDto
            {
                Pos = pos,
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

        public static GameObjectBase CreateCore(Pos2D pos) 
            => CreateFromDto(SetEntityStats(new ActorDto(ActorType.Core), Actors.Core, GameObjectType.Core, pos));

        public static GameObjectBase CreateActor(string id, Pos2D pos)
        {
            ActorType actorType = GetActorType(id);
            switch (actorType)
            {
                case ActorType.AntiVirus: return new AntiVirus(pos);
                case ActorType.LogicBomb: return new LogicBomb(pos);
                case ActorType.Turret: return new Turret(pos);
                case ActorType.Core: return new LevelCore(pos);
                case ActorType.Player: return new Player(pos, ActorType.Player);
                case ActorType.Bug: return new Bug(pos);
                case ActorType.Virus: return new Virus(pos);
                case ActorType.Worm: return new Worm(pos);

                case ActorType.Bit:
                case ActorType.Daemon:
                case ActorType.SystemDefender:
                case ActorType.Inspector:
                case ActorType.SecurityAgent:
                case ActorType.GarbageCollector:
                case ActorType.Helpy:
                case ActorType.QueryAgent:
                case ActorType.KernelWorker:
                case ActorType.Feature: 
                case ActorType.Glitch:
                default:
                    throw new NotImplementedException($"Actor Type {actorType:G} is not currently supported");
            }
        }
    }
}