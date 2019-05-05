module MattEland.Emergence.Domain.Rooms

open Newtonsoft.Json

/// Represents a room that can be applied to a level
type RoomData (id: string, data: string list) =
  member this.Id = id;
  member this.Data = data;
  
  /// Grabs the character at the relative position within the data, returning a space if that is invalid
  member this.getCharAtPos x y =
    match y < 0 || y >= data.Length with
    | true -> ' '
    | false ->
      let row = data.[y]
      match x < 0 || x >= row.Length with
        | true -> ' '
        | false -> row.[x]

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

