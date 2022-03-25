using System.Collections.Generic;
using TheyAreComing;

namespace Service
{
    public class GameService : ServiceBase
    {
        private readonly List<IGameStateObserver> _gameStateObservers = new List<IGameStateObserver>();
        private GameState _currentGameState;

        public void NotifyGameStateChange(GameState gameState)
        {
            if (gameState == _currentGameState) return;
            _currentGameState = gameState;
            _gameStateObservers.ForEach(x => x.OnGameStateChange(gameState));
        }

        public void ToggleObserver(IGameStateObserver gameStateObserver, bool bind)
        {
            if (bind) _gameStateObservers.Add(gameStateObserver);
            else _gameStateObservers.Remove(gameStateObserver);
        }
    }

    public enum GameState
    {
        Ready,
        Play,
        Won,
        Fail
    }
}