namespace MattEland.Emergence.Domain

[<AbstractClass>]
type WorldObject(initialPosition: Position) =
  let mutable position = initialPosition
  
  member this.Position
    with get () = position
    and set (newPos) = position <- newPos
  
  abstract member AsciiCharacter: char;

  abstract member ZIndex: int