namespace MattEland.Emergence.Domain

type LogicObjectType =
  | Core = 0
  | CharacterSelect = 1
  | StairsUp = 2
  | StairsDown = 3
  | Help = 4

/// Represents empty territory in the game world
type LogicObject(initialPosition: Position, objType: LogicObjectType) =
  inherit WorldObject(initialPosition)

  member this.ObjectType = objType

  override this.AsciiCharacter =
    match this.ObjectType with
      | LogicObjectType.Core -> 'C'
      | LogicObjectType.CharacterSelect -> '@'
      | LogicObjectType.StairsDown -> '<'
      | LogicObjectType.StairsUp -> '>'
      | _ -> '?'