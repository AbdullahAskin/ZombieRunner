using AmazingAssets.CurvedWorld;
using DG.Tweening;
using Service;
using UnityEngine;

namespace TheyAreComing
{
    public class GameManager : MonoBehaviour, IGameStateObserver
    {
        private static Player _player;
        private static CurvedWorldController _curvedWorldController;
        private static SpawnManager _spawnManager;
        private static GameService _gameService;
        private MenuService _menuService;

        public static Player Player => _player
            ? _player
            : _player = FindObjectOfType<Player>();

        public static SpawnManager SpawnManager => _spawnManager
            ? _spawnManager
            : _spawnManager = FindObjectOfType<SpawnManager>();

        public static CurvedWorldController CurvedWorldController => _curvedWorldController
            ? _curvedWorldController
            : _curvedWorldController = FindObjectOfType<CurvedWorldController>();

        private static GameService GameService =>
            _gameService ? _gameService : _gameService = ServiceManager.GetService<GameService>();

        private MenuService MenuService =>
            _menuService ? _menuService : _menuService = ServiceManager.GetService<MenuService>();

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
                    ToggleCharacters(false);
                    DOVirtual.DelayedCall(1f, () => MenuService.GetMenu<FailMenu>().Appear());
                    break;
                case GameState.Won:
                    break;
            }
        }

        public static void ToggleCharacters(bool bind)
        {
            SpawnManager.ToggleSpawn(bind);
            Player.ToggleState(bind);
            EnemyManager.ToggleEnemies(bind);
        }

        public static void RevivePlayer()
        {
            GameService.NotifyGameStateChange(GameState.Play);
            EnemyManager.DisappearNearEnemies();
            Player.OnRevive();
        }
    }
}