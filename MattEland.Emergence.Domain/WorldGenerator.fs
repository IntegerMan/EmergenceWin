module WorldGenerator

open System
open MattEland.Emergence.Domain
open RandomFunctions

    let getPosition maxX maxY (random: Random) = new Position(getRandomCell maxX random, getRandomCell maxY random)

    let buildObstacle pos = new Obstacle(pos, 10)

    let generateObstacles count maxX maxY =
        let random = new Random()
        seq {
            for i in 0..count do
                yield getPosition maxX maxY random |> buildObstacle
        }
