using Service;
using UnityEngine;

namespace TheyAreComing
{
    public class GameManager : MonoBehaviour,IGameStateObserver
    {
        //TODO Will be removed with zenject
        [SerializeField] private Player player;
        private GameService _gameService;
        private GameService gameService => _gameService ?? (_gameService = ServiceManager.GetService<GameService>());

        private void Start()=> gameService.ToggleObserver(this,true);
        private void OnDisable()=> gameService.ToggleObserver(this,false);

        public void OnGameStateChange(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Ready:
                    break;
                case GameState.Play:
                    player.Init();
                    break;
                case GameState.Fail:
                    break;
                case GameState.Won:
                    break;
            }
        }
    }
}