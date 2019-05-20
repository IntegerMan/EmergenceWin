using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;

namespace MattEland.Emergence.Definitions.Entities
{
    public class Floor : GameObjectBase
    {
        public Floor(GameObjectDto dto, FloorType floorType) : base(dto)
        {
            FloorType = floorType;
        }

        public FloorType FloorType { get; }

        public override char AsciiChar => '.'; // TODO

        public override void OnInteract(CommandContext context, IGameObject source) => context.MoveObject(source, Pos);
    }
}