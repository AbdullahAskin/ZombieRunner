using UnityEngine;

namespace TheyAreComing
{
    public class EnemyStateAttack : EnemyStateBase
    {
        public EnemyStateAttack(EnemyStateManager stateManager) : base(stateManager)
        {
        }

        public override void EnterState()
        {
            EnemyAnimationController.ToggleWalk(false);
            EnemyAnimationController.SetTrigger(EnemyAnimationController.Attack);
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            Rotate();
        }

        public override void OnCollisionEnter(Collision collision)
        {
        }
    }
}