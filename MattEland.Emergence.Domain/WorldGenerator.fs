module WorldGenerator

open System
open System
open MattEland.Emergence.Domain
open MattEland.Emergence.Domain.Obstacles
open MattEland.Emergence.Domain.Floors
open MattEland.Emergence.Domain.Rooms
open RandomFunctions

let getPosition maxX maxY= new Position(getPositiveInt maxX, getPositiveInt maxY)

let buildObstacle pos = new Obstacle(pos, enum<ObstacleType>(getPositiveInt(1)))
let buildFloor pos = new Floor(pos, enum<FloorType>(getPositiveInt(1)))
let buildVoid pos = new Void(pos)

type ObjectType =
    | Void = 0
    | Floor = 1
    | Obstacle = 2

let generateCell pos: WorldObject =
    let enumId = getPositiveInt(2)
    let objType:ObjectType = enum<ObjectType>(enumId)
    match objType with
        | ObjectType.Floor -> buildFloor pos :> WorldObject
        | ObjectType.Obstacle -> buildObstacle pos :> WorldObject
        | ObjectType.Void -> buildVoid pos :> WorldObject
        | _ -> raise (NotSupportedException("Unsupported ObjectType " + objType.ToString("G")))

/// Generates a map containing random values in every cell
let generateChaoticMap sizeX sizeY =
    seq {
        for y in 0..sizeY do
            for x in 0..sizeX do
                let pos = new Position(x, y)
                yield generateCell pos
    }

let getObjectForChar char pos = 
    match char with
        | '#' -> new Obstacle(pos, ObstacleType.Wall) :> WorldObject
        | '+' -> new Obstacle(pos, ObstacleType.Column) :> WorldObject // TODO: This is a door
        | '_' -> new Floor(pos, FloorType.Grate) :> WorldObject
        | '.' -> new Floor(pos, FloorType.LargeTile) :> WorldObject
        | 'd' -> new Floor(pos, FloorType.QuadTile) :> WorldObject // TODO: This is actually a drop indicator too
        | ' ' -> buildVoid pos :> WorldObject
        | _ -> raise (NotSupportedException("Char type " + char.ToString() + " has no object mapping"))
    
let generateMap sizeX sizeY =
    let room = loadDataFromJson Rooms.roomPillarJson
    seq {
        for y in 0..sizeY do
            for x in 0..sizeX do
                let pos = new Position(x, y)
                let char = room.getCharAtPos pos
                yield getObjectForChar char pos 
    }