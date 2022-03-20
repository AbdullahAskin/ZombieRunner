using DG.Tweening;
using UnityEngine;

namespace TheyAreComing
{
    public class EnemyStateMovement : EnemyStateBase
    {
        private float _currentSpeed;
    
        public EnemyStateMovement(EnemyStateManager stateManager) : base(stateManager)
        {
        }


        public override void EnterState()
        {
            EnemyAnimationController.ToggleWalk(true);
            DOVirtual.Float(0, CharacterSettings.speed, .5f, x => _currentSpeed = x);
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            var distance = Vector3.Distance(PlayerTrans.position, EnemyTrans.position);
            if (CharacterSettings.attackRange > distance)
                //ATTACK
                // EnemyAnimationController.ToggleWalk(false);
                return;

            var dir = (PlayerTrans.position - EnemyTrans.position).normalized;
            Move();
            Rotate();
        }

        public override void OnCollisionEnter(Collision collision)
        {
        }

        private void Rotate()
        {
            var targetRotation = Quaternion.LookRotation(PlayerTrans.position - EnemyTrans.position,Vector3.up);
            EnemySkinTrans.rotation = Quaternion.Slerp(EnemySkinTrans.rotation, targetRotation, .3f);
        }

        private void Move()
        {
            var step = _currentSpeed * Time.fixedDeltaTime;
            EnemyTrans.position = Vector3.MoveTowards(EnemyTrans.position, PlayerTrans.position, step);
        }
    }
}