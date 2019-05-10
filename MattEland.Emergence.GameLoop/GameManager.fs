namespace MattEland.Emergence.GameLoop

open MattEland.Emergence.Domain
open MattEland.Emergence.Domain.Actors
open MattEland.Emergence.GameLoop

[<AbstractClass>]
type GameMessage() = 
  class end

type ObjectCreatedMessage(object: WorldObject) =
  inherit GameMessage()

  member this.Object = object

type ObjectUpdatedMessage(object: WorldObject) =
  inherit GameMessage()

  member this.Object = object

type GameManager() =
  let mutable currentState: GameState = GameState.NotStarted
  let mutable objects: WorldObject seq = Seq.empty
  let mutable levelId: int = 0
  let mutable player: Actor option = None;

  let isPlayer (obj: WorldObject): bool = 
    match obj with
    | :? Actors.Actor as act -> act.ActorType = Actors.ActorType.Player
    | _ -> false
  
  member this.State = currentState
  
  member this.Objects = objects
  member this.Player = player
    
  member this.Start(): (GameMessage seq) =
      if this.State <> GameState.NotStarted then invalidOp "The game has already been started"
      
      currentState <- GameState.Executing
      
      printfn "Start invoked"
      objects <- WorldGenerator.generateMap levelId

      printfn "Looking for player"
      for obj in objects do 
        if isPlayer obj then do
          player <- Some(obj :?> Actor)

      currentState <- GameState.Ready

      // Return a sequence of the created elements
      seq {
        for obj in objects do 
          yield new ObjectCreatedMessage(obj)
      }

  member this.MovePlayer(direction: MoveDirection) =
    seq {
      printfn "MovePlayer"
      if player.IsNone then do invalidOp "No player is present. Cannot move."

      printfn "Set Player Position"
      player.Value.Position <- player.Value.Position.GetNeighbor direction

      printfn "Yield update message"
      yield new ObjectUpdatedMessage(player.Value)
    }
      