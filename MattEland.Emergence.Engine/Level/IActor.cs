using System.Collections.Generic;
using MattEland.Emergence.Engine.Commands;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Level
{
    public interface IActor : IGameObject
    {
        int Accuracy { get; set;  }
        ActorType ActorType { get; }
        int Defense { get; set; }
        int Evasion { get; set; }
        bool IsImmobile { get; set; }
        int MaxOperations { get; set; }
        int Operations { get; set; }
        int Strength { get; set; }
        decimal EffectiveLineOfSightRadius { get; set; }
        ISet<Pos2D> VisibleCells { get; set; }
        decimal LineOfSightRadius { get; set; }
        DamageType AttackDamageType { get; }
        Alignment ActualTeam { get; set; }

        /// <summary>
        /// Gets or sets the number of kills the actor is responsible for.
        /// </summary>
        /// <value>The kill count.</value>
        int KillCount { get; set; }

        bool AdjustStability(int amountToAdd);
        bool AdjustOperationsPoints(int amountToAdd);

        bool IsCommandActive(IGameCommand command);
        void SetCommandActiveState(IGameCommand command, bool isActive);
        void MarkCellsAsKnown(IEnumerable<Pos2D> cells);
        bool CanSee(Pos2D pos);
        void CopyCellCollectionsToDto(ActorDto actorDto, ILevel level);
        void CopyCellCollectionsFromDto(ActorDto dto, ILevel level);
        void OnWaited(CommandContext context);

        int CoresCaptured { get; set; }
        int DamageDealt { get; set; }
        int DamageReceived { get; set; }
        IList<Pos2D> RecentPositions { get; }
        ISet<Pos2D> KnownCells { get; set; }
    }

}