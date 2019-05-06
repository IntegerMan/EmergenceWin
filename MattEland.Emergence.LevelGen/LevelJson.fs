module MattEland.Emergence.Domain.LevelJson
open System

let level0Json = """{
    "Name": "Tutorial",
    "Start": { "X": -14, "Y": 44 },
    "Instructions": [
        {
            "PrefabId": "Tutorial_Start",
            "X": -17,
            "Y": 41,
            "EncounterSet": "none"
        },
        {
            "PrefabId": "Tutorial_CharacterSelect1",
            "X": -23,
            "Y": 38,
            "EncounterSet": "none"
        },
        {
            "PrefabId": "Tutorial_CharacterSelect2",
            "X": -29,
            "Y": 38,
            "EncounterSet": "none"
        },
        {
            "PrefabId": "Tutorial_CharacterSelect3",
            "X": -35,
            "Y": 38,
            "EncounterSet": "none"
        },
        {
            "PrefabId": "Tutorial_End",
            "X": -39,
            "Y": 41,
            "EncounterSet": "none"
        },
    ]
}"""

let level1Json = """{
    "Name": "Simple Server",
    "Start": { "X": 11, "Y": 4 },
    "Instructions": [
        {
            "PrefabId": "Intersection",
            "X": 2,
            "Y": -8,
            "EncounterSet": "default"
        },
        {
            "PrefabId": "Tank Room",
            "X": -12,
            "Y": -7,
            "EncounterSet": "default"
        },
        {
            "PrefabId": "Tank Room",
            "X": 12,
            "Y": -7,
            "EncounterSet": "default"
        },
        {
            "PrefabId": "Vertical Hall",
            "X": -12,
            "Y": -1,
            "EncounterSet": "small"
        },
        {
            "PrefabId": "Network Adapter",
            "X": 0,
            "Y": 0,
            "EncounterSet": "none"
        },
        {
            "PrefabId": "Threadpool",
            "X": 34,
            "Y": -6,
            "EncounterSet": "default"
        },
        {
            "PrefabId": "Junction Room",
            "X": -20,
            "Y": -6,
            "EncounterSet": "small"
        },
        {
            "PrefabId": "Junction Room",
            "X": 26,
            "Y": -6,
            "EncounterSet": "small"
        },
        {
            "PrefabId": "Junction Room",
            "X": -20,
            "Y": 2,
            "EncounterSet": "small"
        },
        {
            "PrefabId": "Connector Bus",
            "X": 16,
            "Y": -19,
            "EncounterSet": "default"
        },
        {
            "PrefabId": "Connector Bus",
            "X": -26,
            "Y": -10,
            "EncounterSet": "default"
        },
        {
            "PrefabId": "Side Chamber",
            "X": 25,
            "Y": 2,
            "EncounterSet": "small"
        },
        {
            "PrefabId": "Side Chamber",
            "X": -35,
            "Y": -2,
            "EncounterSet": "small"
        },
        {
            "PrefabId": "Side Chamber",
            "X": -35,
            "Y": -10,
            "EncounterSet": "small"
        },
        {
            "PrefabId": "Data Warehouse",
            "X": 22,
            "Y": -18,
            "EncounterSet": "default"
        },
        {
            "PrefabId": "Operating System Kernel",
            "X": -36,
            "Y": -22,
            "EncounterSet": "kernel"
        },
        {
            "PrefabId": "Tank Room",
            "X": -4,
            "Y": -23,
            "EncounterSet": "default"
        },
        {
            "PrefabId": "Security Checkpoint",
            "X": -2,
            "Y": -17,
            "EncounterSet": "security"
        },
        {
            "PrefabId": "Narrow Hall",
            "X": -12,
            "Y": -22,
            "EncounterSet": "small"
        },
        {
            "PrefabId": "Narrow Hall",
            "X": 37,
            "Y": -15,
            "EncounterSet": "small"
        },
        {
            "PrefabId": "Narrow Hall",
            "X": 10,
            "Y": -19,
            "EncounterSet": "small"
        },
        {
            "PrefabId": "Narrow Junction",
            "X": 50,
            "Y": -6,
            "EncounterSet": "small"
        },
        {
            "PrefabId": "Narrow Junction",
            "X": -24,
            "Y": 2,
            "EncounterSet": "small"
        },
        {
            "PrefabId": "Narrow Junction",
            "X": 44,
            "Y": -15,
            "EncounterSet": "small"
        },
        {
            "PrefabId": "Narrow Junction",
            "X": -10,
            "Y": 1,
            "EncounterSet": "small"
        },
        {
            "PrefabId": "Vertical Hall",
            "X": 45,
            "Y": -12,
            "EncounterSet": "small"
        }
    ]
}"""

let level2Json = """{
  "Name": "Smart Fridge",
  "Start": { "X": 3, "Y": -2 },
  "Instructions": [
    {
      "PrefabId": "Short Vertical Hall",
      "X": -5,
      "Y": -10
    },
    {
      "PrefabId": "Short Vertical Hall",
      "X": 1,
      "Y": -10
    },
    {
      "PrefabId": "Large Hall",
      "X": -8,
      "Y": -14
    },
    {
      "PrefabId": "Horizontal Airlock",
      "X": -27,
      "Y": -14
    },
    {
      "PrefabId": "Large Hall",
      "X": -24,
      "Y": -14
    },
    {
      "PrefabId": "Large Hall",
      "X": -56,
      "Y": -36
    },
    {
      "PrefabId": "Connector Bus",
      "X": -4,
      "Y": -26
    },
    {
      "PrefabId": "Connector Bus",
      "X": -20,
      "Y": -26
    },
    {
      "PrefabId": "Small Arena",
      "X": -8,
      "Y": -36
    },
    {
      "PrefabId": "Small Arena",
      "X": -24,
      "Y": -36
    },
    {
      "PrefabId": "Water Maze",
      "X": -39,
      "Y": -20
    },
    {
      "PrefabId": "Quad Service Room",
      "X": -39,
      "Y": -30
    },
    {
      "PrefabId": "Mini Room with Twin Data Stores",
      "X": -31,
      "Y": -36
    },
    {
      "PrefabId": "Mini Room with Services",
      "X": -39,
      "Y": -36
    },
    {
      "PrefabId": "Mini Room with Treasure",
      "X": -35,
      "Y": -36
    },
    {
      "PrefabId": "Room with Services",
      "X": -56,
      "Y": -44
    },
    {
      "PrefabId": "Room with Trapped Goodies",
      "X": -50,
      "Y": -44
    },
    {
      "PrefabId": "Short Horizontal Hall",
      "X": -60,
      "Y": -40
    },
    {
      "PrefabId": "Short Horizontal Hall",
      "X": -59,
      "Y": -17
    },
    {
      "PrefabId": "Shallow Airlock",
      "X": -57,
      "Y": -26
    },
    {
      "PrefabId": "Horizontal Airlock",
      "X": -11,
      "Y": -33
    },
    {
      "PrefabId": "Gated Arena",
      "X": -55,
      "Y": -32
    },
    {
      "PrefabId": "L Pool",
      "X": -55,
      "Y": -22
    },
    {
      "PrefabId": "Turret Hall",
      "X": -74,
      "Y": -46
    },
    {
      "PrefabId": "Turret Hall",
      "X": -74,
      "Y": -6,
      "FlipY": true
    },
    {
      "PrefabId": "Dog Bone Service Hall",
      "X": -77,
      "Y": -28
    },
    {
      "PrefabId": "Barrier U Hall",
      "X": -75,
      "Y": -20
    },
    {
      "PrefabId": "Barrier U Hall",
      "X": -75,
      "Y": -36,
      "FlipY": true
    },
    {
      "PrefabId": "Core Trap Room",
      "X": -71,
      "Y": -22
    },
    {
      "PrefabId": "Core Trap Room",
      "X": -71,
      "Y": -32,
      "FlipY": true
    },
    {
      "PrefabId": "Data Beach",
      "X": -74,
      "Y": -42
    },
    {
      "PrefabId": "Data Beach",
      "X": -74,
      "Y": -12,
      "FlipY": true
    },
    {
      "PrefabId": "Horizontal Airlock",
      "X": -27,
      "Y": -33
    },
    {
      "PrefabId": "3x7 Data Lake",
      "X": -24,
      "Y": -22
    },
    {
      "PrefabId": "3x3 Treasure",
      "X": -24,
      "Y": -26
    },
    {
      "PrefabId": "3x8 Partitioned Room",
      "X": -14,
      "Y": -23
    },
    {
      "PrefabId": "3x2 Service Locker",
      "X": -14,
      "Y": -26
    },
    {
      "PrefabId": "1x2 Data Closet",
      "X": 2,
      "Y": -26
    },
    {
      "PrefabId": "1x2 Data Closet",
      "X": 4,
      "Y": -26
    },
    {
      "PrefabId": "3x3 2 North Doors",
      "X": 2,
      "Y": -23
    },
    {
      "PrefabId": "3x7 Data Peninsula",
      "X": -8,
      "Y": -26
    },
    {
      "PrefabId": "3x4 Grates",
      "X": 2,
      "Y": -19
    },
    {
      "PrefabId": "Horizontal Airlock",
      "X": -43,
      "Y": -36
    },
    {
      "PrefabId": "Vertical Airlock",
      "X": -8,
      "Y": -18
    },
    {
      "PrefabId": "Vertical Airlock",
      "X": -35,
      "Y": -24
    },
    {
      "PrefabId": "One Block Junction",
      "X": -10,
      "Y": -13
    },
    {
      "PrefabId": "Network Adapter",
      "X": -8,
      "Y": -6,
      "EncounterSet": "none"
    }
  ]
}"""

let level3Json = """{
    "Name": "Data Interchange",
    "Start": { "X": 2, "Y": 0},
    "Instructions": [
        {
            "PrefabId": "Network Adapter",
            "X": -9,
            "Y": -4,
            "EncounterSet": "none"
        },
        {
            "PrefabId": "Vertical Airlock",
            "X": -14,
            "Y": -14
        },
        {
            "PrefabId": "Vertical Airlock",
            "X": 6,
            "Y": -14
        },
        {
            "PrefabId": "Elbow Corridor",
            "X": -13,
            "Y": -10
        },
        {
            "PrefabId": "Short Vertical Hall",
            "X": -25,
            "Y": -22
        },
        {
            "PrefabId": "Short Vertical Hall",
            "X": 19,
            "Y": -22
        },
        {
            "PrefabId": "Mini Room with Grates",
            "X": 18,
            "Y": -28
        },
        {
            "PrefabId": "Bridge Room End",
            "X": -22,
            "Y": -34
        },
        {
            "PrefabId": "Bridge Room End",
            "X": 11,
            "Y": -34,
            "FlipX": true
        },
        {
            "PrefabId": "Bridge Room Segment",
            "X": -14,
            "Y": -34
        },
        {
            "PrefabId": "Bridge Room Segment",
            "X": -2,
            "Y": -34
        },
        {
            "PrefabId": "Bridge Room Segment",
            "X": 4,
            "Y": -34
        },
        {
            "PrefabId": "Bridge Room Segment",
            "X": -8,
            "Y": -34
        },
        {
            "PrefabId": "Mini Room with Grates",
            "X": -26,
            "Y": -28
        },
        {
            "PrefabId": "Mini Room with Grates",
            "X": -66,
            "Y": -18
        },
        {
            "PrefabId": "Vertical Airlock",
            "X": -66,
            "Y": -12
        },
        {
            "PrefabId": "Force Field Room",
            "X": -70,
            "Y": -8
        },
        {
            "PrefabId": "Vertical Airlock",
            "X": -66,
            "Y": -2
        },
        {
            "PrefabId": "Elbow Corridor",
            "X": 0,
            "Y": -10,
            "FlipX": true
        },
        {
            "PrefabId": "Long Corridor",
            "X": -27,
            "Y": -18,
            "FlipX": true
        },
        {
            "PrefabId": "Long Corridor",
            "X": 5,
            "Y": -18,
            "FlipX": true
        },
        {
            "PrefabId": "Short Vertical Hall",
            "X": 60,
            "Y": -25
        },
        {
            "PrefabId": "Short Vertical Hall",
            "X": 60,
            "Y": -11
        },
        {
            "PrefabId": "Short Vertical Hall",
            "X": -34,
            "Y": -25
        },
        {
            "PrefabId": "Short Vertical Hall",
            "X": -34,
            "Y": -11
        },
        {
            "PrefabId": "Horizontal Water Maze",
            "X": 54,
            "Y": -29
        },
        {
            "PrefabId": "Horizontal Water Maze",
            "X": 54,
            "Y": -7,
            "FlipX": true,
            "FlipY": true
        },
        {
            "PrefabId": "Horizontal Water Maze",
            "X": -40,
            "Y": -29,
            "FlipX": true
        },
        {
            "PrefabId": "Horizontal Water Maze",
            "X": -40,
            "Y": -7,
            "FlipY": true
        },
        {
            "PrefabId": "Widening Segment",
            "X": 23,
            "Y": -21
        },
        {
            "PrefabId": "Widening Segment",
            "X": -53,
            "Y": -21
        },
        {
            "PrefabId": "Wide Water Bridge Segment",
            "X": -45,
            "Y": -21
        },
        {
            "PrefabId": "Wide Water Bridge Segment",
            "X": -39,
            "Y": -21
        },
        {
            "PrefabId": "Widening Segment",
            "X": -34,
            "Y": -21,
            "FlipX": true
        },
        {
            "PrefabId": "Wide Water Bridge Segment",
            "X": 31,
            "Y": -21
        },
        {
            "PrefabId": "Wide Water Bridge Segment",
            "X": 37,
            "Y": -21
        },
        {
            "PrefabId": "Wide Water Bridge Segment",
            "X": 43,
            "Y": -21
        },
        {
            "PrefabId": "Wide Water Bridge Segment",
            "X": 49,
            "Y": -21
        },
        {
            "PrefabId": "Wide Water Bridge Segment",
            "X": 55,
            "Y": -21
        },
        {
            "PrefabId": "Widening Segment",
            "X": 60,
            "Y": -21,
            "FlipX": true
        },
        {
            "PrefabId": "Horizontal Airlock",
            "X": 67,
            "Y": -18
        },
        {
            "PrefabId": "Narrow Hall",
            "X": -60,
            "Y": -17
        },
        {
            "PrefabId": "One Block Junction",
            "X": -62,
            "Y": -17
        },
        {
            "PrefabId": "Wellhead Quad Core",
            "X": 71,
            "Y": -25
        },
        {
            "PrefabId": "Wellhead Quad Core",
            "X": -73,
            "Y": 2
        },
        {
            "PrefabId": "Large Caution Room",
            "X": -9,
            "Y": -21,
            "FlipX": true
        }
    ]
}"""

let level4Json = """{
  "Name": "Bastion System",
  "Start": { "X": -20, "Y": 31 },
  "Instructions": [
    {
      "PrefabId": "Widening Corridor",
      "X": -29,
      "Y": 23
    },
    {
      "PrefabId": "Widening Corridor",
      "X": -2,
      "Y": -17,
      "FlipY": true
    },
    {
      "PrefabId": "Connector Bus",
      "X": -27,
      "Y": 11
    },
    {
      "PrefabId": "Connector Bus",
      "X": -18,
      "Y": -13
    },
    {
      "PrefabId": "Intersection",
      "X": -60,
      "Y": -2
    },
    {
      "PrefabId": "Connector Bus",
      "X": 0,
      "Y": -39
    },
    {
      "PrefabId": "5x7 Core",
      "X": -51,
      "Y": -13
    },
    {
      "PrefabId": "15x9 Swimming Pool",
      "X": -56,
      "Y": -27
    },
    {
      "PrefabId": "Corridor with Ducts",
      "X": -56,
      "Y": -17
    },
    {
      "PrefabId": "Corridor with Ducts",
      "X": -45,
      "Y": -17,
      "FlipX": true 
    },
    {
      "PrefabId": "Pillar Room",
      "X": -2,
      "Y": -27
    },
    {
      "PrefabId": "Wellhead One Core",
      "X": -24,
      "Y": -31
    },
    {
      "PrefabId": "5x4 Vertical Hallway",
      "X": -9,
      "Y": 13
    },
    {
      "PrefabId": "5x4 Vertical Hallway",
      "X": -18,
      "Y": -36
    },
    {
      "PrefabId": "5x5 Quad Service",
      "X": -8,
      "Y": -10
    },
    {
      "PrefabId": "5x5 NS Bridge",
      "X": -18,
      "Y": -42
    },
    {
      "PrefabId": "Horizontal Airlock",
      "X": -22,
      "Y": -9
    },
    {
      "PrefabId": "Horizontal Airlock",
      "X": -12,
      "Y": -9
    },
    {
      "PrefabId": "Horizontal Airlock",
      "X": -6,
      "Y": -24
    },
    {
      "PrefabId": "Horizontal Airlock",
      "X": -50,
      "Y": 0
    },
    {
      "PrefabId": "Horizontal Airlock",
      "X": -28,
      "Y": -24
    },
    {
      "PrefabId": "Horizontal Airlock",
      "X": -40,
      "Y": -24
    },
    {
      "PrefabId": "Horizontal Airlock",
      "X": -2,
      "Y": -10
    },
    {
      "PrefabId": "13x5 Laser Maze",
      "X": -55,
      "Y": -41
    },
    {
      "PrefabId": "Vertical Airlock",
      "X": -54,
      "Y": -45
    },
    {
      "PrefabId": "Quad Service Room",
      "X": -21,
      "Y": 14
    },
    {
      "PrefabId": "T Corridor",
      "X": -49,
      "Y": 14
    },
    {
      "PrefabId": "6x7 Barrier Room",
      "X": -46,
      "Y": -2
    },
    {
      "PrefabId": "Room with Services",
      "X": -51,
      "Y": -35
    },
    {
      "PrefabId": "7x5 Service Room",
      "X": -57,
      "Y": 16
    },
    {
      "PrefabId": "Two-Sided Trench",
      "X": -21,
      "Y": 8
    },
    {
      "PrefabId": "5x3 Vertical Hallway",
      "X": -27,
      "Y": 7
    },
    {
      "PrefabId": "One Block Junction",
      "X": -25,
      "Y": 5
    },
    {
      "PrefabId": "One Block Junction",
      "X": -56,
      "Y": -4
    },
    {
      "PrefabId": "One Block Junction",
      "X": -42,
      "Y": -4
    },
    {
      "PrefabId": "5x3 Vertical Hallway",
      "X": -34,
      "Y": 4
    },
    {
      "PrefabId": "Round Turret Pond",
      "X": -36,
      "Y": -26
    },
    {
      "PrefabId": "5x7 Services",
      "X": -28,
      "Y": -11
    },
    {
      "PrefabId": "7x3 Treasure",
      "X": -35,
      "Y": 0
    },
    {
      "PrefabId": "3x5 Horizontal Hall",
      "X": -39,
      "Y": -1
    },
    {
      "PrefabId": "7x2 Data",
      "X": -35,
      "Y": -3
    },
    {
      "PrefabId": "Corner Hall",
      "X": -32,
      "Y": -8
    },
    {
      "PrefabId": "One Block Junction",
      "X": -39,
      "Y": 18
    },
    {
      "PrefabId": "1x7 Hallway",
      "X": -45,
      "Y": 6
    },
    {
      "PrefabId": "T Corridor with Water",
      "X": -10,
      "Y": 18
    },
    {
      "PrefabId": "T Corridor with Water",
      "X": -19,
      "Y": -46,
      "FlipY": true
    },
    {
      "PrefabId": "BossSideChamber",
      "X": -10,
      "Y": 23,
      "FlipY": true
    },
    {
      "PrefabId": "BossSideChamber",
      "X": -19,
      "Y": -54
    },
    {
      "PrefabId": "5x1 Hall",
      "X": -33,
      "Y": 16
    },
    {
      "PrefabId": "5x1 Hall",
      "X": -33,
      "Y": -51
    },
    {
      "PrefabId": "5x1 Hall",
      "X": -24,
      "Y": -51
    },
    {
      "PrefabId": "5x1 Hall",
      "X": -11,
      "Y": -51
    },
    {
      "PrefabId": "5x1 Hall",
      "X": -43,
      "Y": 10
    },
    {
      "PrefabId": "5x1 Hall",
      "X": -47,
      "Y": -51
    },
    {
      "PrefabId": "Mini-Wellhead 1 Core",
      "X": -57,
      "Y": -55
    },
    {
      "PrefabId": "Horizontal Airlock",
      "X": -28,
      "Y": -52
    },
    {
      "PrefabId": "Side Cage",
      "X": 2,
      "Y": -13
    },
    {
      "PrefabId": "3x7 Center Treasure",
      "X": 2,
      "Y": -4
    },
    {
      "PrefabId": "7x5 Service Room",
      "X": -41,
      "Y": -53
    },
    {
      "PrefabId": "3x5 Hub Room",
      "X": -37,
      "Y": 14
    },
    {
      "PrefabId": "Water Pond Room",
      "X": -37,
      "Y": 8
    },
    {
      "PrefabId": "3x5 Dual Data",
      "X": -26,
      "Y": -1
    },
    {
      "PrefabId": "3x5 Dual Data",
      "X": -8,
      "Y": -1
    },
    {
      "PrefabId": "5x1 Hall",
      "X": -4,
      "Y": 1
    },
    {
      "PrefabId": "10 Silos",
      "X": -5,
      "Y": -54
    },
    {
      "PrefabId": "Water Trench Add-on",
      "X": -2,
      "Y": -46
    },
    {
      "PrefabId": "Tank Room",
      "X": -22,
      "Y": -1
    },
    {
      "PrefabId": "Room with Services",
      "X": -9,
      "Y": 5
    },
    {
      "PrefabId": "Network Adapter",
      "X": -31,
      "Y": 27,
      "EncounterSet": "none"
    }
  ]
}"""

let level5Json = """{
  "Name": "Router Gateway",
  "Start": {"X": 5, "Y":  2},
  "Instructions": [
    {
      "PrefabId": "Network In-Only",
      "X": 0,
      "Y": 0,
      "EncounterSet": "none"
    },
    {
      "PrefabId": "Tank Room",
      "X": -14,
      "Y": -1
    },
    {
      "PrefabId": "Pillar Room",
      "X": -24,
      "Y": -3
    },
    {
      "PrefabId": "Airlock",
      "X": -21,
      "Y": 7
    },
    {
      "PrefabId": "AirlockHForceField",
      "X": -28,
      "Y": 0
    },
    {
      "PrefabId": "Airlock",
      "X": -21,
      "Y": -7
    },
    {
      "PrefabId": "Boss Side Core",
      "X": -24,
      "Y": -21,
      "EncounterSet": "bossCore"
    },
    {
      "PrefabId": "Boss Side Core",
      "X": -24,
      "Y": 11,
      "FlipY": true,
      "EncounterSet": "bossCore"
    },
    {
      "PrefabId": "BossChamber",
      "X": -46,
      "Y": -7,
      "EncounterSet": "boss"
    },
    {
      "PrefabId": "BossSideChamber",
      "X": -41,
      "Y": -15,
      "EncounterSet": "bossCore"
    },
    {
      "PrefabId": "BossSideChamber",
      "X": -41,
      "Y": 12,
      "FlipY": true,
      "EncounterSet": "bossCore"
    },
    {
      "PrefabId": "BossEndChamber",
      "X": -54,
      "Y": -1,
      "EncounterSet": "bossEnd"
    }
  ]
}"""


let getLevelJson levelId : string =
  match levelId with
    | 0 -> level0Json
    | 1 -> level1Json
    | 2 -> level2Json
    | 3 -> level3Json
    | 4 -> level4Json
    | 5 -> level5Json
    | _ -> raise (NotSupportedException("The level " + levelId.ToString() + " is not supported"))