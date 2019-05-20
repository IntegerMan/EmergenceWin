using System;
using JetBrains.Annotations;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;
using MattEland.Emergence.Definitions.Services;
using ICommandContext = MattEland.Emergence.Definitions.Services.ICommandContext;

namespace MattEland.Emergence.Definitions.Entities
{
    /// <summary>
    /// Represents an object within the game world that can either move from cell to cell or can be modified in some capacity over time
    /// </summary>
    public abstract class GameObjectBase : IGameObject
    {
        private string _name;
        private int _corruption;
        private Alignment _team;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObjectBase"/> class.
        /// </summary>
        /// <param name="dto">The Data Transmission Object containing object details</param>
        protected GameObjectBase(GameObjectDto dto)
        {
            ObjectType = dto.Type;
            ObjectId = dto.ObjectId;
            Pos = Pos2D.FromString(dto.Pos);
            Stability = dto.MaxHP - dto.HPUsed;
            MaxStability = dto.MaxHP;
            Name = dto.Name;
            Team = dto.Team;
            State = dto.State;
            IsHidden = dto.IsHidden;
            Corruption = dto.Corruption;
        }

        /// <summary>
        /// Represents whether or not the object should be explicitly hidden on the client-side
        /// </summary>
        public bool IsHidden { get; set; }

        /// <summary>
        /// Gets the type of the game object.
        /// </summary>
        /// <value>The type of the object.</value>
        public GameObjectType ObjectType { get; }

        /// <summary>
        /// Gets or sets the object identifier.
        /// </summary>
        /// <value>The object identifier.</value>
        public string ObjectId { get; set; }

        /// <summary>
        /// Gets the position of the object within the game world.
        /// </summary>
        /// <value>The position of the object.</value>
        public Pos2D Pos { get; set; }

        /// <summary>
        /// Gets or sets the stability or health of the object.
        /// </summary>
        /// <remarks>
        /// <see cref="int.MaxValue"/> in this field represents an invulnerable object.
        /// </remarks>
        /// <value>The stability of the object.</value>
        public int Stability { get; set; }

        /// <summary>
        /// Gets or sets the maximum stability or health of the object.
        /// </summary>
        /// <value>The maximum stability of the object.</value>
        public int MaxStability { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is invulnerable.
        /// </summary>
        /// <value><c>true</c> if this instance is invulnerable; otherwise, <c>false</c>.</value>
        public virtual bool IsInvulnerable => false;
        public virtual bool IsTargetable => true;

        /// <summary>
        /// Gets whether or not this object has AI behind it.
        /// </summary>
        public virtual bool HasAI => false;

        /// <summary>
        /// Sets the object to be in an invulnerable state.
        /// </summary>
        public void SetInvulnerable()
        {
            Stability = int.MaxValue;
        }

        /// <summary>
        /// Called when an actor attempts to enter the cell.
        /// </summary>
        /// <param name="context">The command context.</param>
        /// <param name="actor">The actor entering the cell.</param>
        /// <param name="cell">The cell.</param>
        /// <returns><c>true</c> if the action is allowable, <c>false</c> if the action was handled and should be prevented.</returns>
        public virtual bool OnActorAttemptedEnter(ICommandContext context, IActor actor, IGameCell cell)
        {
            return true;
        }

        /// <summary>
        /// Gets or sets the name of the object.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get => CustomName;
            set => _name = value;
        }

        protected virtual string CustomName => _name;

        /// <summary>
        /// Gets a value indicating whether this actor is dead.
        /// </summary>
        /// <value><c>true</c> if this actor is dead; otherwise, <c>false</c>.</value>
        public bool IsDead => Stability <= 0;

        public virtual decimal EffectiveStrength { get; set; }
        public virtual decimal EffectiveDefense { get; set; }
        public virtual decimal EffectiveAccuracy { get; set; }
        public virtual decimal EffectiveEvasion { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not the object represents the player.
        /// </summary>
        public virtual bool IsPlayer => false;

        public virtual bool BlocksSight => false;

        /// <summary>
        /// Builds a data transmission object from this object.
        /// </summary>
        /// <returns>A data transmission object.</returns>
        public GameObjectDto BuildDto()
        {
            var dto = CreateDto();
            ConfigureDto(dto);
            return dto;
        }

        /// <summary>
        /// Represents which team the object is currently on. Certain actions can change which team an actor is on.
        /// </summary>
        public Alignment Team
        {
            get => IsCorrupted ? Alignment.Bug : _team;
            set => _team = value;
        }

        public Alignment ActualTeam
        {
            get => _team;
            set => _team = value;
        }

        public bool IsCorrupted => Corruption > 1;
        public virtual bool IsInteractive => false;
        public abstract char AsciiChar { get; }
        public virtual string ForegroundColor => GameColors.LightGray;
        public virtual string BackgroundColor => GameColors.Black;

        /// <summary>
        /// Creates an empty data transmission object to represent this instance.
        /// </summary>
        /// <returns>An empty data transmission object.</returns>
        protected virtual GameObjectDto CreateDto()
        {
            return new GameObjectDto();
        }

        /// <summary>
        /// Configures a data transmission object based on the properties of this object.
        /// </summary>
        /// <param name="dto">The data transmission object.</param>
        protected virtual void ConfigureDto(GameObjectDto dto)
        {
            dto.Pos = Pos.SerializedValue;
            dto.ObjectId = ObjectId;
            dto.Type = ObjectType;
            dto.HPUsed = MaxStability - Stability;
            dto.MaxHP = MaxStability;
            dto.Name = Name;
            dto.Team = _team; // Important to use field - don't want to lose track of it in round-tripping
            dto.State = State;
            dto.IsHidden = IsHidden;
            dto.Corruption = Corruption;
        }

        /// <summary>
        /// Gets a value indicating whether or not this object can be corrupted.
        /// </summary>
        public virtual bool IsCorruptable => true;

        public virtual void ApplyCorruptionDamage(ICommandContext context, [CanBeNull] IGameObject source, int damage)
        {
            Corruption += damage;
        }

        public abstract void OnInteract(CommandContext context, IActor actor);

        /// <summary>
        /// Gets or sets the corruption amount present on the object.
        /// </summary>
        public virtual int Corruption
        {
            get => _corruption;
            set => _corruption = Math.Max(Math.Min(3, value), 0);
        }

        public virtual bool IsCapturable => false;

        public virtual void OnCaptured(ICommandContext context, IGameObject executor, Alignment oldTeam)
        {
            if (executor.IsPlayer || context.CanPlayerSee(executor.Pos) || context.CanPlayerSee(Pos))
            {
                context.AddMessage($"{Name} is now under the control of {executor.Name}", ClientMessageType.Success);
            }
        }

        public virtual void OnDestroyed(ICommandContext context, IGameObject attacker)
        {
            var debris = CreationService.CreateObject(ObjectId, GameObjectType.Debris, Pos);

            context.Level.AddObject(debris);
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public virtual void MaintainActiveEffects(ICommandContext context)
        {
            // Do this when actors executing commands becomes a thing
        }

        public virtual void ApplyActiveEffects(ICommandContext context)
        {
            // Do this when actors executing commands becomes a thing            
        }

        public virtual int ZIndex => 5;

        public string State { get; set; }
    }
}