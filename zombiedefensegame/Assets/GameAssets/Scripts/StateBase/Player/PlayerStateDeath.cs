namespace TheyAreComing
{
    public class PlayerStateDeath : PlayerStateBase
    {
        public PlayerStateDeath(PlayerStateManager stateManager) : base(stateManager)
        {
        }

        public override void EnterState()
        {
            Movement.SetSpeed(0f, 0f);
            AnimationController.OnDeath();
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
        }
    }
}