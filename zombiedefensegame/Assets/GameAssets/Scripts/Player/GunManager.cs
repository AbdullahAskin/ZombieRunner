using DG.Tweening;
using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(GunGuide))]
    public class GunManager : MonoBehaviour
    {
        public Transform aimPivotTrans;
        public Gun currentGun;
        public float rotateSpeed;

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

        // public void UpdateAim()
        // {
        //     var enemiesInRange = _gunGuide.GetEnemiesInRange();
        //     if (enemiesInRange.Count == 0) return;
        //     var targetAngle = _gunGuide.GetClosestAngle(enemiesInRange);
        //     var currentEuler = AimPivotTrans.localEulerAngles;
        //     if (Mathf.Abs(targetAngle - currentEuler.y) < 1f) currentGun.Fire();
        //     var rotateAmount = Mathf.Sign(targetAngle - currentEuler.y) * Time.fixedDeltaTime * rotateSpeed;
        //     var currentAngle = rotateAmount + currentEuler.y;
        //     AimPivotTrans.localEulerAngles = Mathf.Clamp(currentAngle, currentAngle, targetAngle) * Vector3.up;
        // }
    }
}