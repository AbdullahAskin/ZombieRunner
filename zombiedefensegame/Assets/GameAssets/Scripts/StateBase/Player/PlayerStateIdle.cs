using UnityEngine;

namespace TheyAreComing
{
    public class PlayerStateIdle : PlayerStateBase
    {
        public PlayerStateIdle(PlayerStateManager playerStateManager) : base(
            playerStateManager)
        {
        }

        public override void EnterState()
        {
            PlayerStateManager.PlayerAnimationController.ToggleWalk(false);
            PlayerStateManager.PlayerSplineManager.SetSpeed(0f, 1f);
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