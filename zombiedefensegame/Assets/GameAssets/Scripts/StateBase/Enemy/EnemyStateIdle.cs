using UnityEngine;

namespace TheyAreComing
{
    public class EnemyStateIdle : EnemyStateBase
    {
        public EnemyStateIdle(EnemyStateManager enemyStateManager) : base(enemyStateManager)
        {
        }

        public override void EnterState()
        {
            _enemyStateManager.enemyAnimationController.ToggleWalk(false);
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