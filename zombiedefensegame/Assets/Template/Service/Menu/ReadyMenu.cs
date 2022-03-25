using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Service
{
    public class ReadyMenu : MenuBase
    {
        [SerializeField] private Button backgroundButton;
        [SerializeField] private TextMeshProUGUI touchToStartText;
        private GameService _gameService;
        private bool _isClicked;
        private Tween _textFadeInOutTween;

        private GameService GameService =>
            _gameService ? _gameService : _gameService = ServiceManager.GetService<GameService>();

        private void Start()
        {
            _textFadeInOutTween = touchToStartText.DOFade(0f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
            MenuState = MenuState.Activated;
            if (backgroundButton) backgroundButton.onClick.AddListener(OnFingerDown);
        }

        public void OnFingerDown()
        {
            if (_isClicked) return;
            _isClicked = true;  
            _textFadeInOutTween?.Kill();
            Disappear();
            GameService.NotifyGameStateChange(GameState.Play);
        }
    }
}