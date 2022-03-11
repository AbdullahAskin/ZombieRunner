using System.Collections.Generic;
using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(PlayerAnimationController), typeof(PlayerSplineManager))]
    public class PlayerStateManager : MonoBehaviour
    {
        private PlayerBaseState _currentState;
        private PlayerAnimationController _playerAnimationController;
        private List<PlayerBaseState> _playerBaseStates;
        private PlayerSplineManager _playerSplineManager;

        public PlayerAnimationController playerAnimationController => _playerAnimationController
            ? _playerAnimationController
            : _playerAnimationController = GetComponent<PlayerAnimationController>();

        public PlayerSplineManager playerSplineManager => _playerSplineManager
            ? _playerSplineManager
            : _playerSplineManager = GetComponent<PlayerSplineManager>();

        private void Start()
        {
            _playerBaseStates = new List<PlayerBaseState>
            {
                new PlayerIdleState(this,playerSplineManager), new PlayerMovementState(this,playerSplineManager)
            };
            SwitchState<PlayerIdleState>();
        }

        private void FixedUpdate()
        {
            _currentState?.UpdateState(this);
        }

        private void OnCollisionEnter(Collision collision)
        {
            _currentState?.OnCollisionEnter(this, collision);
        }

        public void SwitchState<T>() where T : PlayerBaseState
        {
            var state = (T) _playerBaseStates.Find(x => x.GetType() == typeof(T));
            _currentState?.ExitState(this);
            _currentState = state;
            _currentState?.EnterState(this);
        }
    }
}