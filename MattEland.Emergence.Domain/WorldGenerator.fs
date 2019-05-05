module WorldGenerator

open System
open MattEland.Emergence.Domain
open MattEland.Emergence.Domain.Obstacles
open MattEland.Emergence.Domain.Floors
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

let generateMap sizeX sizeY =
    seq {
        for y in 0..sizeY do
            for x in 0..sizeX do
                let pos = new Position(x, y)
                yield generateCell pos
    }