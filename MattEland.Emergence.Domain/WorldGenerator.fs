module WorldGenerator

open System
open MattEland.Emergence.Domain
open MattEland.Emergence.Domain.Obstacles
open MattEland.Emergence.Domain.Floors
open RandomFunctions

let getPosition maxX maxY= new Position(getPositiveInt maxX, getPositiveInt maxY)

let buildObstacle pos = new Obstacle(pos, enum<ObstacleType>(getPositiveInt(1)))
let buildFloor pos = new Floor(pos, enum<FloorType>(getPositiveInt(1)))

type ObjectType =
    | Floor = 0
    | Obstacle = 1

let generateCell pos: WorldObject =
    let enumId = getPositiveInt(1)
    let objType:ObjectType = enum<ObjectType>(enumId)
    match objType with
        | ObjectType.Floor -> buildFloor pos :> WorldObject
        | ObjectType.Obstacle -> buildObstacle pos :> WorldObject
        | _ -> raise (NotSupportedException("Unsupported ObjectType " + objType.ToString("G")))

let generateMap sizeX sizeY =
    seq {
        for y in 0..sizeY do
            for x in 0..sizeX do
                let pos = new Position(x, y)
                yield generateCell pos
    }