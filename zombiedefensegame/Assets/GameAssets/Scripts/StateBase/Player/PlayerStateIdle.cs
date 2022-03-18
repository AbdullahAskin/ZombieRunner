using UnityEngine;

namespace TheyAreComing
{
    public class PlayerStateIdle : PlayerStateBase
    {
        public PlayerStateIdle(PlayerStateManager playerStateManager, PlayerSplineManager splineManager) : base(
            playerStateManager, splineManager)
        {
        }

        public override void EnterState()
        {
            playerStateManager.PlayerAnimationController.ToggleWalk(false);
            playerStateManager.PlayerSplineManager.SetSpeed(0f, 1f);
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
        }

        public override void OnCollisionEnter(Collision collision)
        {
        }
    }
}