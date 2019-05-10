namespace MattEland.Emergence.Domain

[<AbstractClass>]
type WorldObject(initialPosition: Position, id: System.Guid) =
  let mutable position = initialPosition
  
  member this.Position
    with get () = position
    and set (newPos) = position <- newPos
  
  member this.Id = id

  abstract member AsciiCharacter: char

  abstract member ZIndex: int