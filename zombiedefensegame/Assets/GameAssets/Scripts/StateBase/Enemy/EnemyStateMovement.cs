using DG.Tweening;
using UnityEngine;

namespace TheyAreComing
{
    public class EnemyStateMovement : EnemyStateBase
    {
        private Tween _movementTween;

        public EnemyStateMovement(EnemyStateManager stateManager) : base(stateManager)
        {
        }


        public override void EnterState()
        {
            EnemyAnimationController.ToggleWalk(true);
            _movementTween = SetSpeed(0, CharacterSettings.Speed, .5f);
        }

        public override void ExitState()
        {
            _movementTween?.Kill();
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
    }
}