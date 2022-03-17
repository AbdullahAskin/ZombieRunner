using System.Collections.Generic;
using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(PlayerAnimationController), typeof(PlayerSplineManager))]
    public class PlayerStateManager : StateManager
    {
        private PlayerAnimationController _playerAnimationController;
        private PlayerSplineManager _playerSplineManager;

        public PlayerAnimationController playerAnimationController => _playerAnimationController
            ? _playerAnimationController
            : _playerAnimationController = GetComponent<PlayerAnimationController>();

        public PlayerSplineManager playerSplineManager => _playerSplineManager
            ? _playerSplineManager
            : _playerSplineManager = GetComponent<PlayerSplineManager>();

        private void Start()
        {
            InitStates(new List<IStateBase>
                {new PlayerStateIdle(this, playerSplineManager), new PlayerStateMovement(this, playerSplineManager)});
            SwitchState<PlayerStateIdle>();
        }
    }
}