namespace MattEland.Emergence.GameLoop

open MattEland.Emergence.Domain

type GameManager() =
  let mutable currentState: GameState = GameState.NotStarted
  let mutable objects: WorldObject seq = Seq.empty
  let mutable levelId: int = 0
  
  member this.State = currentState
  
  member this.Objects = objects
    
  member this.Start() =
      if this.State <> GameState.NotStarted then invalidOp "The game has already been started"
      
      currentState <- GameState.Executing
      
      objects <- WorldGenerator.generateMap levelId
      
      currentState <- GameState.Ready
      