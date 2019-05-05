module MattEland.Emergence.Domain.LevelData

open Newtonsoft.Json
open MattEland.Shared.Functions.Strings

type LevelInstruction (prefabId: string, x: int, y: int, encounterSet: string) =
  member this.PrefabId = prefabId;
  member this.X = x;
  member this.Y = y;
  member this.EncounterSet = encounterSet;
    
/// Represents a room that can be applied to a level
type LevelData (name: string, start: Position, instructions: LevelInstruction seq) =
  member this.Name = name
  member this.Instructions = instructions
  member this.PlayerStart = start

/// Creates a room from the provided JSON
let loadDataFromJson json : LevelData = JsonConvert.DeserializeObject<LevelData> json
