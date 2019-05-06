module MattEland.Emergence.Domain.RoomPlacement

open MattEland.Emergence.Domain
open MattEland.Emergence.Domain.Rooms

let mergeChars oldChar newChar =
    match oldChar with
    | '+' -> oldChar
    | _ -> match newChar with
            | ' ' -> oldChar
            | _ -> newChar

type RoomPlacement (room: RoomData, upperLeftCorner: Position) =    
    member this.getCharAtPos (pos: Position) currentChar =
        pos.Subtract upperLeftCorner |> room.getCharAtPos |> mergeChars currentChar 