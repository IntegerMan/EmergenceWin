namespace MattEland.Emergence.Domain

[<AbstractClass>]
type WorldObject(initialPosition: Position) =
  let mutable position = initialPosition
  
  member this.Position = position
  
  member this.UpdatePosition newPos = position <- newPos

  abstract member AsciiCharacter: char;

/// Represents empty territory in the game world
type Void(initialPosition: Position) =
  inherit WorldObject(initialPosition)

  override this.AsciiCharacter = ' '