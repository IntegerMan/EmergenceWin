module MattEland.Emergence.Domain.Floors

type FloorType =
    | LargeTile = 0
    | QuadTile = 1
    | Grate = 2
    | Caution = 3
  
type Floor (position: Position, floorType: FloorType) =
    inherit WorldObject(position)
    member this.FloorType = floorType