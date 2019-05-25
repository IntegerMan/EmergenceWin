using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public IList<Pos2D> RecentPositions { get; } = new List<Pos2D>();

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
            ActorType = GetActorType(dto.ObjectId);

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

        private static HashSet<Pos2D> ConvertStringCollectionToPointCollection(IEnumerable<string> rows, LevelData level)
        {
            var set = new HashSet<Pos2D>();

            if (rows != null)
            {
                int y = level.UpperLeft.Y;

                foreach (var row in rows)
                {
                    int x = level.UpperLeft.X;

                    foreach(var c in row)
                    {
                        if (c == '1')
                        {
                            set.Add(new Pos2D(x, y));
                        }

                        x++;
                    }

                    y++;

                }
            }

            return set;
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

        /// <summary>
        /// Gets the type of the actor from an actor <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The actor identifier.</param>
        /// <returns>The type of actor.</returns>
        protected static ActorType GetActorType(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("id cannot be null or whitespace", nameof(id));
            }

            switch (id)
            {
                case "ACTOR_BIT":
                    return ActorType.Bit;
                case "ACTOR_DAEMON":
                    return ActorType.Daemon;
                case "ACTOR_ANTI_VIRUS":
                    return ActorType.AntiVirus;
                case "ACTOR_DEFENDER":
                    return ActorType.SystemDefender;
                case "ACTOR_INSPECTOR":
                    return ActorType.Inspector;
                case "ACTOR_SEC_AGENT":
                    return ActorType.SecurityAgent;
                case "ACTOR_GARBAGE_COLLECTOR":
                    return ActorType.GarbageCollector;
                case "ACTOR_HELP":
                    return ActorType.Helpy;
                case "ACTOR_SEARCH":
                    return ActorType.QueryAgent;
                case "ACTOR_KERNEL_WORKER":
                    return ActorType.KernelWorker;
                case "ACTOR_LOGIC_BOMB":
                    return ActorType.LogicBomb;
                case "ACTOR_TURRET":
                    return ActorType.Turret;
                case "ACTOR_CORE":
                    return ActorType.Core;
                case "ACTOR_BUG":
                    return ActorType.Bug;
                case "ACTOR_WORM":
                    return ActorType.Worm;
                case "ACTOR_FEATURE":
                    return ActorType.Feature;
                case "ACTOR_GLITCH":
                    return ActorType.Glitch;
                case "ACTOR_VIRUS":
                    return ActorType.Virus;
                case "ACTOR_PLAYER_LOGISTICS":
                case "ACTOR_PLAYER_MALWARE":
                case "ACTOR_PLAYER_GAME":
                case "ACTOR_PLAYER_ANTIVIRUS":
                case "ACTOR_PLAYER_SEARCH":
                case "ACTOR_PLAYER_FORECAST":
                case "ACTOR_PLAYER_DEBUGGER":
                    return ActorType.Player;
                default:
                    throw new ArgumentOutOfRangeException(nameof(id), $"{id} is not supported for getting an actor type");
            }

        }

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(CommandContext context, Actor actor)
        {
            if (actor != this)
            {
                context.CombatManager.HandleAttack(context, actor, this, "attacks", actor.AttackDamageType);
            }

            return false;
        }

        public virtual DamageType AttackDamageType => DamageType.Normal;

        public override char AsciiChar
        {
            get
            {
                switch (ActorType)
                {
                    case ActorType.Bit:
                        return '1';
                    case ActorType.Daemon:
                        return 'd';
                    case ActorType.AntiVirus:
                        return 'V';
                    case ActorType.SystemDefender:
                        return 'D';
                    case ActorType.Inspector:
                        return 'i';
                    case ActorType.SecurityAgent:
                        return 's';
                    case ActorType.GarbageCollector:
                        return 'G';
                    case ActorType.Helpy:
                        return '?';
                    case ActorType.QueryAgent:
                        return 'q';
                    case ActorType.KernelWorker:
                        return 'k';
                    case ActorType.LogicBomb:
                        return 'l';
                    case ActorType.Turret:
                        return 'T';
                    case ActorType.Core:
                        return 'C';
                    case ActorType.Player:
                        return '@';
                    case ActorType.Bug:
                        return 'b';
                    case ActorType.Feature:
                        return 'f';
                    case ActorType.Virus:
                        return 'v';
                    case ActorType.Worm:
                        return 'w';
                    case ActorType.Glitch:
                        return 'g';
                    default:
                        return 'a';
                }
            }
        }

        /// <inheritdoc />
        protected override GameObjectDto CreateDto()
        {
            return new ActorDto();
        }

        /// <inheritdoc />
        protected override void ConfigureDto(GameObjectDto dto)
        {
            base.ConfigureDto(dto);

            var actorDto = (ActorDto)dto;
            actorDto.OpUsed = MaxOperations - Operations;
            actorDto.MaxOp = MaxOperations;
            actorDto.Accuracy = Accuracy;
            actorDto.Evasion = Evasion;
            actorDto.Strength = Strength;
            actorDto.Defense = Defense;
            actorDto.LineOfSightRadius = LineOfSightRadius;
            actorDto.BlocksSight = BlocksSight;
            actorDto.KillCount = KillCount;
            actorDto.IsImmobile = IsImmobile;
            actorDto.LootRarity = LootRarity;
            actorDto.DamageDealt = DamageDealt;
            actorDto.DamageReceived = DamageReceived;
            actorDto.CoresCaptured = CoresCaptured;
        }
        
        public void CopyCellCollectionsFromDto(ActorDto dto, LevelData level)
        {
            KnownCells = ConvertStringCollectionToPointCollection(dto.Known, level);
            VisibleCells = ConvertStringCollectionToPointCollection(dto.Visible, level);
        }

        public virtual void OnWaited(CommandContext context)
        {
        }

        public int CoresCaptured { get; set; }
        public int DamageDealt { get; set; }
        public int DamageReceived { get; set; }

        public void CopyCellCollectionsToDto(ActorDto actorDto, LevelData level)
        {
            if (VisibleCells != null && VisibleCells.Any() && PersistVisible)
            {
                actorDto.Visible = ConvertPosCollectionToStringCollection(VisibleCells, level);
            }

            if (KnownCells != null && KnownCells.Any() && PersistKnown)
            {
                actorDto.Known = ConvertPosCollectionToStringCollection(KnownCells, level);
            }
        }

        private static IEnumerable<string> ConvertPosCollectionToStringCollection(ISet<Pos2D> collection, LevelData level)
        {
            var rows = new List<string>();
            var sb = new StringBuilder();

            for (int y = level.UpperLeft.Y; y <= level.LowerRight.Y; y++)
            {
                sb.Clear();

                for (int x = level.UpperLeft.X; x <= level.LowerRight.X; x++)
                {
                    sb.Append(collection.Contains(new Pos2D(x, y)) ? "1" : "0");
                }

                rows.Add(sb.ToString());
            }

            return rows;
        }

        protected virtual bool PersistVisible => false;
        protected virtual bool PersistKnown => false;


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

        /// <summary>
        /// Increases the actor's stability points by the specified <paramref name="amountToAdd"/>.
        /// </summary>
        /// <param name="amountToAdd">The amount of stability to add.</param>
        public bool AdjustStability(int amountToAdd)
        {
            var oldStability = Stability;
            Stability = Math.Min(Stability + amountToAdd, MaxStability);

            return Stability > oldStability;
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

        public override string ForegroundColor {
            get
            {
                switch (Team)
                {
                    case Alignment.SystemAntiVirus:
                        return GameColors.Orange;
                    case Alignment.SystemSecurity:
                        return GameColors.Red;
                    case Alignment.Virus:
                    case Alignment.Bug:
                        return GameColors.Purple;
                    case Alignment.Player:
                        return GameColors.Green;
                    default:
                        return GameColors.Yellow;
                }
            }
        }

    }
}