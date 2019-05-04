module MattEland.Emergence.Domain.RandomFunctions

open System

let random = new Random()

let getRandomCell maxValue = random.Next(0, maxValue)
    
