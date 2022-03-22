using DG.Tweening;
using UnityEngine;

namespace TheyAreComing
{
    public class EnemyStateMovement : EnemyStateBase
    {
        public EnemyStateMovement(EnemyStateManager stateManager) : base(stateManager)
        {
        }


        public override void EnterState()
        {
            EnemyAnimationController.ToggleWalk(true);
            DOVirtual.Float(0, CharacterSettings.Speed, .5f, x => CurrentSpeed = x);
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            var distance = Vector3.Distance(PlayerTrans.position, EnemyTrans.position);
            if (CharacterSettings.AttackRange > distance)
            {
                StateManager.SwitchState<EnemyStateAttack>(0);
                return;
            }

            Move();
            Rotate();
        }

        public override void OnCollisionEnter(Collision collision)
        {
        }
    }
}