using System;
using System.Linq;
using UnityEngine;

namespace TheyAreComing
{
    public class GunGuide : MonoBehaviour
    {
        public Transform aimPivotTrans;
        public Vector2 aimLimit;
        public float range;

        private void Awake()
        {
            ToggleSubscribes(true);
        }

        private void OnDisable()
        {
            ToggleSubscribes(false);
        }

        private void ToggleSubscribes(bool bind)
        {
        }

        private float FromDirectionToAngle(Vector3 direction)
        {
            var radian = (float) Math.Atan2(direction.z, direction.x);
            var angle = radian * Mathf.Rad2Deg;
            return angle;
        }

        public bool IsAnyEnemyShootable()
        {
            return (from enemyBase in EnemyManager.TargetEnemyBases
                where Vector3.Distance(enemyBase.transform.position, transform.position) < range
                select FromDirectionToAngle(enemyBase.transform.position - aimPivotTrans.position)).Any(currentAngle =>
                !(currentAngle < aimLimit.x) && !(currentAngle > aimLimit.y));
        }

        public float GetClosestAngle()
        {
            var targetAngle = 90f;
            var minDis = float.MaxValue;

            foreach (var enemyBase in EnemyManager.TargetEnemyBases)
            {
                //Is enemy in min range
                var currentDis = Vector3.Distance(aimPivotTrans.position, enemyBase.transform.position);
                if (currentDis > minDis) continue;

                //Is enemy shootable
                var currentAngle = FromDirectionToAngle(enemyBase.transform.position - aimPivotTrans.position);
                if (currentAngle < aimLimit.x || currentAngle > aimLimit.y) continue;

                minDis = currentDis;
                targetAngle = currentAngle;
            }

            return targetAngle;
        }
    }
}