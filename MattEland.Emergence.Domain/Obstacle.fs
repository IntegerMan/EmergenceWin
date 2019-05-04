namespace MattEland.Emergence.Domain

type Obstacle (position: Position, health: Health) = 
  inherit WorldObject(position)