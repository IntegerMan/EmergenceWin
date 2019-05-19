﻿using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Definitions.Level
{
    public interface IGameObject
    {
        string Name { get; set; }
        bool IsPlayer { get; }
        Alignment Team { get; set; }
        bool IsCapturable { get; }

        bool HasAI { get; }
        bool IsInvulnerable { get; }
        bool IsTargetable { get; }
        int MaxStability { get; set; }
        string ObjectId { get; set; }
        GameObjectType ObjectType { get; }
        Pos2D Position { get; set; }
        int Stability { get; set; }

        bool IsDead { get; }

        decimal EffectiveStrength { get; set; }
        decimal EffectiveDefense { get; set; }
        decimal EffectiveAccuracy { get; set; }
        decimal EffectiveEvasion { get; set; }

        bool BlocksSight { get; }
        int ZIndex { get; }

        GameObjectDto BuildDto();
        bool OnActorAttemptedEnter(ICommandContext context, IActor actor, IGameCell cell);
        void SetInvulnerable();
        void OnCaptured(ICommandContext context, IGameObject newOwner, Alignment oldTeam);

        void OnDestroyed(ICommandContext context, IGameObject attacker);
        void MaintainActiveEffects(ICommandContext context);
        void ApplyActiveEffects(ICommandContext context);

        string State { get; set; }
        int Corruption { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not this object can be corrupted.
        /// </summary>
        bool IsCorruptable { get; }

        bool IsCorrupted { get; }
        bool IsInteractive { get; }

        void ApplyCorruptionDamage(ICommandContext context, IGameObject source, int damage);
    }
}