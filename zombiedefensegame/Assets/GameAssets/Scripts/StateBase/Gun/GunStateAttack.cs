using UnityEngine;

namespace TheyAreComing
{
    public class GunStateAttack : GunStateBase
    {
        public GunStateAttack(PlayerStateManager stateManager) : base(stateManager)
        {
        }

        public override void EnterState()
        {
        }

        public override void ExitState()
        {
        }

        public override void UpdateState()
        {
            var enemiesInRange = GunGuide.GetEnemiesInRange();
            if (enemiesInRange.Count == 0)
            {
                StateManager.SwitchState<GunStateIdle>(1);
                return;
            }

            var targetAngle = GunGuide.GetClosestAngle(enemiesInRange);
            if (Mathf.Abs(targetAngle - AimPivotTrans.localEulerAngles.y) < 1f)
                GunManager.Fire();
            UpdateAimTowardsTarget(targetAngle);
        }

        public override void OnCollisionEnter(Collision collision)
        {
        }

        private void UpdateAimTowardsTarget(float targetAngle)
        {
            var currentEuler = AimPivotTrans.localEulerAngles;
            var rotateAmount = Mathf.Sign(targetAngle - currentEuler.y) * Time.fixedDeltaTime * GunManager.rotateSpeed;
            var currentAngle = rotateAmount + currentEuler.y;
            AimPivotTrans.localEulerAngles = Mathf.Clamp(currentAngle, currentAngle, targetAngle) * Vector3.up;
        }
    }
}