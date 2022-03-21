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
        
        private void Move()
        {
            var step = _currentSpeed * Time.fixedDeltaTime;
            EnemyTrans.position = Vector3.MoveTowards(EnemyTrans.position, PlayerTrans.position, step);
        }
    }
}