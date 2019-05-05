module MattEland.Shared.Functions.Strings

/// Gets a character from a string, falling back to the fallback character if the index is out of range
let getCharSafe (str:string) index fallback : char =
    match index < 0 || index >= str.Length with
      | true -> fallback
      | false -> str.[index] 