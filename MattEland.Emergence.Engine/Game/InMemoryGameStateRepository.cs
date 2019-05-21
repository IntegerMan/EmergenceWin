using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Game
{
    public class InMemoryGameStateRepository : IGameStateRepository
    {
        private readonly object _lock;
        private readonly IDictionary<Guid, GameState> _gameState;

        public InMemoryGameStateRepository()
        {
            _lock = new object();
            _gameState = new Dictionary<Guid, GameState>();
        }

        public void CacheState(GameResponse response)
        {
            if (response?.State != null)
            {

                if (response.State.IsGameOver)
                {
                    RemoveState(response.State.UID);
                }
                else
                {
                    lock (_lock)
                    {
                        _gameState[response.State.UID] = response.State;
                    }

                    OnStateSet(response.State.UID, response.State);
                }
            }
        }

        protected virtual void OnStateSet(Guid key, GameState newState)
        {
        }

        protected virtual void OnStateRemoved(Guid key)
        {
        }

        protected virtual void OnCacheCleared()
        {
        }

        protected virtual Task<GameState> HandleStateNotFoundInCache(Guid key)
        {
            return Task.FromResult<GameState>(null);
        }


        public bool RemoveState(Guid uid)
        {
            bool removed;

            lock (_lock)
            {
                removed = _gameState.Remove(uid);
            }

            if (removed)
            {
                OnStateRemoved(uid);
            }

            return removed;
        }

        public IEnumerable<Guid> GetActiveKeys()
        {
            lock (_lock)
            {
                return _gameState.Keys.ToList();
            }
        }

        public void ClearCache()
        {
            lock (_lock)
            {
                _gameState.Clear();
            }

            OnCacheCleared();
        }

        public async Task<GameState> GetStateAsync(Guid uid)
        {
            GameState state = null;
            lock (_lock)
            {
                if (_gameState.ContainsKey(uid))
                {
                    state = _gameState[uid];
                }
            }

            if (state != null)
            {
                return state;
            }

            return await HandleStateNotFoundInCache(uid);
        }
    }
}