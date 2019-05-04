namespace MattEland.Emergence.Domain

type Obstacle (position: Position, maxHealth: int) = 
  inherit DestructibleObject(position, maxHealth)