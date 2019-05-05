module MattEland.Emergence.Domain.RoomJson

let roomNetworkJson = """{
    "Id": "Network Adapter",
    "IsInvulnerable": true,
    "Data": [
      "  ##+#####+##  ",
      "  #.,..#..,.#  ",
      "  #.,.###.,.#  ",
      "###.,..,..,.###",
      "#>|,,,,,,,,,|<# ",
      "###.........###",
      "  ##.......##  ",
      "  ########### "
    ]
  }"""

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
  
/// JSON representing a hallway with a series of connecting services
let roomTankHallJson = """{
    "Id": "Tank Room",
    "Data": [
      "#######+#######",
      "#.............#",
      "#.*.*.*.*.*.*.#",
      "+.............+",
      "#.*.*.*.*.*.*.#",
      "#.............#",
      "#######+#######"
    ]
  }"""
  
/// JSON for a square room with 4 services
let roomQuadServiceJson = """{
    "Id": "5x5 Quad Service",
    "Data": [
      "###+###",
      "#.....#",
      "#.*.*.#",
      "+.....+",
      "#.*.*.#",
      "#.....#",
      "#######"
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