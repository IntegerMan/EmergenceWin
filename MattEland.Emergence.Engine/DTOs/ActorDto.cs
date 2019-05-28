using System.Collections.Generic;
using System.Diagnostics;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.DTOs
{
    /// <summary>
    /// A data transmission object representing an Actor in the game world.
    /// </summary>
    [DebuggerDisplay("(Actor: Pos:{Pos} Type:{Type} Id:{ObjectId} Damage:{HpUsed} OP Spent:{OpUsed})")]
    public class ActorDto : GameObjectDto
    {
        public ActorDto(ActorType actorType)
        {
            ActorType = actorType;
        }

        /// <summary>
        /// Gets or sets the ops points lost from the object's maximum ops. Typically this will be 0 until something uses operations,
        /// and we don't serialize 0 values, so this is a minor performance hack.
        /// </summary>
        public int OpUsed { get; set; }

        /// <summary>
        /// The maximum number of operations points the actor can store.
        /// </summary>
        public int MaxOp { get; set; }

        /// <summary>
        /// The number of kills the actor has accumulated.
        /// </summary>
        public int KillCount { get; set; }

        /// <summary>
        /// Indicates whether or not the object blocks line of sight.
        /// </summary>
        public bool BlocksSight { get; set; }

        /// <summary>
        /// Gets or sets the actor's accuracy (ability to hit evasive targets)
        /// </summary>
        public int Accuracy { get; set; }

        /// <summary>
        /// Gets or sets the actor's evasive capabilities.
        /// </summary>
        public int Evasion { get; set; }

        /// <summary>
        /// Gets or sets the actor's offensive strength.
        /// </summary>
        public int Strength { get; set; }

        /// <summary>
        /// Gets or sets the actor's defensive strength.
        /// </summary>
        public int Defense { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not this instance can move.
        /// </summary>
        public bool IsImmobile { get; set; }

        /// <summary>
        /// Gets or sets a collection of cell positions that are currently visible for this actor.
        /// </summary>
        public IEnumerable<string> Visible { get; set; }

        /// <summary>
        /// Gets or sets a collection of cell positions that this actor has seen.
        /// </summary>
        public IEnumerable<string> Known { get; set; }

        public decimal LineOfSightRadius { get; set; }
        public Rarity LootRarity { get; set; }
        public int DamageDealt { get; set; }
        public int DamageReceived { get; set; }
        public int CoresCaptured { get; set; }
        public string LastPos { get; set; }
        public ActorType ActorType { get; set; }
    }
}