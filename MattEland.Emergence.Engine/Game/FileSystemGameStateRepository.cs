using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using MattEland.Emergence.Engine.DTOs;

namespace MattEland.Emergence.Engine.Game
{
    public class FileSystemGameStateRepository : InMemoryGameStateRepository
    {
        private readonly ILogger<FileSystemGameStateRepository> _logger;
        private readonly IHostingEnvironment _env;

        public FileSystemGameStateRepository(ILogger<FileSystemGameStateRepository> logger,
                                             IHostingEnvironment env)
        {
            _logger = logger;
            _env = env;

            if (!Directory.Exists(RootPath))
            {
                Directory.CreateDirectory(RootPath);
            }
        }

        protected override void OnStateSet(Guid key, GameState newState)
        {
            base.OnStateSet(key, newState);

            Task.Run(() => WriteStateToFile(key, newState));
        }

        private void WriteStateToFile(Guid key, GameState newState)
        {
            try
            {
                var path = GetFilePath(key);

                _logger.LogInformation($"Writing JSON data for game {key} to {path}");

                var formatting = Formatting.Indented;

                File.WriteAllText(path, JsonConvert.SerializeObject(newState, formatting));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Could not log game state to disk for session {key}");
            }
        }

        private string RootPath => $"{_env.ContentRootPath}\\StateData";

        private string GetFilePath(Guid key)
        {
            return $"{RootPath}\\{key.ToString()}.json";
        }

        protected override void OnStateRemoved(Guid key)
        {
            base.OnStateRemoved(key);

            Task.Run(() => DeleteStateIfExists(key));
        }

        private void DeleteStateIfExists(Guid key)
        {
            try
            {
                var path = GetFilePath(key);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Could not delete game state from disk for session {key}");
            }
        }
        private void DeleteAllStateFiles()
        {
            try
            {
                var di = new DirectoryInfo(RootPath);

                if (di.Exists)
                {
                    foreach (var file in di.GetFiles())
                    {
                        try
                        {
                            file.Delete();
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"Could not delete file {file.FullName}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error clearing stored game state from disk");
            }
        }

        protected override void OnCacheCleared()
        {
            base.OnCacheCleared();

            Task.Run(() => DeleteAllStateFiles());
        }

        protected override Task<GameState> HandleStateNotFoundInCache(Guid key)
        {
            try
            {
                var path = GetFilePath(key);

                if (File.Exists(path))
                {
                    var contents = File.ReadAllText(path);

                    return Task.FromResult(JsonConvert.DeserializeObject<GameState>(contents));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error trying to retrieve game state for session {key}");
            }

            return base.HandleStateNotFoundInCache(key);
        }
    }
}