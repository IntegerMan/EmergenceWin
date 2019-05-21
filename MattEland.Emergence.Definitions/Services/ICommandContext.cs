using System;
using System.Collections.Generic;
using GeneticSharp.Domain.Randomizations;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Effects;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model.Messages;

namespace MattEland.Emergence.Definitions.Services
{
    public interface ICommandContext
    {
        IGameService GameService { get; }
        ILevel Level { get; }
        IPlayer Player { get; }
        ICombatManager CombatManager { get; }
        ILootProvider LootProvider { get; }
        IEnumerable<EffectBase> Effects { get; }

        IEnumerable<GameMessage> Messages {get;}
        IRandomization Randomizer { get; set; }
        event EventHandler<ActorDamagedEventArgs> OnActorHurt;

        void AddMessage(string message, ClientMessageType messageType);

        /// <summary>
        /// Moves the player to the next available level, or wins the game if all levels have been played.
        /// </summary>
        void AdvanceToNextLevel();

        /// <summary>
        /// Sets the current level to <paramref name="level"/>
        /// </summary>
        /// <param name="level">The new level.</param>
        void SetLevel(ILevel level);
        void AddError(string message);

        /// <summary>
        /// Teleports <paramref name="actor"/> to the targeted <paramref name="pos"/>, swapping actor positions and applying teleport damage as necessary.
        /// </summary>
        /// <param name="actor">The actor moving</param>
        /// <param name="pos">The target position</param>
        void TeleportActor(IActor actor, Pos2D pos);

        IEnumerable<IGameCell> GetCellsVisibleFromPoint(Pos2D point, decimal radius);
        void HandleObjectKilled(IGameObject defender, IGameObject attacker);

        /// <summary>
        /// Displays help for the specified <paramref name="helpTopic"/>
        /// </summary>
        /// <param name="helpTopic">The topic to display help on</param>
        void DisplayHelp(IGameObject source, string helpTopic);

        void ReplacePlayer(IPlayer player, Pos2D position);
        void CalculateLineOfSight(IActor actor);

        void AddEffect(EffectBase effect);

        bool CanPlayerSee(IGameObject obj);
        bool CanPlayerSee(Pos2D pos);
        ICommandContext Clone();
        void PreviewObjectHurt(IGameObject attacker, IGameObject defender, int damage, DamageType damageType);
    }
}