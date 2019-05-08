module MattEland.Emergence.Domain.Actors

open System

type ActorType =
    | Player

let getMaxHealth actorType =
    match actorType with
    | ActorType.Player -> 50
    | _ -> raise (NotImplementedException("ActorType " + actorType.ToString() + " does not have a max health set"))

type Actor (position: Position, actorType: ActorType) = 
  inherit DestructibleObject(position, getMaxHealth actorType)

  override this.AsciiCharacter = '@'

  override this.ZIndex = 90

  member this.ActorType = actorType