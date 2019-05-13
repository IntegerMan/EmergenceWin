namespace MattEland.Emergence.GameLoop

open MattEland.Emergence.Domain
open MattEland.Emergence.Domain.Actors
open MattEland.Emergence.GameLoop

type GameManager() =
  let mutable currentState: GameState = GameState.NotStarted
  let mutable objects: WorldObject seq = Seq.empty
  let mutable levelId: int = 0
  let mutable player: Actor option = None;

  let isPlayer (obj: WorldObject): bool = 
    match obj with
    | :? Actors.Actor as act -> act.ActorType = Actors.ActorType.Player
    | _ -> false

  let getObjectsAtPos (pos: Position): WorldObject seq =
    seq {
      for obj in objects do
        if obj.Position = pos then yield obj
    }
  
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

  member this.GetInteractiveObjectsAtPos(pos: Position): IInteractive seq = 

    let localObjects = getObjectsAtPos pos
    let sortedObjects: WorldObject seq = Seq.sortBy (fun (o: WorldObject) -> o.ZIndex * -1) localObjects

    seq {
      for obj in sortedObjects do
        match box obj with
        | :? IInteractive as i -> yield i
        | _ -> ()
    }

  member this.MovePlayer(direction: MoveDirection): GameMessage seq =
    seq {
      printfn "MovePlayer"
      if player.IsNone then do invalidOp "No player is present. Cannot move."

      let targetPos = direction |> player.Value.Position.GetNeighbor
      let interactive = this.GetInteractiveObjectsAtPos targetPos

      for i in interactive do
        // TODO: Some messages should stop future interactions
        let interactMessages = i.interact player.Value
        for message in interactMessages do
          yield message

      printfn "Yield update message"
      yield new ObjectUpdatedMessage(player.Value)
    }
      