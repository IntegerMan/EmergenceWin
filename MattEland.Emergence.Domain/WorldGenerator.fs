module WorldGenerator

open MattEland.Emergence.Domain
open RandomFunctions

    let getPosition maxX maxY= new Position(getRandomCell maxX, getRandomCell maxY)

    let buildObstacle pos = new Obstacle(pos, 10)

    let generateCell pos =  buildObstacle pos // TODO: An option for other types of cells would be good
    
    let generateObstacles count maxX maxY =
        seq {
            for i in 0..count do
                yield getPosition maxX maxY |> buildObstacle
        }

    let generateMap sizeX sizeY =
        seq {
            for y in 0..sizeY do
                for x in 0..sizeX do
                    let pos = new Position(x, y)
                    yield generateCell pos
        }