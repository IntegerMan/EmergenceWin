﻿using System;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Commands;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Services
{
    public static class CreationService
    {
        public static Player CreatePlayer([NotNull] string playerId)
        {
            if (PlayerCreationFunction == null)
            {
                throw new InvalidOperationException("PlayerCreationFunction was not configured.");
            }

            return PlayerCreationFunction(playerId);
        }

        public static LevelData CreateLevel(LevelType levelId, string levelName, Pos2D playerStart)
        {
            if (LevelCreationFunction == null)
            {
                throw new InvalidOperationException("LevelCreationFunction was not configured.");
            }

            return LevelCreationFunction(levelId, levelName, playerStart);
        }

        public static GameObjectBase CreateWall(Pos2D pos, bool isExterior)
        {
            if (WallCreationFunction == null)
            {
                throw new InvalidOperationException("WallCreationFunction was not configured.");
            }

            return WallCreationFunction(pos, isExterior);
        }

        public static GameObjectBase CreateObject(string id, GameObjectType objType, Pos2D pos)
        {
            if (ObjectCreationFunction == null)
            {
                throw new InvalidOperationException("ObjectCreationFunction was not configured.");
            }

            return ObjectCreationFunction(id, objType, pos);
        }


        public static IGameCommand CreateCommand(string commandId)
        {
            if (CommandCreationFunction == null)
            {
                throw new InvalidOperationException("CommandCreationFunction was not configured.");
            }

            return CommandCreationFunction(commandId);
        }

        public static ICommandInstance CreateCommandReference(CommandInfoDto dto)
        {
            if (CommandReferenceCreationFunction == null)
            {
                throw new InvalidOperationException("CommandReferenceCreationFunction was not configured.");
            }

            return CommandReferenceCreationFunction(dto);
        }

        public static Func<string, Player> PlayerCreationFunction { get; set; }
        public static Func<LevelType, string, Pos2D, LevelData> LevelCreationFunction { get; set; }
        public static Func<Pos2D, bool, GameObjectBase> WallCreationFunction { get; set; }
        public static Func<string, GameObjectType, Pos2D, GameObjectBase> ObjectCreationFunction { get; set; }
        public static Func<string, IGameCommand> CommandCreationFunction { get; set; }
        public static Func<CommandInfoDto, ICommandInstance> CommandReferenceCreationFunction { get; set; }

    }
}