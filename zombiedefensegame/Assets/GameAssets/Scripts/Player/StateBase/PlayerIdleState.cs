using Dreamteck.Splines;
using UnityEngine;

namespace TheyAreComing
{
    public class PlayerIdleState : PlayerBaseState
    {
        public override void EnterState(PlayerStateManager playerStateManager)
        {
            playerStateManager.playerAnimationController.ToggleWalk(false);
            playerStateManager.playerSplineManager.SetSpeed(0f,1f);
        }

        public override void ExitState(PlayerStateManager playerStateManager)
        {
            
        }

        public override void UpdateState(PlayerStateManager playerStateManager)
        {
        }

        public override void OnCollisionEnter(PlayerStateManager playerStateManager,Collision collision)
        {
        }

        public PlayerIdleState(PlayerStateManager stateManager, PlayerSplineManager splineManager) : base(stateManager, splineManager)
        {
        }
    }
}