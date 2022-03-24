using DG.Tweening;
using ExampleNamespace;
using RootMotion.FinalIK;
using Service;
using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(PlayerStateManager))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private CharacterSettingsScriptableObject characterSettingsScriptableObject;
        [SerializeField] private AimIK aimIK;
        [SerializeField] private FullBodyBipedIK fullBodyBipedIK;
        private GameService _gameService;
        private PlayerCollisionManager _playerCollisionManager;
        private PlayerStateManager _playerStateManager;
        public bool IsAlive { get; set; } = true;

        public PlayerCharacterSettings PlayerCharacterSettings =>
            characterSettingsScriptableObject.PlayerCharacterSettings;

        public PlayerCollisionManager PlayerCollisionManager => _playerCollisionManager
            ? _playerCollisionManager
            : _playerCollisionManager = GetComponent<PlayerCollisionManager>();

        public GameService GameService => _gameService
            ? _gameService
            : _gameService = ServiceManager.GetService<GameService>();

        private PlayerStateManager StateManager => _playerStateManager
            ? _playerStateManager
            : _playerStateManager = GetComponent<PlayerStateManager>();

        public void ToggleState(bool bind)
        {
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

        public void Death()
        {
            IsAlive = false;
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