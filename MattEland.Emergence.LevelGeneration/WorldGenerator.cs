using System;
using System.Collections.Generic;
using System.Linq;
using MattEland.Emergence.LevelGeneration.Properties;
using MattEland.Emergence.Model;
using MattEland.Emergence.Model.Entities;
using MattEland.Shared.Collections;

namespace MattEland.Emergence.LevelGeneration
{
    public static class WorldGenerator
    {

        private static RoomPlacement PlacePrefab(LevelInstruction instr, RoomDataProvider roomProvider)
            => new RoomPlacement(roomProvider.GetRoomById(instr.PrefabId), new Position(instr.X, instr.Y));

        private static Actor GetOrCreatePlayer(Actor existingPlayer, LevelData levelData)
        {
            if (existingPlayer == null) return new Actor(levelData.PlayerStart, ActorType.Player);

            existingPlayer.Pos = levelData.PlayerStart;
            return existingPlayer;
        }

        public static WorldGenerationResult GenerateMap(int levelId, Actor existingPlayer)
        {
            var roomProvider = new RoomDataProvider();
            var json = GetLevelJson(levelId);
            var levelData = LevelData.LoadFromJson(json);

            var placements = levelData.Instructions.Select(i => PlacePrefab(i, roomProvider)).ToList();

            var player = GetOrCreatePlayer(existingPlayer, levelData);

            var objects = new List<WorldObject> {player};

            // TODO: Analyze instructions to determine min/max x/y
            for (int y = -200; y < 200; y++)
            {
                for (int x = -200; x < 200; x++)
                {
                    var pos = new Position(x, y);

                    char c = ' ';
                    placements.Each(p => p.GetChar(pos, c));

                    if (c != ' ')
                    {
                        objects.Add(LevelObjectCreator.GetObject(c, pos));
                    }
                }
            }

            return new WorldGenerationResult(levelData, objects);
        }

        private static string GetLevelJson(int levelId)
        {
            switch (levelId)
            {
                case 0: return Resources.LevelTutorial;
                case 1: return Resources.Level1;
                case 2: return Resources.Level2;
                case 3: return Resources.Level3;
                case 4: return Resources.Level4;
                case 5: return Resources.LevelBoss;
                default: throw new NotSupportedException($"Level {levelId} has no data associated with it");
            }
        }

        /*
let generateMap (levelId:int, existingPlayer: Actor option) : WorldGenerationResult =
    
    printfn "Generate Map %i" levelId

    let roomProvider = new RoomDataProvider()
    let json = LevelJson.getLevelJson levelId
    let levelData = LevelData.loadDataFromJson json 

    let placements = Seq.map (fun i -> placePrefab(i, roomProvider)) levelData.Instructions

    // Spawn the player at the level start location
    let player = getOrCreatePlayer(existingPlayer, levelData)
    player.Pos <- levelData.PlayerStart

    // Any logic inside of this will be repeated every enumeration
    let result = new WorldGenerationResult(levelData, seq {
        
        printfn "Generate Map %i Sequence" levelId

        yield player
        
        // TODO: Analyze instructions to determine min/max x/y
        for y in -200..200 do
            for x in -200..200 do
                let pos = new Position(x, y)
                
                let mutable char = ' '
                for instr in placements do
                    char <- instr.getCharAtPos pos char
                    
                if char <> ' ' then yield getObjectForChar char pos
      })
    
    result
         */
    }
}