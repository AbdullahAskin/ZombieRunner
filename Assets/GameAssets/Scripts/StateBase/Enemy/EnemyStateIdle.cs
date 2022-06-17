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
            EnemyTrans.rotation = Quaternion.LookRotation(Player.Position - EnemyTrans.position, Vector3.up);
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            var dis = Vector3.Distance(StateManager.transform.position, Player.Position);
            if (dis > CharacterSettings.chaseRange) return;
            StateManager.SwitchState<EnemyStateMovement>(0);
        }
    }
}