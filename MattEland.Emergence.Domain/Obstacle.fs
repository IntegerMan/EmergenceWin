module MattEland.Emergence.Domain.Obstacles

open MattEland.Emergence.Domain

type ObstacleType =
    | Wall = 0
    | Column = 1
    | Service = 2
    | Data = 3
    | ThreadPool = 4
    
let getMaxHealth obstacleType =
    match obstacleType with
        | ObstacleType.Wall -> 10        
        | _ -> 5
        
type Obstacle (position: Position, obstacleType: ObstacleType) = 
  inherit DestructibleObject(position, getMaxHealth obstacleType)
  member this.ObstacleType = obstacleType