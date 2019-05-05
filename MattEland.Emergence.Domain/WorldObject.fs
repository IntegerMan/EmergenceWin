namespace MattEland.Emergence.Domain

[<AbstractClass>]
type WorldObject(initialPosition: Position) =
  let mutable position = initialPosition
  
  member this.Position = position
  
  member this.UpdatePosition newPos =
       position <- newPos

/// Represents empty territory in the game world
type Void(initialPosition: Position) =
  inherit WorldObject(initialPosition)