module MattEland.Emergence.Domain.Rooms

open Newtonsoft.Json
open MattEland.Shared.Functions.Strings

/// Represents a room that can be applied to a level
type RoomData (id: string, data: string list, isInvulnerable: bool) =
  member this.Id = id;
  member this.Data = data;
  member this.IsInvulnerable = isInvulnerable;
  
  /// Grabs the character at the relative position within the data, returning a space if that is invalid
  member this.getCharAtPos (pos: Position) =
    match pos.Y < 0 || pos.Y >= data.Length with
    | true -> ' '
    | false -> getCharSafe data.[pos.Y] pos.X ' '

/// Creates a room from the provided JSON
let loadDataFromJson json : RoomData = JsonConvert.DeserializeObject<RoomData> json
