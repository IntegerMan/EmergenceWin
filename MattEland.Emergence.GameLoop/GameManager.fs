namespace MattEland.Emergence.GameLoop

open MattEland.Emergence.Domain
open MattEland.Emergence.GameLoop

[<AbstractClass>]
type GameMessage() = 
  class end

type ObjectCreatedMessage(object: WorldObject) =
  inherit GameMessage()

  member this.Object = object

type GameManager() =
  let mutable currentState: GameState = GameState.NotStarted
  let mutable objects: WorldObject seq = Seq.empty
  let mutable levelId: int = 0
  
  member this.State = currentState
  
  member this.Objects = objects
    
  member this.Start(): (GameMessage seq) =
      if this.State <> GameState.NotStarted then invalidOp "The game has already been started"
      
      currentState <- GameState.Executing
      
      objects <- WorldGenerator.generateMap levelId

      currentState <- GameState.Ready

      // Return a sequence of the created elements
      seq {
        for obj in objects do yield new ObjectCreatedMessage(obj)
      }


      