module MattEland.Emergence.Domain.RoomJson

/// JSON representing a room with 4 pillars
let roomPillarJson = """{
    "Id": "Pillar Room",
    "Data": [
      "#####+#####",
      "#...._....#",
      "#.##._.##.#",
      "#.#.._..#.#",
      "#...___...#",
      "+____d____+",
      "#...___...#",
      "#.#.._..#.#",
      "#.##._.##.#",
      "#...._....#",
      "#####+#####"
    ]
  }"""
  
/// JSON representing a starting room with 4 corners and loot in the center
let roomIntersectionJson = """{
    "Id": "Intersection",
    "Data": [
      "#####+#####",
      "#.........#",
      "#...#.#...#",
      "#..##.##..#",
      "#....$....#",
      "#..##.##..#",
      "#...#.#...#",
      "#.........#",
      "#####+#####"
    ]
  }"""