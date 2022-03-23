using UnityEngine;

namespace TheyAreComing
{
    public class EnemyEmptyState : EnemyStateBase
    {
        public EnemyEmptyState(EnemyStateManager stateManager) : base(stateManager)
        {
        }

        public override void EnterState()
        {
            EnemyAnimationController.ToggleWalk(false);
            SetRelativeSpeed(0f,.5f);
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
        }

    }
}