module WorldGenerator

open System
open MattEland.Emergence.Domain
open MattEland.Emergence.Domain.Obstacles
open MattEland.Emergence.Domain.Floors
open MattEland.Emergence.Domain.Doors
open MattEland.Emergence.Domain.LevelData
open MattEland.Emergence.Domain.RoomPlacement
open MattEland.Emergence.LevelData

let getObjectForChar char pos = 
    match char with
        // Special Tiles
        | '+' -> new Door(pos) :> WorldObject
        | '|' -> new Firewall(pos) :> WorldObject
        // Character Select
        | '<' -> new LogicObject(pos, LogicObjectType.StairsDown) :> WorldObject
        | '>' -> new LogicObject(pos, LogicObjectType.StairsUp) :> WorldObject
        | 'C' -> new LogicObject(pos, LogicObjectType.Core) :> WorldObject
        | '?' -> new LogicObject(pos, LogicObjectType.Help) :> WorldObject
        | '1' | '2' | '3' | '4' | '5' | '6' -> new LogicObject(pos, LogicObjectType.CharacterSelect) :> WorldObject
        // Obstacles
        | '#' -> new Obstacle(pos, ObstacleType.Wall) :> WorldObject
        | 'd' -> new Obstacle(pos, ObstacleType.Data) :> WorldObject
        | '*' -> new Obstacle(pos, ObstacleType.Service) :> WorldObject
        | '=' -> new Obstacle(pos, ObstacleType.Barrier) :> WorldObject
        | '~' -> new Obstacle(pos, ObstacleType.ThreadPool) :> WorldObject
        // Floors
        | ',' -> new Floor(pos, FloorType.Grate) :> WorldObject
        | '.' -> new Floor(pos, FloorType.LargeTile) :> WorldObject
        | '_' -> new Floor(pos, FloorType.Caution) :> WorldObject
        | 't' -> new Floor(pos, FloorType.QuadTile) :> WorldObject // TODO: This is actually a treasure indicator
        | '$' -> new Floor(pos, FloorType.QuadTile) :> WorldObject // TODO: This is actually a drop indicator too
        // Misc. Cases
        | ' ' -> failwith "Cannot create a void entity"
        | _ -> raise (NotSupportedException("Char type " + char.ToString() + " has no object mapping"))
    
let placePrefab (instr: LevelInstruction, roomProvider: RoomDataProvider): RoomPlacement =
    let room = roomProvider.GetRoomById(instr.PrefabId)
    new RoomPlacement(room, new Position(instr.X, instr.Y))
    
let getMapInstructions levelId : RoomPlacement seq =
    
    let roomProvider = new RoomDataProvider()
    let json = LevelJson.getLevelJson levelId
    let levelData = LevelData.loadDataFromJson json 
    seq {        
        for instr in levelData.Instructions do
            yield placePrefab(instr, roomProvider)
    }
    
let generateMap levelId: WorldObject seq =
    let instructions = getMapInstructions levelId
    seq {        
        // TODO: Analyze instructions to determine min/max x/y
        for y in -100..100 do
            for x in -100..100 do
                let pos = new Position(x, y)
                
                let mutable char = ' '
                for instr in instructions do
                    char <- instr.getCharAtPos pos char
                    
                if char <> ' ' then yield getObjectForChar char pos
    }