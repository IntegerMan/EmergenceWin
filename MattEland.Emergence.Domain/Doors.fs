module MattEland.Emergence.Domain.Doors

type Door (position: Position) = 
  inherit WorldObject(position, System.Guid.NewGuid())
  
  let mutable isOpen: bool = false
  
  member this.IsOpen = isOpen

  override this.AsciiCharacter =
    match isOpen with 
    | true -> '.'
    | _ -> '+'

  override this.ZIndex = 75

  interface IInteractive with
    member this.interact source =
      seq {
        yield new DisplayMessage("Doors are not implemented")
      }
