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
            if (!GameManager.Player.IsAlive) return;
            
            StateManager.SwitchState<EnemyStateMovement>(0);
        }
    }
}