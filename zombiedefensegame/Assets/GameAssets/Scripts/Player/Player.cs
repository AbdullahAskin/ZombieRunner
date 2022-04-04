using DG.Tweening;
using ExampleNamespace;
using MoreMountains.NiceVibrations;
using RootMotion.FinalIK;
using Service;
using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(PlayerStateManager))]
    public class Player : MonoBehaviour
    {
        public AimIK aimIK;
        [SerializeField] private CharacterSettingsScriptableObject characterSettingsScriptableObject;
        [SerializeField] private FullBodyBipedIK fullBodyBipedIK;
        private GameService _gameService;
        private PlayerCollisionManager _playerCollisionManager;
        private PlayerMovement _playerMovement;
        private PlayerStateManager _playerStateManager;
        private ProgressBar _progressBar;
        public bool IsAlive { get; set; } = true;

        public Vector3 Position => PlayerMovement.movementPivot.position;

        public PlayerCharacterSettings PlayerCharacterSettings =>
            characterSettingsScriptableObject.PlayerCharacterSettings;

        public PlayerMovement PlayerMovement => _playerMovement
            ? _playerMovement
            : _playerMovement = GetComponent<PlayerMovement>();

        public PlayerCollisionManager PlayerCollisionManager => _playerCollisionManager
            ? _playerCollisionManager
            : _playerCollisionManager = GetComponent<PlayerCollisionManager>();

        public ProgressBar ProgressBar => _progressBar
            ? _progressBar
            : _progressBar = GetComponent<ProgressBar>();

        public GameService GameService => _gameService
            ? _gameService
            : _gameService = ServiceManager.GetService<GameService>();

        private PlayerStateManager StateManager => _playerStateManager
            ? _playerStateManager
            : _playerStateManager = GetComponent<PlayerStateManager>();

        public void ToggleState(bool bind)
        {
            ProgressBar.ToggleProgressBar(bind);
            if (bind)
            {
                StateManager.SwitchState<PlayerStateMovement>(0);
                StateManager.SwitchState<GunStateIdle>(1);
            }
            else
            {
                StateManager.SwitchState<PlayerStateEmpty>(0);
                StateManager.SwitchState<GunStateEmpty>(1);
            }
        }

        public void OnDeath()
        {
            IsAlive = false;
            MMVibrationManager.Haptic(HapticTypes.Failure);
            DOVirtual.Float(1, 0, .5f, x =>
            {
                aimIK.solver.IKPositionWeight = x;
                fullBodyBipedIK.solver.IKPositionWeight = x;
            });
            GameManager.ToggleCharacters(false);
            DOVirtual.DelayedCall(1f, () => GameService.NotifyGameStateChange(GameState.Fail));
        }
    }
}