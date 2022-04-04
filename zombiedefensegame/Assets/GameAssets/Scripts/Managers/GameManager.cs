using AmazingAssets.CurvedWorld;
using Service;
using UnityEngine;

namespace TheyAreComing
{
    public class GameManager : MonoBehaviour, IGameStateObserver
    {
        private static Player _player;
        private static CurvedWorldController _curvedWorldController;
        private GameService _gameService;

        public static Player Player => _player
            ? _player
            : _player = FindObjectOfType<Player>();

        public static CurvedWorldController CurvedWorldController => _curvedWorldController
            ? _curvedWorldController
            : _curvedWorldController = FindObjectOfType<CurvedWorldController>();

        private GameService GameService =>
            _gameService ? _gameService : _gameService = ServiceManager.GetService<GameService>();

        private void Start()
        {
            GameService.ToggleObserver(this, true);
        }

        private void OnDisable()
        {
            GameService.ToggleObserver(this, false);
        }

        public void OnGameStateChange(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Ready:
                    break;
                case GameState.Play:
                    ToggleCharacters(true);
                    break;
                case GameState.Fail:
                    break;
                case GameState.Won:
                    break;
            }
        }

        public static void ToggleCharacters(bool bind)
        {
            Player.ToggleState(bind);
            EnemyManager.ToggleEnemies(bind);
        }
    }
}