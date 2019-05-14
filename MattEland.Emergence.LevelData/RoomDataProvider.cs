﻿using System.Collections.Generic;
using JetBrains.Annotations;
using MattEland.Shared.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MattEland.Emergence.LevelData
{
    public class RoomDataProvider
    {
        [NotNull]
        private readonly string _prefabJson = $@"[
  {{
    ""Id"": ""Network Adapter"",
    ""IsInvulnerable"": true,
    ""Data"": [
      ""  ##+#####+##  "",
      ""  #.,..#..,.#  "",
      ""  #.,.###.,.#  "",
      ""###.,..,..,.###"",
      ""#>|,,,,,,,,,|<# "",
      ""###.........###"",
      ""  ##.......##  "",
      ""  ########### ""
    ]
  }},
  {{
    ""Id"": ""Network In-Only"",
    ""IsInvulnerable"": true,
    ""Data"": [
      ""###+### "",
      ""#..,..###"",
      ""+..,,,|<# "",
      ""#..,..###"",
      ""###+### ""
    ]
  }},
  {{
    ""Id"": ""Test_Doors"",
    ""Data"": [
      ""....."",
      "".#+#."",
      "".+..."",
      "".#..."",
      "".....""
    ]
  }},
  {{
    ""Id"": ""Pillar Room"",
    ""Data"": [
      ""#####+#####"",
      ""#...._....#"",
      ""#.##._.##.#"",
      ""#.#.._..#.#"",
      ""#...___...#"",
      ""+____d____+"",
      ""#...___...#"",
      ""#.#.._..#.#"",
      ""#.##._.##.#"",
      ""#...._....#"",
      ""#####+#####""
    ]
  }},
  {{
    ""Id"": ""Intersection"",
    ""Data"": [
      ""#####+#####"",
      ""#.........#"",
      ""#...#.#...#"",
      ""#..##.##..#"",
      ""#....$....#"",
      ""#..##.##..#"",
      ""#...#.#...#"",
      ""#.........#"",
      ""#####+#####""
    ]
  }},
  {{
    ""Id"": ""Round Turret Pond"",
    ""Data"": [
      ""  ##+##  "",
      ""##.._..##"",
      ""#.='='=.#"",
      ""#.'~~~'.#"",
      ""+_=~T~=.+"",
      ""#.'~~~'.#"",
      ""#.='='=.#"",
      ""##.._..##"",
      ""  ##+##  ""
    ]
  }},
  {{
    ""Id"": ""Corridor with Ducts"",
    ""Data"": [
      ""#+#   "",
      ""#.####"",
      ""#._''#"",
      ""#.##'#"",
      ""#.#*'#"",
      ""#.##_#"",
      ""#.=''#"",
      ""#._''+"",
      ""#.=''#"",
      ""#.##_#"",
      ""#.#*'#"",
      ""#.##'#"",
      ""#._''#"",
      ""#.####"",
      ""#+#   ""
    ]
  }},
  {{
    ""Id"": ""Tank Room"",
    ""Data"": [
      ""#######+#######"",
      ""#.............#"",
      ""#.*.*.*.*.*.*.#"",
      ""+.............+"",
      ""#.*.*.*.*.*.*.#"",
      ""#.............#"",
      ""#######+#######""
    ]
  }},
  {{
    ""Id"": ""5x5 Quad Service"",
    ""Data"": [
      ""###+###"",
      ""#.....#"",
      ""#.*.*.#"",
      ""+.....+"",
      ""#.*.*.#"",
      ""#.....#"",
      ""#######""
    ]
  }},
  {{
    ""Id"": ""7x2 Data"",
    ""Data"": [
      ""####+####"",
      ""+..d.d..+"",
      ""#d.....d#"",
      ""####+####""
    ]
  }},
  {{
    ""Id"": ""7x3 Treasure"",
    ""Data"": [
      ""####+####"",
      ""#.......#"",
      ""+.t...t.+"",
      ""#.......#"",
      ""####+####""
    ]
  }},
  {{
    ""Id"": ""3x5 Horizontal Hall"",
    ""Data"": [
      ""#####"",
      ""#...#"",
      ""#___#"",
      ""+...+"",
      ""#___#"",
      ""#...#"",
      ""#####""
    ]
  }},
  {{
    ""Id"": ""Corner Hall"",
    ""Data"": [
      ""#####"",
      ""#___+"",
      ""#_###"",
      ""#_#  "",
      ""#_#  "",
      ""#+#  ""
    ]
  }},
  {{
    ""Id"": ""5x7 Services"",
    ""Data"": [
      ""###+###"",
      ""#.....#"",
      ""#.*.*.#"",
      ""#.....#"",
      ""+..*..+"",
      ""#.....#"",
      ""#.*.*.#"",
      ""#.....#"",
      ""#######""
    ]
  }},
  {{
    ""Id"": ""5x7 Core"",
    ""Data"": [
      ""###+###"",
      ""#.....#"",
      ""#.T.d.#"",
      ""#.....#"",
      ""+..C..+"",
      ""#.....#"",
      ""#.d.T.#"",
      ""#.....#"",
      ""###+###""
    ]
  }},
  {{
    ""Id"": ""15x9 Swimming Pool"",
    ""Data"": [
      ""#+######+######+#"",
      ""#'''''''''''''''#"",
      ""#'''''''''''''''#"",
      ""#''~~~~~~~~~~~''#"",
      ""#''~~~~~~~~~~~''#"",
      ""+''~~~~~~~~~___'+"",
      ""#''~~~~~~~~~~~''#"",
      ""#''~~~~~~~~~~~''#"",
      ""#'''''''''''''''#"",
      ""#'''''''''''''''#"",
      ""#+######+######+#""
    ]
  }},
  {{
    ""Id"": ""5x5 NS Bridge"",
    ""Data"": [
      ""###+###"",
      ""#==_==#"",
      ""#~~_~~#"",
      ""#~~_~~#"",
      ""#~~_~~#"",
      ""#==_==#"",
      ""###+###""
    ]
  }},
  {{
    ""Id"": ""Side Cage"",
    ""Data"": [
      ""#+#"",
      ""#_####"",
      ""#....#"",
      ""#XXX.#"",
      ""#''X.#"",
      ""+''X.#"",
      ""#''X.#"",
      ""#XXX.#"",
      ""#....#"",
      ""##+###""
    ]
  }},
  {{
    ""Id"": ""3x7 Center Treasure"",
    ""Data"": [
      ""##+##"",
      ""#...#"",
      ""+...+"",
      ""#t't#"",
      ""#'_'#"",
      ""#t't#"",
      ""+...+"",
      ""#...#"",
      ""##+##""
    ]
  }},
  {{
    ""Id"": ""Data Warehouse"",
    ""maxCores"": 1,
    ""Data"": [
      ""################"",
      ""#..............#"",
      ""#..dddddddddd..#"",
      ""#..dddddddddd..#"",
      ""#...,.....,....#"",
      ""+...,,,,,,,,,..+"",
      ""#.....,.....,..#"",
      ""#..dddddddddd..#"",
      ""#..dddddddddd..#"",
      ""#..............#"",
      ""################""
    ]
  }},
  {{
    ""Id"": ""Threadpool"",
    ""maxCores"": 1,
    ""Data"": [
      ""########+########"",
      ""#...............#"",
      ""+......._.......+"",
      ""#...~~~~_~~~~...#"",
      ""#...~~~~_~~~~...#"",
      ""#...~~~~~~~~~...#"",
      ""#...~~~~~~~~~...#"",
      ""#...~~~~~~~~~...#"",
      ""#...~~~~~~~~~...#"",
      ""#...~~~~~~~~~...#"",
      ""#...~~~~~~~~~...#"",
      ""#...~~~~~~~~~...#"",
      ""#...............#"",
      ""#...............#"",
      ""#################""
    ]
  }},
  {{
    ""Id"": ""Security Checkpoint"",
    ""maxCores"": 1,
    ""Data"": [
      ""#########+#########"",
      ""+..+...........+..+"",
      ""#..#..XXX'XXX..#..#"",
      ""#XX#..X.....X..#XX#"",
      ""#..X..'.....'..X..#"",
      ""#..+..X.....X..+..#"",
      ""#.##..XXX'XXX..##.#"",
      ""#..X...........X..#"",
      ""#..X...........X..#"",
      ""#########+#########""
    ]
  }},
  {{
    ""Id"": ""Operating System Kernel"",
    ""minCores"": 2,
    ""maxCores"": 4,
    ""Data"": [
      ""   #####################"",
      ""  #.,...X..,......X..,..#"",
      "" #.,,......,.........,...#"",
      ""#..,XX....#XXXXX#....XX...#"",
      ""###,,X,,,.X.....X.,,,X,,###"",
      ""#....X..,...X.X...,..X....#"",
      ""#XXXXX.,,,,,.#.,,,,..XXXXX#"",
      ""#....X.,....X.X...,..X....#"",
      ""###,,X,,..X.....X.,,,X,,###"",
      ""#...XX....#XXXXX#....XX,..#"",
      "" #..,......,...,,,.....,.#"",
      ""  #.,...X..,.....,X....,#"",
      ""   ###+######+######+###""
    ]
  }},
  {{
    ""Id"": ""Connector Bus"",
    ""Data"": [
      ""###+###"",
      ""#.,.,.#"",
      ""+.,.,.#"",
      ""#.,.,.#"",
      ""##,.,##"",
      ""#.,.,.#"",
      ""#.,.,.+"",
      ""#.,.,.#"",
      ""##,.,##"",
      ""#.,.,.#"",
      ""+.,.,.#"",
      ""#.,.,.#"",
      ""###+###""
    ]
  }},
  {{
    ""Id"": ""Narrow Junction"",
    ""Data"": [
      ""#+###"",
      ""+..~#"",
      ""#~..+"",
      ""###+#""
    ]
  }},
  {{
    ""Id"": ""3x7 Data Lake"",
    ""Data"": [
      ""##+##"",
      ""#d..#"",
      ""+...+"",
      ""#..~#"",
      ""#.~~#"",
      ""#.~~#"",
      ""+..~#"",
      ""#d..#"",
      ""##+##""
    ]
  }},
  {{
    ""Id"": ""3x3 Treasure"",
    ""Data"": [
      ""#####"",
      ""#...#"",
      ""#.t.#"",
      ""#...#"",
      ""##+##""
    ]
  }},
  {{
    ""Id"": ""1x2 Data Closet"",
    ""Data"": [
      ""###"",
      ""#d#"",
      ""#.#"",
      ""#+#""
    ]
  }},
  {{
    ""Id"": ""1x7 Hallway"",
    ""Data"": [
      ""#+#"",
      ""#.#"",
      ""#'#"",
      ""+.+"",
      ""#'#"",
      ""+.+"",
      ""#'#"",
      ""#.#"",
      ""#+#""
    ]
  }},
  {{
    ""Id"": ""3x5 Hub Room"",
    ""Data"": [
      ""##+##"",
      ""+...+"",
      ""#...#"",
      ""+...+"",
      ""#...#"",
      ""+...+"",
      ""##+##""
    ]
  }},
  {{
    ""Id"": ""Water Pond Room"",
    ""Data"": [
      ""######+####"",
      ""+.........+"",
      ""#.~.~.~.~.#"",
      ""+.........+"",
      ""#.~.~.~.~.#"",
      ""+.........+"",
      ""##+########""
    ]
  }},
  {{
    ""Id"": ""3x3 2 North Doors"",
    ""Data"": [
      ""#+#+#"",
      ""#.*.#"",
      ""#...#"",
      ""+...+"",
      ""##+##""
    ]
  }},
  {{
    ""Id"": ""3x7 Data Peninsula"",
    ""Data"": [
      ""#####"",
      ""#~~~#"",
      ""#~d~#"",
      ""#~_~#"",
      ""#'''#"",
      ""#...#"",
      ""#...+"",
      ""#...#"",
      ""##+##""
    ]
  }},
  {{
    ""Id"": ""3x4 Grates"",
    ""Data"": [
      ""##+##"",
      ""#...#"",
      ""#._.#"",
      ""#._.#"",
      ""#...+"",
      ""##+##""
    ]
  }},
  {{
    ""Id"": ""Widening Corridor"",
    ""Data"": [
      ""  ###+###  "",
      "" ##.....## "",
      ""##..___..##"",
      ""#...._....#"",
      ""##+#####+##""
    ]
  }},
  {{
    ""Id"": ""T Corridor with Water"",
    ""Data"": [
      "" ###+### "",
      "" #~...~# "",
      ""##.....##"",
      ""#...~...#"",
      ""#+#XXX#+#""
    ]
  }},
  {{
    ""Id"": ""6x7 Barrier Room"",
    ""Data"": [
      ""##+##+##"",
      ""#......#"",
      ""#......#"",
      ""#'XXXX'#"",
      ""+_====_+"",
      ""#'XXXX'#"",
      ""#......#"",
      ""#......#"",
      ""##+##+##""
    ]
  }},
  {{
    ""Id"": ""T Corridor"",
    ""Data"": [
      ""   ##+##   "",
      ""   #=_=#   "",
      ""   #._.#   "",
      ""####._.####"",
      ""#=...=...=#"",
      ""+___=_=___+"",
      ""#=.......=#"",
      ""#####+#####""
    ]
  }},
  {{
    ""Id"": ""3x2 Service Locker"",
    ""Data"": [
      ""#####"",
      ""#..*#"",
      ""#*..#"",
      ""##+##""
    ]
  }},
  {{
    ""Id"": ""3x8 Partitioned Room"",
    ""Data"": [
      ""##+##"",
      ""#...#"",
      ""#._.#"",
      ""+...+"",
      ""###.#"",
      ""#...#"",
      ""#._.#"",
      ""#._.#"",
      ""#...#"",
      ""##+##""
    ]
  }},
  {{
    ""Id"": ""Narrow Hall"",
    ""Data"": [
      ""#########"",
      ""+.......+"",
      ""#########""
    ]
  }},
  {{
    ""Id"": ""5x1 Hall"",
    ""Data"": [
      ""#######"",
      ""+_____+"",
      ""#######""
    ]
  }},
  {{
    ""Id"": ""One Block Junction"",
    ""Data"": [
      ""#+#"",
      ""+_+"",
      ""#+#""
    ]
  }},
  {{
    ""Id"": ""Large Hall"",
    ""Data"": [
      ""##+#+##+##+#+##"",
      ""#.............#"",
      ""+.............+"",
      ""#.............#"",
      ""##+#+##+##+#+##""
    ]
  }},
  {{
    ""Id"": ""7x5 Service Room"",
    ""Data"": [
      ""####+####"",
      ""#.......#"",
      ""#..*_*..#"",
      ""+.._=_..+"",
      ""#..*_*..#"",
      ""#.......#"",
      ""####+####""
    ]
  }},
  {{
    ""Id"": ""Force Field Room"",
    ""Data"": [
      "" #####+##### "",
      "" #.........# "",
      ""###='='='=###"",
      ""#___________#"",
      ""###='='='=###"",
      "" #.........# "",
      "" #####+##### ""
    ]
  }},
  {{
    ""Id"": ""13x5 Laser Maze"",
    ""Data"": [
      ""###+###+###+###"",
      ""#...X...X...X.#"",
      ""#.X.X.X.X.X.X.#"",
      ""+.X...X...X...+"",
      ""#.X.X.X.X.X.X.#"",
      ""#...X...X...X.#"",
      ""###+###+###+###""
    ]
  }},
  {{
    ""Id"": ""Mini-Wellhead 1 Core"",
    ""Data"": [
      ""   ##+##   "",
      ""  #'''''#  "",
      "" #_....._# "",
      ""#'.=====.'#"",
      ""#'.='''=.'#"",
      ""+'.='C'=.'+"",
      ""#'.='''=.'#"",
      ""#'.=====.'#"",
      "" #_....._# "",
      ""  #'''''#  "",
      ""   ##+##   ""
    ]
  }},
  {{
    ""Id"": ""Wellhead Quad Core"",
    ""Data"": [
      ""    #####+#####    "",
      ""   #...........#   "",
      ""  #..*.......*..#  "",
      "" #...............# "",
      ""#......d,,,d......#"",
      ""#.*......,......*.#"",
      ""#.....##.,.##.....#"",
      ""#...d.#..,..#..d..#"",
      ""#...,...C,C....,..#"",
      ""+...,,,,,T,,,,,,..+"",
      ""#...,...C,C....,..#"",
      ""#...d.#..,..#..d..#"",
      ""#.....##.,.##.....#"",
      ""#.*......,......*.#"",
      ""#......d,,,d......#"",
      "" #...............# "",
      ""  #..*.......*..#  "",
      ""   #...........#   "",
      ""    #####+#####    ""
    ]
  }},  
  {{
    ""Id"": ""Wellhead One Core"",
    ""Data"": [
      ""    #####+#####    "",
      ""   #...........#   "",
      ""  #..*.......*..#  "",
      "" #...............# "",
      ""#......d,,,d......#"",
      ""#.*......,......*.#"",
      ""#.....##.,.##.....#"",
      ""#...d.#..,..#..d..#"",
      ""#...,....,.....,..#"",
      ""+...,,,,,C,,,,,,..+"",
      ""#...,....,.....,..#"",
      ""#...d.#..,..#..d..#"",
      ""#.....##.,.##.....#"",
      ""#.*......,......*.#"",
      ""#......d,,,d......#"",
      "" #...............# "",
      ""  #..*.......*..#  "",
      ""   #...........#   "",
      ""    #####+#####    ""
    ]
  }},
  {{
    ""Id"": ""Junction Room"",
    ""Data"": [
      ""  #####"",
      ""###...###"",
      ""+.......+"",
      ""###...###"",
      ""  #####""
    ]
  }},
  {{
    ""Id"": ""Side Chamber"",
    ""Data"": [
      ""#######"",
      ""#..t..####"",
      ""#........+"",
      ""#..t..####"",
      ""#######""
    ]
  }},
  {{
    ""Id"": ""Boss Side Core"",
    ""Data"": [
      ""   ##+##   "",
      ""  ##===##  "",
      "" ##.....## "",
      "" #T_~_~_T# "",
      ""##~_~C~_~##"",
      ""#~~_~~~_~~#"",
      ""#T~.....~T#"",
      ""+._..T.._.+"",
      ""#.~~...~~.#"",
      ""#.~~~~~~~.#"",
      ""#.~=====~.#"",
      ""#.~=...=~.#"",
      ""+.__...__.+"",
      ""#T~=...=~T#"",
      ""#####+#####""
    ]
  }},
  {{
    ""Id"": ""Vertical Hall"",
    ""Data"": [
      ""#+#"",
      ""#.#"",
      ""#.#"",
      ""#.#"",
      ""#.#"",
      ""#.#"",
      ""#+#""
    ]
  }},
  {{
    ""Id"": ""Short Vertical Hall"",
    ""Data"": [
      ""#+#"",
      ""#.#"",
      ""#.#"",
      ""#.#"",
      ""#+#""
    ]
  }},
  {{
    ""Id"": ""Shallow Airlock"",
    ""Data"": [
      ""#+#"",
      ""#.#"",
      ""+.+"",
      ""#.#"",
      ""#+#""
    ]
  }},
  {{
    ""Id"": ""Short Horizontal Hall"",
    ""Data"": [
      ""#####"",
      ""+...+"",
      ""#####""
    ]
  }},
  {{
    ""Id"": ""Horizontal Water Maze"",
    ""Data"": [
      ""###+###+###"",
      ""#~~__~____#"",
      ""+______~~_+"",
      ""#~__~~__~_#"",
      ""###+###+###""
    ]
  }},
  {{
    ""Id"": ""Mini Room with Grates"",
    ""Data"": [
      ""##+##"",
      ""#...#"",
      ""+._.+"",
      ""#._.#"",
      ""+._.+"",
      ""#...#"",
      ""##+##""
    ]
  }},
  {{
    ""Id"": ""10 Silos"",
    ""Data"": [
      ""####+########+####"",
      ""#................#"",
      ""#.__.__.__.__.__.#"",
      ""#.__.__.__.__.__.#"",
      ""+................+"",
      ""#.__.__.__.__.__.#"",
      ""#.__.__.__.__.__.#"",
      ""#................#"",
      ""####+########+####""
    ]
  }},
  {{
    ""Id"": ""Water Trench Add-on"",
    ""Data"": [
      ""#=========#"",
      ""#~_~~~~~~~#"",
      ""#~_______~#"",
      ""#~~~~~~~_~#"",
      ""#=========#"",
      ""#.........#"",
      ""#.........#"",
      ""#####+#####""
    ]
  }},
  {{
    ""Id"": ""Mini Room with Twin Data Stores"",
    ""Data"": [
      ""##+##"",
      ""#d.d#"",
      ""+...+"",
      ""#...#"",
      ""+...+"",
      ""#...#"",
      ""##+##""
    ]
  }},  {{
    ""Id"": ""3x5 Dual Data"",
    ""Data"": [
      ""##+##"",
      ""#d.d#"",
      ""#...#"",
      ""+...+"",
      ""#...#"",
      ""#...#"",
      ""##+##""
    ]
  }},
  {{
    ""Id"": ""Mini Room with Treasure"",
    ""Data"": [
      ""#####"",
      ""#...#"",
      ""#.t.#"",
      ""#...#"",
      ""#...#"",
      ""#...#"",
      ""##+##""
    ]
  }},
  {{
    ""Id"": ""Mini Room with Services"",
    ""Data"": [
      ""##+##"",
      ""#*.*#"",
      ""+...+"",
      ""#*.*#"",
      ""+...+"",
      ""#*.*#"",
      ""##+##""
    ]
  }},
  {{
    ""Id"": ""Room with Services"",
    ""Data"": [
      ""###+###"",
      ""#.....#"",
      ""#.*'*.#"",
      ""#.'_'.#"",
      ""#.*'*.#"",
      ""+.'_'.#"",
      ""#.*'*.#"",
      ""#.....#"",
      ""###+###""
    ]
  }},
  {{
    ""Id"": ""5x4 Vertical Hallway"",
    ""Data"": [
      ""###+###"",
      ""+.._..+"",
      ""#.._..#"",
      ""#.._..#"",
      ""+.._..+"",      
      ""###+###""
    ]
  }},
  {{
    ""Id"": ""Room with Trapped Goodies"",
    ""Data"": [
      ""#######"",
      ""#_'''_#"",
      ""#'t't'#"",
      ""#''_''#"",
      ""#'d'd'#"",
      ""#_'''_#"",
      ""#=====#"",
      ""#.....#"",
      ""###+###""
    ]
  }},
  {{
    ""Id"": ""Bridge Room End"",
    ""Data"": [
      ""##+##+##"",
      ""#......#"",
      ""+.____.#"",
      ""#._''_.#"",
      ""#._''_.="",
      ""+._''_._"",
      ""#._''_.="",
      ""#._''_.#"",
      ""+.____.#"",
      ""#......#"",
      ""##+##+##""
    ]
  }},
  {{
    ""Id"": ""Bridge Room Segment"",
    ""Data"": [
      ""#######"",
      ""   T   "",
      ""       "",
      ""='='='="",
      ""......."",
      ""_______"",
      ""......."",
      ""='='='="",
      ""       "",
      ""   T   "",
      ""#######""
    ]
  }},
  {{
    ""Id"": ""Widening Segment"",
    ""Data"": [
      ""   ###+#"",
      ""  ##=__="",
      "" ##....'"",
      ""##..'''="",
      ""#=.''__'"",
      ""+'''___'"",
      ""#=.''__'"",
      ""##..'''="",
      "" ##....'"",
      ""  ##=__="",
      ""   ###+#""
    ]
  }},
  {{
    ""Id"": ""Wide Water Bridge Segment"",
    ""Data"": [
      ""######"",
      ""~~~~~~"",
      ""~~~~~~"",
      ""~~~~~~"",
      "".....'"",
      ""=___='"",
      "".....'"",
      ""~~~~~~"",
      ""~~~~~~"",
      ""~~~~~~"",
      ""######""
    ]
  }},
  {{
    ""Id"": ""Airlock"",
    ""Data"": [
      ""##+##"",
      ""#...#"",
      ""+...+"",
      ""#...#"",
      ""##+##""
    ]
  }},
  {{
    ""Id"": ""Vertical Airlock"",
    ""Data"": [
      ""##+##"",
      ""#._.#"",
      ""#._.#"",
      ""#._.#"",
      ""##+##""
    ]
  }},
  {{
    ""Id"": ""Horizontal Airlock"",
    ""Data"": [
      ""#####"",
      ""#...#"",
      ""+___+"",
      ""#...#"",
      ""#####""
    ]
  }},
  {{
    ""Id"": ""AirlockHForceField"",
    ""Data"": [
      ""#####"",
      ""#_=_#"",
      ""+.=.+"",
      ""#_=_#"",
      ""#####""
    ]
  }},
  {{
    ""Id"": ""Elbow Corridor"",
    ""Data"": [
      ""#+#"",
      ""#.#"",
      ""#.########"",
      ""#........#"",
      ""########.#"",
      ""       #.#"",
      ""       #+#""
    ]
  }},
  {{
    ""Id"": ""Small Arena"",
    ""Data"": [
      ""#######+#######"",
      ""#.............#"",
      ""#...~~'.'~~...#"",
      ""##..~.....~..##"",
      "" #..'.....'..# "",
      "" +.....~.....+ "",
      "" #..'.....'..# "",
      ""##..~.....~..##"",
      ""#...~~'.'~~...#"",
      ""#.............#"",
      ""#######+#######""
    ]
  }},
  {{
    ""Id"": ""Long Corridor"",
    ""Data"": [
      ""###+#####+#####+###"",
      ""#.....=.....=.....#"",
      ""+_____=_____=_____+"",
      ""#.....=.....=.....#"",
      ""###+#####+#####+###""
    ]
  }},
  {{
    ""Id"": ""Barrier U Hall"",
    ""Data"": [
      ""##+##       ##+##"",
      ""#...#       #...#"",
      ""#...#       #...#"",
      ""#X.X#       #X.X#"",
      ""+...#########...+"",
      ""#....X.....X....#"",
      ""#...............#"",
      ""#....X.....X....#"",
      ""####+#######+####""
    ]
  }},  
  {{
    ""Id"": ""Dog Bone Service Hall"",
    ""Data"": [
      ""####+##       ##+####"",
      ""#.....#       #.....#"",
      ""#.*...##+###+##...*.#"",
      ""#...................#"",
      ""+...*..*..*..*..*...+"",
      ""#...................#"",
      ""#.*...##+###+##...*.#"",
      ""#.....#       #.....#"",
      ""####+##       ##+####""
    ]
  }},
  {{
    ""Id"": ""Gated Arena"",
    ""Data"": [
      ""######+######"",
      ""#...X...X...#"",
      ""+...X...X...+"",
      ""#.,,XX_XX,,.#"",
      ""#.,.......,.#"",
      ""#,,.......,,#"",
      ""#.,.......,.#"",
      ""#.,,XX_XX,,.#"",
      ""+...X...X...+"",
      ""#...X...X...#"",
      ""######+######""
    ]
  }},
  {{
    ""Id"": ""L Pool"",
    ""Data"": [
      ""######+######"",
      ""#...........#"",
      ""#.~~~._.~~~.#"",
      ""#.~~~._.~~~.#"",
      ""#.~~~._.~~~.#"",
      ""#....'''~~~.#"",
      ""+.___'d'~~~.+"",
      ""#....'''~~~.#"",
      ""#.~~~~~~~~~.#"",
      ""#.~~~~~~~~~.#"",
      ""#.~~~~~~~~~.#"",
      ""#...........#"",
      ""######+######""
    ]
  }},
  {{
    ""Id"": ""Data Beach"",
    ""Data"": [
      ""#####+###+#####"",
      ""#.............#"",
      ""#.d.........d.#"",
      ""+....=====....+"",
      ""#...==~~~==...#"",
      ""#...=~~~~~=...#"",
      ""###+#######+###""
    ]
  }},
  {{
    ""Id"": ""Turret Hall"",
    ""Data"": [
      ""#####+###+#####"",
      ""#T....===....T#"",
      ""+.....=C=.....+"",
      ""#T....===....T#"",
      ""#####+###+#####""
    ]
  }},
  {{
    ""Id"": ""Water Maze"",
    ""Data"": [
      ""##+###+###+##"",
      ""#~.~...~.~.~#"",
      ""+..~~.~~....+"",
      ""#~.~..~..~~~#"",
      ""#~.~.~~~.~..+"",
      ""+..~.~T~...~#"",
      ""#~...~~~.~.~#"",
      ""#~.~.....~.~#"",
      ""+..~~~.~~~..+"",
      ""#~..~....~.~#"",
      ""##+###+###+##""
    ]
  }},
  {{
    ""Id"": ""Two-Sided Trench"",
    ""Data"": [
      ""#############"",
      ""+...=~~~=...#"",
      ""#...'~~~'...+"",
      ""+...=~~~=...#"",
      ""#...'~~~'...+"",
      ""+...=~~~=...#"",
      ""#############""
    ]
  }},
  {{
    ""Id"": ""5x3 Vertical Hallway"",
    ""Data"": [
      ""###+###"",
      ""#_..._#"",
      ""#_..._#"",
      ""#_..._#"",
      ""###+###""
    ]
  }},
  {{
    ""Id"": ""Quad Service Room"",
    ""Data"": [
      ""##+###+###+##"",
      ""#...........#"",
      ""#...*...*...#"",
      ""+...........+"",
      ""#.*.......*.#"",
      ""#...........#"",
      ""##+###+###+##""
    ]
  }},
  {{
    ""Id"": ""Large Caution Room"",
    ""Data"": [
      ""#######+#######"",
      ""#.=T=_..._=T=.#"",
      ""#.===.===.===.#"",
      ""#_..._=d=_..._#"",
      ""#.===.===.===.#"",
      ""+.=d=_..._=d=.+"",
      ""#.===.===.===.#"",
      ""#_..._=d=_..._#"",
      ""#.===.===.===.#"",
      ""#.=T=_..._=T=.#"",
      ""#######+#######""
    ]
  }},
  {{
    ""Id"": ""BossChamber"",
    ""Data"": [
      ""######+#XXX#+######"",
      ""#........'........#"",
      ""#.~_~T.X.'.X.T~_~.#"",
      ""#._T~~.X.'.X.~~T_.#"",
      ""#.~~~~.X.'.X.~~~~.#"",
      ""#.~~~_.._'_.._~~~.#"",
      ""#......._'_.......#"",
      ""#..XXX..'_'..XXX..#"",
      ""_=_=_='''='''=_=_=#"",
      ""'''''''_=T=_''''''+"",
      ""_=_=_='''='''=_=_=#"",
      ""#..XXX..'_'..XXX..#"",
      ""#......._'_.......#"",
      ""#.~~~_.._'_.._~~~.#"",
      ""#.~~~~.X.'.X.~~~~.#"",
      ""#._T~~.X.'.X.~~T_.#"",
      ""#.~_~T.X.'.X.T~_~.#"",
      ""#........'........#"",
      ""######+#XXX#+######""
    ]
  }},
  {{
    ""Id"": ""BossSideChamber"",
    ""Data"": [
      ""#########"",
      ""#T.....T#"",
      ""#..._...#"",
      ""#..=_=..#"",
      ""#.__C__.#"",
      ""#..=_=..#"",
      ""#..._...#"",
      ""#.......#""
    ]
  }},  
  {{
    ""Id"": ""Core Trap Room"",
    ""Data"": [
      ""##+###+##"",
      ""#d..#..d#"",
      ""#.......#"",
      ""#...C...#"",
      ""#.......#"",
      ""#T..#..T#"",
      ""#########""
    ]
  }},
  {{
	""Id"": ""Tutorial_Start"",
	""Data"": [
	  "" ##### "",
	  ""##...##"",
	  ""#..?..#"",
	  ""+.....+"",
	  ""#.....#"",
	  ""##...##"",
	  "" ##+## "",
	],
	""Mapping"": [
	{{
		""Char"": ""?"",
		""ObjId"": ""HELP_WELCOME"",
		""ObjType"": ""Help""
	}}
	]
  }},
  {{
	""Id"": ""Tutorial_CharacterSelect1"",
	""Data"": [
	  ""##### "",
	  ""#...# "",
	  ""#.1.# "",
	  ""#___# "",
	  ""#=3=##"",
	  ""......"",
	  ""......"",
	  ""......"",
	  ""#=4=##"",
	  ""#___# "",
	  ""#.2.# "",
	  ""#...# "",
	  ""##### ""
	],
	""Mapping"": [
	{{
		""Char"": ""1"",
		""ObjId"": ""ACTOR_PLAYER_LOGISTICS"",
		""ObjType"": ""CharacterSelect""
	}},
	{{
		""Char"": ""2"",
		""ObjId"": ""ACTOR_PLAYER_FORECAST"",
		""ObjType"": ""CharacterSelect""
	}},
	{{
		""Char"": ""3"",
		""ObjId"": ""HELP_ACTOR_PLAYER_LOGISTICS"",
		""ObjType"": ""Help""
	}},
	{{
		""Char"": ""4"",
		""ObjId"": ""HELP_ACTOR_PLAYER_FORECAST"",
		""ObjType"": ""Help""
	}}
	]
  }},
  {{
	""Id"": ""Tutorial_CharacterSelect2"",
	""Data"": [
	  ""##### "",
	  ""#...# "",
	  ""#.1.# "",
	  ""#___# "",
	  ""#=3=##"",
	  ""......"",
	  ""......"",
	  ""......"",
	  ""#=4=##"",
	  ""#___# "",
	  ""#.2.# "",
	  ""#...# "",
	  ""##### ""
	],
	""Mapping"": [
	{{
		""Char"": ""1"",
		""ObjId"": ""ACTOR_PLAYER_MALWARE"",
		""ObjType"": ""CharacterSelect""
	}},
	{{
		""Char"": ""2"",
		""ObjId"": ""ACTOR_PLAYER_SEARCH"",
		""ObjType"": ""CharacterSelect""
	}},
	{{
		""Char"": ""3"",
		""ObjId"": ""HELP_ACTOR_PLAYER_MALWARE"",
		""ObjType"": ""Help""
	}},
	{{
		""Char"": ""4"",
		""ObjId"": ""HELP_ACTOR_PLAYER_SEARCH"",
		""ObjType"": ""Help""
	}}
	]
  }},
  {{
	""Id"": ""Tutorial_CharacterSelect3"",
	""Data"": [
	  ""##### "",
	  ""#...# "",
	  ""#.1.# "",
	  ""#___# "",
	  ""#=3=##"",
	  ""......"",
	  ""......"",
	  ""......"",
	  ""#=4=##"",
	  ""#___# "",
	  ""#.2.# "",
	  ""#...# "",
	  ""##### ""
	],
	""Mapping"": [
	{{
		""Char"": ""1"",
		""ObjId"": ""ACTOR_PLAYER_ANTIVIRUS"",
		""ObjType"": ""CharacterSelect""
	}},
	{{
		""Char"": ""2"",
		""ObjId"": ""ACTOR_PLAYER_GAME"",
		""ObjType"": ""CharacterSelect""
	}},
	{{
		""Char"": ""3"",
		""ObjId"": ""HELP_ACTOR_PLAYER_ANTIVIRUS"",
		""ObjType"": ""Help""
	}},
	{{
		""Char"": ""4"",
		""ObjId"": ""HELP_ACTOR_PLAYER_GAME"",
		""ObjType"": ""Help""
	}}
	]
  }},  {{
	""Id"": ""Tutorial_End"",
    ""IsInvulnerable"": true,
	""Data"": [
	  "" ####"",
	  "" #C?#"",
	  ""##..="",
	  "">|__="",
	  ""##..="",
	  "" #C.#"",
	  "" ####"",
	],
	""Mapping"": [
	{{
		""Char"": ""?"",
		""ObjId"": ""HELP_FIREWALLS"",
		""ObjType"": ""Help""
	}}
	]
  }},
  {{
    ""Id"": ""BossEndChamber"",
    ""IsInvulnerable"": true,
    ""Data"": [
      ""  ##### "",
      ""###T=T##"",
      ""#=|.=..="",
      ""#>|'_'''"",
      ""#=|.=..="",
      ""###T=T##"",
      ""  ##### ""
    ]
  }},
  {{
    ""Id"": ""TrainSingleRoom"",
    ""Data"": [
      ""###########"",
      ""#.........#"",
      ""#.........#"",
      ""#.........#"",
      ""#.........#"",
      ""#.........#"",
      ""#.........#"",
      ""#.........#"",
      ""###########"",
    ]
  }},
  {{
    ""Id"": ""TrainDualRooms"",
    ""Data"": [
      ""###########   #####"",
      ""#.........#   #####"",
      ""#.........#   #####"",
      ""#.........#####...#"",
      ""#.........+___+...#"",
      ""#.........#####...#"",
      ""#.........#   #####"",
      ""#.........#   #####"",
      ""###########   #####"",
    ]
  }},
  {{
    ""Id"": ""TrainFriendlyFire"",
    ""Data"": [
      ""######"",
      ""#.#..#"",
      ""###..#"",
      ""######"",
    ]
  }},
  {{
    ""Id"": ""Training"",
    ""Data"": [
      ""############### ###############"",
      ""#.............# #.............#"",
      ""#...~~...~~...# #...~~...~~...#"",
      ""##..~.....~..#####..~.....~..##"",
      ""##...........#...#...........##"",
      "">|.....~.....+___+.....~.....|<"",
      ""##...........#...#...........##"",
      ""##..~.....~..#####..~.....~..##"",
      ""#...~~...~~...# #...~~...~~...#"",
      ""#.............# #.............#"",
      ""#######+####### #######+#######"",
      ""#...#*,.,*#..*# #~~~#*,.,*#D#D#"",
      ""#.t.#.,.,.#*..# #~C~#.,.,.#.#.#"",
      ""#...#.,.,.##+## #~_~#.,.,.#+#+#"",
      ""##+###,.,##...# #...##,.,##.*.#"",
      ""#T..#.,.,.#._.# #...#.,.,.#...#"",
      ""#...+.,.,.+...# #...+.,.,.+...#"",
      ""#..~#.,.,.###.# #...#.,.,.##+##"",
      ""#.~~##,.,##...# ##+###,.,##...#"",
      ""#.~~#.,.,.#._.# #_._#.,.,.#._.#"",
      ""#..~#.,.,.#._.# #_._#.,.,.#._.#"",
      ""#T..#*,.,*#...# #_._#*,.,*#...#"",
      ""##+####+####+## ##+####+####+##"",
      ""#.............###.............#"",
      ""#.............+.+.............#"",
      ""#.............###.............#"",
      ""#.............# #.............#"",
      ""############### ###############"",
    ]
  }}
]
";

        private readonly IDictionary<string, RoomData> _rooms = new Dictionary<string, RoomData>();

        public RoomDataProvider()
        {
            var prefabs = JsonConvert.DeserializeObject<JArray>(_prefabJson);
            prefabs.Each(p =>
            {
                var room = p.ToObject<RoomData>();
                _rooms[room.Id] = room;
            });
        }

        public RoomData GetRoomById(string id)
        {
            _rooms.TryGetValue(id, out var room);

            return room;
        }
    }
}
