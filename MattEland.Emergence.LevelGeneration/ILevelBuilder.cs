using System;
using System.Collections.Generic;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.LevelGeneration.Prefabs;

namespace MattEland.Emergence.LevelGeneration
{
    public interface ILevelBuilder
    {
        IReadOnlyCollection<IGameCell> Cells { get; }
        LevelType LevelId { get; set; }
        string LevelName { get; set; }
        Pos2D PlayerStart { get; set; }

        void AddCell(IGameCell cell, Guid roomId);
        Guid AddInstruction(LevelAssemblyInstruction instruction);
        Guid AddRectangularRoom(Pos2D upperLeft, Pos2D size);
        IGameCell BuildCell(char terrain, Pos2D point);
        IGameCell BuildPrefabCell(char terrain, Pos2D point, PrefabData sourcePrefab);
        ILevel CreateLevel();
        IGameObject GetGameObjectFromCellTerrain(char terrain, Pos2D pos);
    }
}