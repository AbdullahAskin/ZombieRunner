using DG.Tweening;
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
            EnemyAnimationController.SetTrigger(EnemyAnimationController.Attack);
            DOVirtual.Float(CurrentSpeed, 2f, 1f, x => CurrentSpeed = x);
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