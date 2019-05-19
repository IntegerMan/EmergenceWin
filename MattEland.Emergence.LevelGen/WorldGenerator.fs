module WorldGenerator

open System
open MattEland.Emergence.Model
open MattEland.Emergence.Model.Entities
open MattEland.Emergence.Domain
open MattEland.Emergence.Domain.LevelData
open MattEland.Emergence.Domain.RoomPlacement
open MattEland.Emergence.LevelData

type WorldGenerationResult(levelData: LevelData, objects: WorldObject seq) =
    member this.Name = levelData.Name
    member this.PlayerStart = levelData.PlayerStart
    member this.Objects = objects

let getObjectForChar char pos = 
    match char with
        // Special Tiles
        | '+' -> new Door(pos) :> WorldObject
        | '|' -> new Firewall(pos) :> WorldObject
        // Character Select
        | '<' -> new Stairs(pos, false) :> WorldObject
        | '>' -> new Stairs(pos, true) :> WorldObject
        | 'C' -> new Core(pos) :> WorldObject
        | '?' -> new HelpTile(pos, "Hello World") :> WorldObject
        | '1' | '2' | '3' | '4' | '5' | '6' -> new CharacterSelect(pos) :> WorldObject
        // Obstacles
        | '#' -> new Obstacle(pos, ObstacleType.Wall) :> WorldObject
        | 'd' -> new Obstacle(pos, ObstacleType.Data) :> WorldObject
        | '*' -> new Obstacle(pos, ObstacleType.Service) :> WorldObject
        | 'X' -> new Obstacle(pos, ObstacleType.Barrier) :> WorldObject
        | '~' -> new Obstacle(pos, ObstacleType.ThreadPool) :> WorldObject
        // Floors
        | ',' -> new Floor(pos, FloorType.LargeTile) :> WorldObject
        | '=' -> new Floor(pos, FloorType.Grate) :> WorldObject
        | '.' -> new Floor(pos, FloorType.LargeTile) :> WorldObject
        | '_' -> new Floor(pos, FloorType.Caution) :> WorldObject
        | 't' -> new Floor(pos, FloorType.QuadTile) :> WorldObject // TODO: This is actually a treasure indicator
        | '$' -> new Floor(pos, FloorType.QuadTile) :> WorldObject // TODO: This is actually a drop indicator too
        // Misc. Cases
        | ' ' -> failwith "Cannot create a void entity"
        | c -> new Placeholder(pos, c) :> WorldObject
    
let placePrefab (instr: LevelInstruction, roomProvider: RoomDataProvider): RoomPlacement =
    let room = roomProvider.GetRoomById(instr.PrefabId)
    new RoomPlacement(room, new Position(instr.X, instr.Y))
    
let getOrCreatePlayer (existingPlayer: Actor option, levelData: LevelData): Actor = 
  match existingPlayer with
  | Some p when existingPlayer.Value <> null -> p
  | _ -> new Actor(levelData.PlayerStart, ActorType.Player)

let generateMap (levelId:int, existingPlayer: Actor option) : WorldGenerationResult =
    
    printfn "Generate Map %i" levelId

    let roomProvider = new RoomDataProvider()
    let json = LevelJson.getLevelJson levelId
    let levelData = LevelData.loadDataFromJson json 

    let placements = Seq.map (fun i -> placePrefab(i, roomProvider)) levelData.Instructions

    // Spawn the player at the level start location
    let player = getOrCreatePlayer(existingPlayer, levelData)
    player.Pos <- levelData.PlayerStart

    // Any logic inside of this will be repeated every enumeration
    let result = new WorldGenerationResult(levelData, seq {
        
        printfn "Generate Map %i Sequence" levelId

        yield player
        
        // TODO: Analyze instructions to determine min/max x/y
        for y in -200..200 do
            for x in -200..200 do
                let pos = new Position(x, y)
                
                let mutable char = ' '
                for instr in placements do
                    char <- instr.getCharAtPos pos char
                    
                if char <> ' ' then yield getObjectForChar char pos
      })
    
    result