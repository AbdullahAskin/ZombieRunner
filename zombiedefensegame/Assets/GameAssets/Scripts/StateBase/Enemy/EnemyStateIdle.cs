using UnityEngine;

namespace TheyAreComing
{
    public class EnemyStateIdle : EnemyStateBase
    {
        public EnemyStateIdle(EnemyStateManager stateManager) : base(stateManager)
        {
        }

        public override void EnterState()
        {
            EnemyAnimationController.ToggleWalk(false);
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            var dis = Vector3.Distance(StateManager.transform.position, PlayerTrans.position);
            if (dis > CharacterSettings.range) return;
            StateManager.SwitchState<EnemyStateMovement>(0);
        }

    }
}