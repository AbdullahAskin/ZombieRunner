using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Service
{
    public class ReadyMenu : MenuBase
    {
        [SerializeField] private TextMeshProUGUI touchToStartText;
        private GameService _gameService;
        private bool _isClicked;
        private Tween _textFadeInOutTween;

        private GameService GameService =>
            _gameService ? _gameService : _gameService = ServiceManager.GetService<GameService>();

        public override void Appear()
        {
            base.Appear();
            _textFadeInOutTween = touchToStartText.DOFade(0f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        }

        public override void Disappear()
        {
            base.Disappear();
            _textFadeInOutTween?.Kill();
        }

        public void OnFingerDown()
        {
            if (_isClicked) return;
            _isClicked = true;
            Disappear();
            GameService.NotifyGameStateChange(GameState.Play);
        }
    }
}