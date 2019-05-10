namespace MattEland.Emergence.Domain

[<AbstractClass>]
type DestructibleObject(initialPosition: Position, maxHealth: int) =
  inherit WorldObject(initialPosition, System.Guid.NewGuid())
  member this.Health = new Health(maxHealth);

