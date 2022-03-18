using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheyAreComing
{
    public class GunGuide : MonoBehaviour
    {
        public Transform aimPivotTrans;
        public Vector2 aimLimit;
        public float range;
        
        private void Awake() => ToggleSubscribes(true);
        private void OnDisable() => ToggleSubscribes(false);

        private void ToggleSubscribes(bool bind)
        {
        }

        private float FromDirectionToAngle(Vector3 direction)
        {
            var radian = (float)Math.Atan2(direction.z, direction.x);
            var angle = radian * Mathf.Rad2Deg;
            return angle;
        }

        public List<EnemyBase> GetEnemiesInRange()=>  EnemyManager.EnemyBases.FindAll(x=> Vector3.Distance(x.transform.position, transform.position) < range);

        public float GetClosestAngle(List<EnemyBase> enemiesInRange)
        {
            var targetAngle = 90f;
            var minDis = float.MaxValue;
            
            foreach (var enemyBase in enemiesInRange)
            {
                var currentAngle = FromDirectionToAngle(enemyBase.transform.position - aimPivotTrans.position);
                if (currentAngle < aimLimit.x || currentAngle > aimLimit.y) continue;
                var currentDis = Vector3.Distance(aimPivotTrans.position, enemyBase.transform.position);
                if (currentDis > minDis) continue;
                minDis = currentDis;
                targetAngle = currentAngle;
            }

            return targetAngle;
        }
    }
}