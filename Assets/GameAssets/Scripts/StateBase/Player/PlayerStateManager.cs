using ExampleNamespace;
using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(GunGuide))]
    [RequireComponent(typeof(PlayerAnimationController), typeof(PlayerMovement))]
    public class PlayerStateManager : StateManager
    {
        private GunGuide _gunGuide;
        private GunManager _gunManager;
        private Player _player;
        private PlayerAnimationController _playerAnimationController;
        private PlayerMovement _playerMovement;

        public PlayerAnimationController PlayerAnimationController => _playerAnimationController
            ? _playerAnimationController
            : _playerAnimationController = GetComponent<PlayerAnimationController>();

        public Player Player => _player
            ? _player
            : _player = GetComponent<Player>();

        public PlayerMovement PlayerMovement => _playerMovement
            ? _playerMovement
            : _playerMovement = GetComponent<PlayerMovement>();

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
            SwitchState<PlayerStateEmpty>(0);
            SwitchState<GunStateEmpty>(1);
        }
    }
}