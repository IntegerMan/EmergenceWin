module MattEland.Emergence.Domain.RandomFunctions

open System

let getRandomCell maxValue (random: Random) = random.Next(0, maxValue)
    
