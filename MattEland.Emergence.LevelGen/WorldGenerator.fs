module WorldGenerator

open System
open MattEland.Emergence.Domain
open MattEland.Emergence.Domain.Obstacles
open MattEland.Emergence.Domain.Floors
open MattEland.Emergence.Domain.Doors
open MattEland.Emergence.Domain.LevelData
open MattEland.Emergence.Domain.RoomPlacement
open MattEland.Shared.Functions.RandomFunctions

let getPosition maxX maxY= new Position(getPositiveInt maxX, getPositiveInt maxY)

let buildObstacle pos = new Obstacle(pos, enum<ObstacleType>(getPositiveInt(1)))
let buildFloor pos = new Floor(pos, enum<FloorType>(getPositiveInt(1)))
let buildVoid pos = new Void(pos)
let buildDoor pos = new Door(pos)

let getObjectForChar char pos = 
    match char with
        // Special Tiles
        | '+' -> buildDoor pos :> WorldObject
        | '|' -> buildDoor pos :> WorldObject // TODO: This is a firewall
        | '<' -> new Floor(pos, FloorType.Caution) :> WorldObject // TODO: This is an outbound port
        | '>' -> new Floor(pos, FloorType.Caution) :> WorldObject // TODO: This is an inbound port
        | '1' | '2' | '3' | '4' | '5' | '6' -> new Floor(pos, FloorType.Caution) :> WorldObject // TODO: Character select etc
        // Obstacles
        | '#' -> new Obstacle(pos, ObstacleType.Wall) :> WorldObject
        | 'd' -> new Obstacle(pos, ObstacleType.Data) :> WorldObject
        | '*' -> new Obstacle(pos, ObstacleType.Service) :> WorldObject
        | '=' -> new Obstacle(pos, ObstacleType.Barrier) :> WorldObject
        | '~' -> new Obstacle(pos, ObstacleType.ThreadPool) :> WorldObject
        | 'C' -> new Obstacle(pos, ObstacleType.Core) :> WorldObject // TODO: Core
        | '?' -> new Obstacle(pos, ObstacleType.Help) :> WorldObject // TODO: Help
        // Floors
        | ',' -> new Floor(pos, FloorType.Grate) :> WorldObject
        | '.' -> new Floor(pos, FloorType.LargeTile) :> WorldObject
        | '_' -> new Floor(pos, FloorType.Caution) :> WorldObject
        | 't' -> new Floor(pos, FloorType.QuadTile) :> WorldObject // TODO: This is actually a treasure indicator
        | '$' -> new Floor(pos, FloorType.QuadTile) :> WorldObject // TODO: This is actually a drop indicator too
        // Misc. Cases
        | ' ' -> buildVoid pos :> WorldObject
        | _ -> raise (NotSupportedException("Char type " + char.ToString() + " has no object mapping"))
    
// let placeRoom(json, x, y): RoomPlacement = new RoomPlacement(loadDataFromJson json, new Position(x, y))
    
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
        for y in -100..100 do
            for x in -100..100 do
                let pos = new Position(x, y)
                
                let mutable char = ' '
                for instr in instructions do
                    char <- instr.getCharAtPos pos char
                    
                if char <> ' ' then yield getObjectForChar char pos
    }