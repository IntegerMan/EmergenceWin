using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.EntityLogic;
using MattEland.Emergence.Services.Levels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace MattEland.Emergence.Services.Game
{
    /// <summary>
    /// Converts between LevelData and LevelDto objects.
    /// </summary>
    [UsedImplicitly]
    public static class LevelDtoBuilder
    {

        /// <summary>
        /// Builds a LevelDto object out of a LevelData instance
        /// </summary>
        /// <param name="levelData">A LevelData object</param>
        /// <returns>A LevelDto representation of <paramref name="levelData"/></returns>
        public static LevelDto BuildLevelDto(this ILevel levelData)
        {
            // Define collections
            var objects = new List<GameObjectDto>();
            var walls = new List<GameObjectDto>();
            var actors = new List<ActorDto>();
            var doors = new List<OpenableDto>();

            // In LevelData instances, all data is stored at the cell level. We need to flatten the hierarchy a bit.
            foreach (var cell in levelData.Cells)
            {
                // Add to the appropriate cell
                foreach (var obj in cell.Objects.OrderByDescending(o => o.ZIndex))
                {
                    // I don't want to see dead things
                    if (obj.IsDead)
                    {
                        continue;
                    }

                    switch (obj.ObjectType)
                    {
                        case GameObjectType.Player:
                            break;

                        case GameObjectType.Actor:
                        case GameObjectType.Turret:
                        case GameObjectType.Core:
                            var actorDto = (ActorDto)obj.BuildDto();
                            actors.Add(actorDto);
                            break;

                        case GameObjectType.Wall:
                            walls.Add(obj.BuildDto());
                            break;

                        case GameObjectType.Door:
                        case GameObjectType.Treasure:
                            doors.Add((OpenableDto)obj.BuildDto());
                            break;

                        default:
                            objects.Add(obj.BuildDto());
                            break;
                    }
                }
            }

            // Build a root level DTO
            var dto = new LevelDto
            {
                Id = levelData.Id,
                Name = levelData.Name,
                LowerRight = levelData.LowerRight.SerializedValue,
                UpperLeft = levelData.UpperLeft.SerializedValue,
                MarkedPos = levelData.MarkedPos.SerializedValue,
                Player = GetPlayerDtoFromLevel(levelData),
                Cells = BuildCellList(levelData, GetFloorCharacterForCell),
                Corruption = BuildCellList(levelData, GetCorruptionCharacterForCell),
                Walls = walls,
                Actors = actors,
                Objects = objects,
                Openable = doors,
                HasAdminAccess = levelData.HasAdminAccess
            };

            return dto;
        }

        private static IEnumerable<string> BuildCellList(ILevel level, Func<IGameCell, char> transformFunc)
        {
            var rows = new List<string>();

            var row = new StringBuilder();
            for (int y = level.UpperLeft.Y; y <= level.LowerRight.Y; y++)
            {
                row.Clear();

                for (int x = level.UpperLeft.X; x <= level.LowerRight.X; x++)
                {
                    var cell = level.GetCell(new Pos2D(x, y));
                    row.Append(transformFunc(cell));
                }

                rows.Add(row.ToString());
            }


            return rows;
        }

        private static char GetCorruptionCharacterForCell(IGameCell cell)
        {
            return cell?.Corruption.ToString(CultureInfo.InvariantCulture)[0] ?? '0';
        }

        private static char GetFloorCharacterForCell(IGameCell cell)
        {
            char cellChar;
            if (cell != null)
            {
                switch (cell.FloorType)
                {
                    case FloorType.Normal:
                        cellChar = '.';
                        break;

                    case FloorType.DecorativeTile:
                        cellChar = '\'';
                        break;

                    case FloorType.Walkway:
                        cellChar = '_';
                        break;

                    case FloorType.CautionMarker:
                        cellChar = '=';
                        break;

                    default:
                        cellChar = ' ';
                        break;
                }
            }
            else
            {
                cellChar = ' ';
            }

            return cellChar;
        }

        private static PlayerDto GetPlayerDtoFromLevel(ILevel levelData)
        {
            IPlayer player = levelData.Cells.SelectMany(c => c.Objects).FirstOrDefault(o => o.ObjectType == GameObjectType.Player) as IPlayer;
            var dto = (PlayerDto)player?.BuildDto();

            player?.CopyCellCollectionsToDto(dto, levelData);

            return dto;
        }

        /// <summary>
        /// Builds a LevelData representation of a given LevelDto instance
        /// </summary>
        /// <param name="dto">The LevelDto to convert</param>
        /// <returns>A LevelData representation of <paramref name="dto"/></returns>
        public static ILevel BuildLevelData(this LevelDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var level = new LevelData
            {
                Id = dto.Id,
                Name = dto.Name,
                UpperLeft = Pos2D.FromString(dto.UpperLeft),
                LowerRight = Pos2D.FromString(dto.LowerRight),
                MarkedPos = Pos2D.FromString(dto.MarkedPos)
            };

            LoadCells(level, dto.Cells, CreateCellFromFloorMap);
            LoadCells(level, dto.Corruption, CopyCellCorruptionMap);

            var toAdd = dto.Objects.Concat(dto.Openable).Concat(dto.Actors).Concat(dto.Walls);
            foreach (var objectDto in toAdd)
            {
                var obj = GameObjectFactory.CreateFromDto(objectDto);

                if (obj is IActor actor)
                {
                    actor.CopyCellCollectionsFromDto((ActorDto) objectDto, level);
                }

                level.AddObject(obj);
            }

            var playerObj = new Player(dto.Player);
            playerObj.CopyCellCollectionsFromDto(dto.Player, level);
            level.AddObject(playerObj);

            level.HasAdminAccess = dto.HasAdminAccess;

            return level;
        }

        
        private static void LoadCells(ILevel level, IEnumerable<string> rows, Action<ILevel, int, int, char> cellFunc)
        {
            var rowList = rows.ToList();
            var rowIndex = 0;

            for (int y = level.UpperLeft.Y; y <= level.LowerRight.Y; y++)
            {
                var row = rowList[rowIndex];

                int x = level.UpperLeft.X;

                foreach (char cell in row)
                {
                    cellFunc(level, y, x, cell);

                    x++;
                }

                rowIndex++;
            }
        }

        private static void CreateCellFromFloorMap(ILevel level, int y, int x, char cellChar)
        {
            FloorType floorType;

            switch (cellChar)
            {
                case '.':
                    floorType = FloorType.Normal;
                    break;
                case '_':
                    floorType = FloorType.Walkway;
                    break;
                case '=':
                    floorType = FloorType.CautionMarker;
                    break;
                case '\'':
                    floorType = FloorType.DecorativeTile;
                    break;
                default:
                    floorType = FloorType.Void;
                    break;
            }

            if (floorType != FloorType.Void)
            {
                level.AddCell(new CellData { Pos = new Pos2D(x, y), FloorType = floorType });
            }
        }

        private static void CopyCellCorruptionMap(ILevel level, int y, int x, char cellChar)
        {
            var cell = level.GetCell(new Pos2D(x, y));

            if (cell != null)
            {
                int.TryParse(cellChar.ToString(), out var corruption);

                cell.Corruption = corruption;
            }
        }

    }
}