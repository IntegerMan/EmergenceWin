using System;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;

namespace MattEland.Emergence.Engine.Level
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

        Guid Id { get; set; }

        GameObjectType ObjectType { get; }
        Pos2D Pos { get; set; }
        int Stability { get; set; }

        bool IsDead { get; }

        decimal EffectiveStrength { get; set; }
        decimal EffectiveDefense { get; set; }
        decimal EffectiveAccuracy { get; set; }
        decimal EffectiveEvasion { get; set; }

        bool BlocksSight { get; }
        int ZIndex { get; }

        GameObjectDto BuildDto();
        bool OnActorAttemptedEnter(CommandContext context, IActor actor);
        void SetInvulnerable();
        void OnCaptured(CommandContext context, IGameObject newOwner, Alignment oldTeam);

        void OnDestroyed(CommandContext context, IGameObject attacker);
        void MaintainActiveEffects(CommandContext context);
        void ApplyActiveEffects(CommandContext context);

        string State { get; set; }
        int Corruption { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not this object can be corrupted.
        /// </summary>
        bool IsCorruptable { get; }

        bool IsCorrupted { get; }
        bool IsInteractive { get; }

        char AsciiChar { get; }

        string ForegroundColor { get; }
        string BackgroundColor { get; }

        void ApplyCorruptionDamage(CommandContext context, IGameObject source, int damage);
    }
}