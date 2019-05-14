module MattEland.Emergence.Domain.RoomPlacement

open MattEland.Emergence.Model
open MattEland.Emergence.LevelData

let mergeChars oldChar newChar =
    match oldChar with
    | '+' -> oldChar
    | _ -> match newChar with
            | ' ' -> oldChar
            | _ -> newChar

type RoomPlacement (room: RoomData, upperLeftCorner: Position) =    
    member this.getCharAtPos (pos: Position) currentChar =
        pos.Subtract upperLeftCorner |> room.GetCharacterAtPosition |> mergeChars currentChar 