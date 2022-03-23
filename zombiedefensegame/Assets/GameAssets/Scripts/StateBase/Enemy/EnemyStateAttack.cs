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
            SetRelativeSpeed(-1f, .5f);
            var isMirroredAttack = PlayerTrans.position.x > EnemyTrans.position.x;
            EnemyAnimationController.TriggerAttack(isMirroredAttack);
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            Move();
            Rotate();
        }

        public override void OnCollisionEnter(Collision collision)
        {
        }
    }
}