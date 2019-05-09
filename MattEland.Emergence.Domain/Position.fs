namespace MattEland.Emergence.Domain

open System

type MoveDirection =
  | Up = 0
  | Right = 1
  | Down = 2
  | Left = 3

[<CustomEquality>]
[<NoComparison>]
type Position (x:int, y:int) =
  struct
    member this.X = x
    member this.Y = y
    
    member this.Add (pos: Position) : Position = new Position(x + pos.X, y + pos.Y)
    member this.Subtract (pos: Position) : Position = new Position(x - pos.X, y - pos.Y)

    override this.ToString() = "{" + x.ToString() + ", " + y.ToString() + "}"

    override this.GetHashCode() = (x.GetHashCode() * 100) + y.GetHashCode()
      
    override this.Equals(b) =
      match b with
      | :? Position as p -> p.X = x && p.Y = y
      | _ -> false

    member this.GetNeighbor(direction: MoveDirection): Position =
      match direction with
      | MoveDirection.Up -> new Position(x, y - 1)
      | MoveDirection.Right -> new Position(x + 1, y)
      | MoveDirection.Down -> new Position(x, y + 1)
      | MoveDirection.Left -> new Position(x - 1, y)
      | _ -> raise (NotSupportedException("The direction " + direction.ToString() + " is not supported for position modification"))
  end