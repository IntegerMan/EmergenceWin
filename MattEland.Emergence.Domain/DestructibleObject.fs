namespace MattEland.Emergence.Domain

[<AbstractClass>]
type DestructibleObject(initialPosition: Position, maxHealth: int) =
  inherit WorldObject(initialPosition)
  member this.Health = new Health(maxHealth);

