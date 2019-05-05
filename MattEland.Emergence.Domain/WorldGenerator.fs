module WorldGenerator

open System
open MattEland.Emergence.Domain
open MattEland.Emergence.Domain.Obstacles
open MattEland.Emergence.Domain.Floors
open MattEland.Emergence.Domain.Rooms
open MattEland.Emergence.Domain.Doors
open MattEland.Emergence.Domain.RoomPlacement
open RandomFunctions

let getPosition maxX maxY= new Position(getPositiveInt maxX, getPositiveInt maxY)

let buildObstacle pos = new Obstacle(pos, enum<ObstacleType>(getPositiveInt(1)))
let buildFloor pos = new Floor(pos, enum<FloorType>(getPositiveInt(1)))
let buildVoid pos = new Void(pos)
let buildDoor pos = new Door(pos)

let getObjectForChar char pos = 
    match char with
        // Special Tiles
        | '+' -> buildDoor pos :> WorldObject
        // Obstacles
        | '#' -> new Obstacle(pos, ObstacleType.Wall) :> WorldObject
        | 'd' -> new Obstacle(pos, ObstacleType.Column) :> WorldObject
        // Floors
        | '_' -> new Floor(pos, FloorType.Grate) :> WorldObject
        | '.' -> new Floor(pos, FloorType.LargeTile) :> WorldObject
        | '$' -> new Floor(pos, FloorType.QuadTile) :> WorldObject // TODO: This is actually a drop indicator too
        // Misc. Cases
        | ' ' -> buildVoid pos :> WorldObject
        | _ -> raise (NotSupportedException("Char type " + char.ToString() + " has no object mapping"))
    
let placeRoom(json, x, y): RoomPlacement = new RoomPlacement(loadDataFromJson json, new Position(x, y))
    
let getMapInstructions sizeX sizeY : RoomPlacement seq =
    seq {
        yield placeRoom(RoomJson.roomPillarJson, 0, 0)
        yield placeRoom(RoomJson.roomIntersectionJson, 10, 3)
    }
    
let generateMap sizeX sizeY =
    seq {
        let instructions = getMapInstructions sizeX sizeY

        for y in 0..sizeY do
            for x in 0..sizeX do
                let pos = new Position(x, y)
                
                let mutable char = ' '
                for instr in instructions do
                    char <- instr.getCharAtPos pos char
                    
                yield getObjectForChar char pos
    }