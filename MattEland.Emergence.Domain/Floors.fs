module MattEland.Emergence.Domain.Floors

type FloorType =
    | LargeTile = 0
    | QuadTile = 1
    | Grate = 2
    | Caution = 3
  
type Floor (position: Position, floorType: FloorType) =
    inherit WorldObject(position, System.Guid.NewGuid())
    member this.FloorType = floorType

    override this.AsciiCharacter = 
      match floorType with
      | FloorType.QuadTile -> ','
      | FloorType.Grate -> '_'
      | FloorType.Caution -> '='
      | _ -> '.'

    override this.ZIndex = 10