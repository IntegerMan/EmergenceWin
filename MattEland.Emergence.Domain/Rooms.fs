module MattEland.Emergence.Domain.Rooms

open Newtonsoft.Json

/// Represents a room that can be applied to a level
type RoomData (id: string, data: string list) =
  member this.Id = id;
  member this.Data = data;
  
  /// Grabs the character at the relative position within the data, returning a space if that is invalid
  member this.getCharAtPos (pos: Position) =
    match pos.Y < 0 || pos.Y >= data.Length with
    | true -> ' '
    | false ->
      let row = data.[pos.Y]
      match pos.X < 0 || pos.X >= row.Length with
        | true -> ' '
        | false -> row.[pos.X]

/// Creates a room from the provided JSON
let loadDataFromJson json : RoomData = JsonConvert.DeserializeObject<RoomData> json
  
/// JSON representing a room with 4 pillars
let roomPillarJson = """
  {
    "Id": "Pillar Room",
    "Data": [
      "#####+#####",
      "#...._....#",
      "#.##._.##.#",
      "#.#.._..#.#",
      "#...___...#",
      "+____d____+",
      "#...___...#",
      "#.#.._..#.#",
      "#.##._.##.#",
      "#...._....#",
      "#####+#####"
    ]
  }
"""

