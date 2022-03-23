using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(GunGuide))]
    [RequireComponent(typeof(PlayerAnimationController), typeof(PlayerSplineManager))]
    public class PlayerStateManager : StateManager
    {
        private GunGuide _gunGuide;
        private GunManager _gunManager;
        private Player _player;
        private PlayerAnimationController _playerAnimationController;
        private PlayerSplineManager _playerSplineManager;

        public PlayerAnimationController PlayerAnimationController => _playerAnimationController
            ? _playerAnimationController
            : _playerAnimationController = GetComponent<PlayerAnimationController>();

        public Player Player => _player
            ? _player
            : _player = GetComponent<Player>();

        public PlayerSplineManager PlayerSplineManager => _playerSplineManager
            ? _playerSplineManager
            : _playerSplineManager = GetComponent<PlayerSplineManager>();

        public GunManager GunManager => _gunManager
            ? _gunManager
            : _gunManager = GetComponent<GunManager>();

        public GunGuide GunGuide => _gunGuide
            ? _gunGuide
            : _gunGuide = GetComponent<GunGuide>();

        public override bool IsAlive => CurrentStates[0].GetType() != typeof(PlayerStateDeath);

        private void Start()
        {
            StateBases.AddRange(GetStateBases<PlayerStateBase>(this));
            StateBases.AddRange(GetStateBases<GunStateBase>(this));
            SwitchState<PlayerStateIdle>(0);
            SwitchState<GunStateIdle>(1);
        }
    }
}