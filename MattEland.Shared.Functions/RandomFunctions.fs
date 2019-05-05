module MattEland.Shared.Functions.RandomFunctions

open System

let random = new Random()

let getInt minValue maxValue = random.Next(minValue, maxValue + 1)

let getPositiveInt maxValue = getInt 0 maxValue
    
