using System;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Entities.Obstacles;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities
{
    /// <summary>
    /// Represents an object within the game world that can either move from cell to cell or can be modified in some capacity over time
    /// </summary>
    public abstract class GameObjectBase
    {
        private int _corruption;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObjectBase"/> class.
        /// </summary>
        protected GameObjectBase(Pos2D pos)
        {
            Pos = pos;

            Initialize();
        }

        /// <summary>
        /// Represents whether or not the object should be explicitly hidden on the client-side
        /// </summary>
        public virtual bool IsHidden => false;

        /// <summary>
        /// Gets the type of the game object.
        /// </summary>
        /// <value>The type of the object.</value>
        public abstract GameObjectType ObjectType { get; }

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
        public int MaxStability { get; set; } = 3;

        /// <summary>
        /// Gets a value indicating whether this instance is invulnerable.
        /// </summary>
        /// <value><c>true</c> if this instance is invulnerable; otherwise, <c>false</c>.</value>
        public virtual bool IsInvulnerable => false;
        public virtual bool IsTargetable => true;

        /// <summary>
        /// Sets the object to be in an invulnerable state.
        /// </summary>
        public void SetInvulnerable() => Stability = int.MaxValue;

        /// <summary>
        /// Called when an actor attempts to enter the cell.
        /// </summary>
        /// <param name="context">The command context.</param>
        /// <param name="actor">The actor entering the cell.</param>
        /// <returns><c>true</c> if the action is allowable, <c>false</c> if the action was handled and should be prevented.</returns>
        public virtual bool OnActorAttemptedEnter(GameContext context, Actor actor) => true;

        /// <summary>
        /// Gets the name of the object.
        /// </summary>
        /// <value>The name.</value>
        public abstract string Name { get; }

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
        /// Represents which team the object is currently on. Certain actions can change which team an actor is on.
        /// </summary>
        public Alignment Team
        {
            get => IsCorrupted ? Alignment.Bug : ActualTeam;
            set => ActualTeam = value;
        }

        public Alignment ActualTeam { get; set; }

        public bool IsCorrupted => Corruption > 1;
        public abstract char AsciiChar { get; }
        public virtual string ForegroundColor => GameColors.LightGray;
        public virtual string BackgroundColor => GameColors.Black;

        /// <summary>
        /// Gets a value indicating whether or not this object can be corrupted.
        /// </summary>
        public virtual bool IsCorruptable => true;

        public virtual void ApplyCorruptionDamage(GameContext context, [CanBeNull] GameObjectBase source, int damage)
        {
            if (IsCorruptable)
            {
                Corruption += damage;
            }
        }

        /// <summary>
        /// Gets or sets the corruption amount present on the object.
        /// </summary>
        public int Corruption
        {
            get => _corruption;
            set => _corruption = Math.Max(Math.Min(3, value), 0);
        }

        public virtual bool CanBeCaptured => false;

        public virtual void OnCaptured(GameContext context, GameObjectBase executor, Alignment oldTeam)
        {
            if (executor.IsPlayer || context.CanPlayerSee(executor.Pos) || context.CanPlayerSee(Pos))
            {
                context.AddMessage($"{Name} is now under the control of {executor.Name}", ClientMessageType.Success);
            }
        }

        public virtual void OnDestroyed(GameContext context, GameObjectBase attacker)
        {
            var debris = new Debris(Pos);

            context.AddObject(debris);
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public virtual void MaintainActiveEffects(GameContext context)
        {
            // Do this when actors executing commands becomes a thing
        }

        public virtual void ApplyActiveEffects(GameContext context)
        {
            // Do this when actors executing commands becomes a thing            
        }

        public virtual int ZIndex => 5;

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

        public virtual void Initialize()
        {
            Stability = MaxStability;
        }
    }
}