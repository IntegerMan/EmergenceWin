module WorldGenerator

open System
open MattEland.Emergence.Domain
open MattEland.Emergence.Domain.Obstacles
open MattEland.Emergence.Domain.Floors
open MattEland.Emergence.Domain.Doors
open MattEland.Emergence.Domain.LevelData
open MattEland.Emergence.Domain.RoomPlacement

let buildVoid pos = new Void(pos)
let buildDoor pos = new Door(pos)

let getObjectForChar char pos = 
    match char with
        // Special Tiles
        | '+' -> buildDoor pos :> WorldObject
        | '|' -> buildDoor pos :> WorldObject // TODO: This is a firewall
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
        | ' ' -> buildVoid pos :> WorldObject
        | _ -> raise (NotSupportedException("Char type " + char.ToString() + " has no object mapping"))
    
let placePrefab (instr: LevelInstruction): RoomPlacement =
    let room = RoomJson.getRoomById instr.PrefabId
    new RoomPlacement(room, new Position(instr.X, instr.Y))
    
let getMapInstructions levelId : RoomPlacement seq =
    
    let json = LevelJson.getLevelJson levelId
    let levelData = LevelData.loadDataFromJson json 
    seq {        
        for instr in levelData.Instructions do
            yield placePrefab instr
    }
    
let generateMap levelId =
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