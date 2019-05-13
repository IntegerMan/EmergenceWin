namespace MattEland.Emergence.Domain

[<AbstractClass>]
type GameMessage() = 
  class end

type ObjectCreatedMessage(object: WorldObject) =
  inherit GameMessage()

  member this.Object = object

type ObjectUpdatedMessage(object: WorldObject) =
  inherit GameMessage()

  member this.Object = object

type DisplayMessage(text: string) =
  inherit GameMessage()

  member this.Text = text