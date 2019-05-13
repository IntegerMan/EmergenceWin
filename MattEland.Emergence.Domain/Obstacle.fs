module MattEland.Emergence.Domain.Obstacles

open MattEland.Emergence.Domain

type ObstacleType =
    | Wall = 0
    | Column = 1
    | Service = 2
    | Data = 3
    | ThreadPool = 4
    | Barrier = 5
    
let getMaxHealth obstacleType =
    match obstacleType with
        | ObstacleType.Wall -> 10        
        | _ -> 5
        
type Obstacle (position: Position, obstacleType: ObstacleType) = 
  inherit DestructibleObject(position, getMaxHealth obstacleType)
  member this.ObstacleType = obstacleType

  override this.AsciiCharacter =
    match obstacleType with
      | ObstacleType.Barrier -> '-'
      | ObstacleType.Column -> 'o'
      | ObstacleType.Service -> '*'
      | ObstacleType.Data -> 'd'
      | ObstacleType.ThreadPool -> '~'
      | _ -> '#'

  override this.ZIndex = 50

  interface IInteractive with
    member this.interact source =
      seq {
        yield new DisplayMessage("You can't go that way. Moron.")
      }
