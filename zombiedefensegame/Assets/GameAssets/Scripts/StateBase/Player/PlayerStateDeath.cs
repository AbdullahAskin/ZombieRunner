namespace TheyAreComing
{
    public class PlayerStateDeath : PlayerStateBase
    {
        public PlayerStateDeath(PlayerStateManager stateManager) : base(stateManager)
        {
        }

        public override void EnterState()
        {
            SplineManager.SetSpeed(0f, .3f);
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