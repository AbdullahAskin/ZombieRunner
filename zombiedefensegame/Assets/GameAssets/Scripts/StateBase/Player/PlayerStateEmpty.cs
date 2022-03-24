using TheyAreComing;
using UnityEngine;

namespace ExampleNamespace
{
    public class PlayerStateEmpty : PlayerStateBase
    {
        public PlayerStateEmpty(PlayerStateManager stateManager) : base(stateManager)
        {
        }

        public override void EnterState()
        {
            AnimationController.ToggleWalk(false);
            SplineManager.SetSpeed(0f, .2f);
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
        }
    }
}