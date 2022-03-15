namespace TheyAreComing
{
    public class Player : SoldierBase
    {
        private PlayerStateManager _playerStateManager;

        private PlayerStateManager playerStateManager => _playerStateManager
            ? _playerStateManager
            : _playerStateManager = GetComponent<PlayerStateManager>();

        public void Init()
        {
            playerStateManager.SwitchState<PlayerMovementState>();
        }
    }
}