using System;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.LevelGeneration.Properties;
using Newtonsoft.Json;

namespace MattEland.Emergence.LevelGeneration.Prefabs
{
    /// <summary>
    /// Provides prefabricated levels.
    /// </summary>
    public static class PrefabLevelProvider
    {

        public static PrefabricatedLevel GetLevel(LevelType levelType)
        {
            string json;

            switch (levelType)
            {
                case LevelType.Tutorial:
                    json = Resources.LevelTutorial;
                    break;

                case LevelType.ClientWorkstation:
                    json = Resources.Level1;
                    break;

                case LevelType.SmartFridge:
                    json = Resources.Level2;
                    break;

                case LevelType.MessagingServer:
                    json = Resources.Level3;
                    break;

                case LevelType.Bastion:
                    json = Resources.Level4;
                    break;

                case LevelType.RouterGateway:
                    json = Resources.LevelBoss;
                    break;

                case LevelType.TestDoors:
                    json = Resources.LevelDoorsTest;
                    break;

                case LevelType.Training:
                    json = Resources.LevelTraining;
                    break;

                case LevelType.TrainSingleRoom:
                    json = Resources.LevelTrainSingleRoom;
                    break;

                case LevelType.TrainTwinRooms:
                    json = Resources.LevelTrainDualRooms;
                    break;

                case LevelType.TrainFriendlyFire:
                    json = Resources.LevelTrainFriendlyFireRoom;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(levelType), $"Level type {levelType} is not supported via prefabs.");
            }

            return JsonConvert.DeserializeObject<PrefabricatedLevel>(json);
        }

    }
}