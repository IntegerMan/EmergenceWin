namespace MattEland.Emergence.Domain

type Position (x:int, y:int) =
    member this.X = x
    member this.Y = y
    member this.Display = printfn "{%i, %i}" x y