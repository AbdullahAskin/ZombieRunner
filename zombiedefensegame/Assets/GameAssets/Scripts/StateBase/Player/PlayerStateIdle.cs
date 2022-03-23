using UnityEngine;

namespace TheyAreComing
{
    public class PlayerStateIdle : PlayerStateBase
    {
        public PlayerStateIdle(PlayerStateManager stateManager) : base(
            stateManager)
        {
        }

        public override void EnterState()
        {
            StateManager.PlayerAnimationController.ToggleWalk(false);
            StateManager.PlayerSplineManager.SetSpeed(0f, 1f);
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
        }
    }
}