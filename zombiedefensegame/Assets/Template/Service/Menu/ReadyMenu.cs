using UnityEngine;
using UnityEngine.UI;

namespace Service
{
    public class ReadyMenu : MenuBase
    {
        [SerializeField] private Button backgroundButton;
        private GameService _gameService;

        private GameService gameService =>
            _gameService ? _gameService : _gameService = ServiceManager.GetService<GameService>();

        private void Start()
        {
            menuState = MenuState.Activated;
            if (backgroundButton) backgroundButton.onClick.AddListener(OnFingerDown);
        }

        public void OnFingerDown()
        {
            Disappear();
            gameService.NotifyGameStateChange(GameState.Play);
        }
    }
}