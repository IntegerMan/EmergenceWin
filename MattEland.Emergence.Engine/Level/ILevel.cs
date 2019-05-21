using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.DTOs;

namespace MattEland.Emergence.Engine.Level
{
    public interface ILevel
    {
        ICollection<IGameCell> Cells { get; set; }
        IEnumerable<IActor> Cores { get; }
        IEnumerable<IActor> Actors { get; }
        IEnumerable<IGameObject> Objects { get; }

        LevelType Id { get; set; }
        Pos2D LowerRight { get; set; }
        string Name { get; set; }
        Pos2D PlayerStart { get; set; }
        Pos2D UpperLeft { get; set; }
        bool HasAdminAccess { get; set; }

        /// <summary>
        /// Gets or sets the location marked by the Mark command within this level.
        /// If the Mark command has not been executed, this will be the level start.
        /// </summary>
        Pos2D MarkedPos { get; set; }


        void AddCell(IGameCell cell);
        void AddObject(IGameObject gameObject);
        IPlayer FindPlayer();

        [CanBeNull]
        IGameCell GetCell(Pos2D pos);
        void MoveObject(IGameObject obj, Pos2D newPos);
        void RemoveObject(IGameObject obj);
        LevelDto ToDto();
        bool IsPosExterior(Pos2D pos);
        IEnumerable<IGameObject> GetTargetsAtPos(Pos2D pos);

        IEnumerable<IGameCell> GetBorderCellsInSquare(Pos2D pos, int radius);
        IEnumerable<IGameCell> GetCellsInSquare(Pos2D pos, int radius);
        bool HasSightBlocker(Pos2D pos);
        void GenerateFillerWallsAsNeeded(Pos2D position);
        void RemoveAllObjects(Func<IGameObject, bool> matcherFunc);
    }
}