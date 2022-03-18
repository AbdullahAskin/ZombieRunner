using DG.Tweening;
using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(GunGuide))]
    public class GunManager : MonoBehaviour
    {
        [SerializeField] private Gun gun;
        [SerializeField] private float rotateSpeed;
        private Transform _aimPivotTrans;
        private SoldierState _currentState;
        private GunGuide _gunGuide;
        private Tween _updateAimTween;

        private void Awake()
        {
            _gunGuide = GetComponent<GunGuide>();
            _currentState = SoldierState.Attack;
            ToggleSubscribes(true);
        }

        public void FixedUpdate()
        {
            if (_currentState != SoldierState.Attack) return;
            UpdateAim();
        }

        private void OnDisable()
        {
            ToggleSubscribes(false);
        }

        private void ToggleSubscribes(bool bind)
        {
            if (bind) _aimPivotTrans = gun.aimPivotTrans;
        }

        private void UpdateAim()
        {
            var enemiesInRange = _gunGuide.GetEnemiesInRange();
            if (enemiesInRange.Count == 0) return;
            var targetAngle = _gunGuide.GetClosestAngle(enemiesInRange);
            var currentEuler = _aimPivotTrans.localEulerAngles;
            if (Mathf.Abs(targetAngle - currentEuler.y) < 1f) gun.Fire();
            var rotateAmount = Mathf.Sign(targetAngle - currentEuler.y) * Time.fixedDeltaTime * rotateSpeed;
            var currentAngle = rotateAmount + currentEuler.y;
            _aimPivotTrans.localEulerAngles = Mathf.Clamp(currentAngle, currentAngle, targetAngle) * Vector3.up;
        }
    }

    public enum SoldierState
    {
        Idle,
        Attack
    }
}