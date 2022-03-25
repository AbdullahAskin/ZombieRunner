using Cinemachine;
using DG.Tweening;
using TheyAreComing;
using UnityEngine;

namespace Service
{
    public class CameraService : ServiceBase,IGameStateObserver
    {
        [SerializeField] private CinemachineVirtualCamera readyCam;
        [SerializeField] private CinemachineVirtualCamera playCam;
        [SerializeField] private CinemachineVirtualCamera failCam;
        [SerializeField] private CinemachineVirtualCamera wonCam;
        private CinemachineVirtualCamera _activeCamera;

        private GameService _gameService;
        private GameService gameService => _gameService ?? (_gameService = ServiceManager.GetService<GameService>());

        private void Start()
        {
            gameService.ToggleObserver(this, true);
            SetActiveCam(readyCam);
        }

        private void OnDisable()=> gameService.ToggleObserver(this,false);

        public void OnGameStateChange(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Ready:
                    SetActiveCam(readyCam);
                    break;
                case GameState.Play:
                    SetActiveCam(playCam);
                    break;
                case GameState.Fail:
                    SetActiveCam(failCam);
                    break;
                case GameState.Won:
                    SetActiveCam(wonCam);
                    break;
            }
        }

        private void SetActiveCam(CinemachineVirtualCamera targetCam)
        {
            if (_activeCamera == targetCam) return;
            if(_activeCamera) _activeCamera.enabled = false;
            targetCam.enabled = true;
            _activeCamera = targetCam;
        }

        public void ShakeCam()
        {
            _activeCamera.transform.DOShakePosition(.25f, Vector2.one * .25f, 25, 0);
        }
    }
}