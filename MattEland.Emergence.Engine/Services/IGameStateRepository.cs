using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MattEland.Emergence.Engine.DTOs;

namespace MattEland.Emergence.Engine.Services
{
    public interface IGameStateRepository
    {
        Task<GameState> GetStateAsync(Guid uid);
        void CacheState(GameResponse response);
        bool RemoveState(Guid uid);
        IEnumerable<Guid> GetActiveKeys();
        void ClearCache();
    }
}