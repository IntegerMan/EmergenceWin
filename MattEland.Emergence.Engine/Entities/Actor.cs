﻿using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Commands;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities
{
    /// <summary>
    /// Represents an actor within the game world. An actor is any sort of entity that has some form of agency - makes moves
    /// and/or decisions every turn and occupies a single space in the game world at any time.
    /// </summary>
    /// <seealso cref="T:MattEland.Emergence.Engine.Entities.GameObjectBase" />
    public class Actor : GameObjectBase
    {
        private int _operations;

        /// <summary>
        /// Gets the type of the actor.
        /// </summary>
        /// <value>The type of the actor.</value>
        public ActorType ActorType { get; }

        /// <summary>
        /// Gets or sets the operations of the object, used as energy for command management.
        /// </summary>
        /// <value>The operations of the object.</value>
        public int Operations
        {
            get => _operations;
            set => _operations = Math.Max(0, Math.Min(MaxOperations, value));
        }

        /// <summary>
        /// Gets or sets the maximum operations of the object, used as energy for command management.
        /// </summary>
        /// <value>The maximum operations of the object.</value>
        public int MaxOperations { get; set; }

        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Accuracy { get; set; }
        public int Evasion { get; set; }
        public decimal LineOfSightRadius { get; set; }

        public override bool HasAi => true;

        /// <summary>
        /// Gets or sets the number of kills the actor is responsible for.
        /// </summary>
        /// <value>The kill count.</value>
        public int KillCount { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Actor"/> class.
        /// </summary>
        public Actor(ActorDto dto) : base(dto)
        {
            ActorType = dto.ActorType;

            // Max should always be set before current
            MaxStability = dto.MaxHp;
            Stability = dto.MaxHp - dto.HpUsed;
            MaxOperations = dto.MaxOp;
            Operations = dto.MaxOp - dto.OpUsed;

            Defense = dto.Defense;
            Evasion = dto.Evasion;
            Strength = dto.Strength;
            Accuracy = dto.Accuracy;
            LineOfSightRadius = dto.LineOfSightRadius;
            IsImmobile = dto.IsImmobile;
            KillCount = dto.KillCount;
            LootRarity = dto.LootRarity;
            DamageDealt = dto.DamageDealt;
            DamageReceived = dto.DamageReceived;
            CoresCaptured = dto.CoresCaptured;

            ResetEffectiveValues();
        }

        public ISet<Pos2D> VisibleCells { get; set; }

        public ISet<Pos2D> KnownCells { get; set; }

        public void ClearKnownCells()
        {
            KnownCells?.Clear();
        }

        private void ResetEffectiveValues()
        {
            EffectiveDefense = Defense;
            EffectiveStrength = Strength;
            EffectiveEvasion = Evasion;
            EffectiveAccuracy = Accuracy;
            EffectiveLineOfSightRadius = LineOfSightRadius;
        }

        public virtual decimal EffectiveLineOfSightRadius { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not this object can move.
        /// </summary>
        public bool IsImmobile { get; set; }

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(CommandContext context, Actor actor)
        {
            if (actor == this) return false;

            context.CombatManager.HandleAttack(context, actor, this, "attacks", actor.AttackDamageType);

            return true;
        }

        public virtual DamageType AttackDamageType => DamageType.Normal;

        private static readonly IDictionary<ActorType, char> _actorCharacters = new Dictionary<ActorType, char>
        {
            [ActorType.Bit] = '1',
            [ActorType.Daemon] = 'd',
            [ActorType.AntiVirus] = 'V',
            [ActorType.SystemDefender] = 'D',
            [ActorType.Inspector] = 'i',
            [ActorType.SecurityAgent] = 's',
            [ActorType.GarbageCollector] = 'G',
            [ActorType.Helpy] = '?',
            [ActorType.QueryAgent] = 'q',
            [ActorType.KernelWorker] = 'k',
            [ActorType.LogicBomb] = 'l',
            [ActorType.Turret] = 'T',
            [ActorType.Core] = 'C',
            [ActorType.Player] = '@',
            [ActorType.Bug] = 'b',
            [ActorType.Feature] = 'f',
            [ActorType.Virus] = 'v',
            [ActorType.Worm] = 'w',
            [ActorType.Glitch] = 'g',
        };

        public override char AsciiChar => _actorCharacters.ContainsKey(ActorType) ? _actorCharacters[ActorType] : 'a';

        public virtual void OnWaited(CommandContext context)
        {
        }

        public int CoresCaptured { get; set; }
        public int DamageDealt { get; set; }
        public int DamageReceived { get; set; }

        public override bool CanBeCaptured => ActorType == ActorType.Core;

        /// <summary>
        /// Increases the actor's operations points by the specified <paramref name="amountToAdd"/>.
        /// </summary>
        /// <param name="amountToAdd">The amount of ops points to add.</param>
        public bool AdjustOperationsPoints(int amountToAdd)
        {
            var oldOps = Operations;
            Operations = Math.Min(Operations + amountToAdd, MaxOperations);
            return Operations > oldOps;
        }

        public virtual bool IsCommandActive(GameCommand command) => false;

        public override void OnDestroyed(CommandContext context, GameObjectBase attacker)
        {
            // Increment the kill count if the player just killed an actor
            if (attacker.IsPlayer)
            {
                context.Player.KillCount += 1;
            }

            // Kills should give the actor responsible an additional operations point
            if (attacker is Actor actor && actor.AdjustOperationsPoints(1) && attacker.IsPlayer)
            {
                context.AddEffect(new OpsChangedEffect(attacker, 1));
            }

            // If the player did this and loot can be a thing, roll for loot
            if (attacker.IsPlayer && LootRarity != Rarity.None)
            {
                context.LootProvider.SpawnLootAsNeeded(context, this, LootRarity);
            }
        }

        public Rarity LootRarity { get; set; }

        public virtual void SetCommandActiveState(GameCommand command, bool isActive)
        {

        }

        public void MarkCellsAsKnown(IEnumerable<Pos2D> cells)
        {
            if (KnownCells == null)
            {
                KnownCells = new HashSet<Pos2D>(cells);
            }
            else
            {
                foreach (var cell in cells)
                {
                    KnownCells.Add(cell);
                }
            }
        }

        public override void ApplyActiveEffects(CommandContext context)
        {
            base.ApplyActiveEffects(context);

            if (ObjectId == Actors.AntiVirus)
            {
                CorruptionHelper.CleanseNearby(context, this, Pos);
            }
        }

        public bool CanSee(Pos2D pos) => VisibleCells != null && VisibleCells.Any(c => c == pos);

        public override int ZIndex => 25;

        public override void ApplyCorruptionDamage(CommandContext context, [CanBeNull] GameObjectBase source, int damage)
        {
            base.ApplyCorruptionDamage(context, source, damage);

            // Only modify operations in the negative manner. -corruption damage is used for anti-viruses
            if (Operations > 0 && damage > 0)
            {
                if (context.CanPlayerSee(Pos))
                {
                    context.AddEffect(new OpsChangedEffect(this, -damage));
                }

                Operations -= damage;
            }
        }

        private static readonly IDictionary<Alignment, string> _teamColors = new Dictionary<Alignment, string>
        {
            [Alignment.SystemSecurity] = GameColors.Red,
            [Alignment.SystemAntiVirus] = GameColors.Orange,
            [Alignment.Player] = GameColors.Green,
            [Alignment.Bug] = GameColors.Purple,
            [Alignment.Virus] = GameColors.Purple
        };

        public override string ForegroundColor => _teamColors.ContainsKey(Team) ? _teamColors[Team] : GameColors.Yellow;
    }
}