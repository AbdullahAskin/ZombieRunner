using DG.Tweening;
using ExampleNamespace;
using MoreMountains.NiceVibrations;
using RootMotion.FinalIK;
using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(PlayerStateManager))]
    public class Player : MonoBehaviour
    {
        public AimIK aimIK;
        [SerializeField] private CharacterSettingsScriptableObject characterSettingsScriptableObject;
        [SerializeField] private FullBodyBipedIK fullBodyBipedIK;
        private PlayerAnimationController _playerAnimationController;
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

        public PlayerAnimationController PlayerAnimationController => _playerAnimationController
            ? _playerAnimationController
            : _playerAnimationController = GetComponent<PlayerAnimationController>();

        public PlayerCollisionManager PlayerCollisionManager => _playerCollisionManager
            ? _playerCollisionManager
            : _playerCollisionManager = GetComponent<PlayerCollisionManager>();

        public ProgressBar ProgressBar => _progressBar
            ? _progressBar
            : _progressBar = GetComponent<ProgressBar>();

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
            SetAimIK(1, 0);
        }

        public void OnRevive()
        {
            IsAlive = true;
            PlayerAnimationController.ResetAnimatorToWalk();
            SetAimIK(0, 1);
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
            PlayerCollisionManager.OnHeal(PlayerCharacterSettings.MaxHealth);
        }

        private void SetAimIK(float start, float end)
        {
            DOVirtual.Float(start, end, .5f, x =>
            {
                aimIK.solver.IKPositionWeight = x;
                fullBodyBipedIK.solver.IKPositionWeight = x;
            });
        }
    }
}