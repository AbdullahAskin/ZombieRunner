using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(PlayerAnimationController), typeof(PlayerSplineManager))]
    public class PlayerStateManager : StateManager
    {
        private PlayerAnimationController _playerAnimationController;
        private PlayerSplineManager _playerSplineManager;

        public PlayerAnimationController PlayerAnimationController => _playerAnimationController
            ? _playerAnimationController
            : _playerAnimationController = GetComponent<PlayerAnimationController>();

        public PlayerSplineManager PlayerSplineManager => _playerSplineManager
            ? _playerSplineManager
            : _playerSplineManager = GetComponent<PlayerSplineManager>();

        private void Start()
        {
            CreateStates<PlayerStateBase>(this, PlayerSplineManager);
            SwitchState<PlayerStateIdle>(0);
        }
    }
}