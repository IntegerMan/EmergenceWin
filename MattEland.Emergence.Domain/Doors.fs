module MattEland.Emergence.Domain.Doors

type Door (position: Position) = 
  inherit WorldObject(position)
  
  let mutable isOpen: bool = false
  
  member this.IsOpen = isOpen

  override this.AsciiCharacter =
    match isOpen with 
    | true -> '.'
    | _ -> '+'

  override this.ZIndex = 75