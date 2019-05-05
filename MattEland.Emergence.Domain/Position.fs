namespace MattEland.Emergence.Domain

type Position (x:int, y:int) =
    member this.X = x
    member this.Y = y
    
    member this.Add (pos: Position) : Position = new Position(x + pos.X, y + pos.Y)
    member this.Subtract (pos: Position) : Position = new Position(x - pos.X, y - pos.Y)