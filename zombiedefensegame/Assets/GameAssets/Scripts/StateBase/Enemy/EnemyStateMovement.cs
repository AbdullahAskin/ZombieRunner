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
            SetSpeed(0, CharacterSettings.Speed, .5f);
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
    }
}