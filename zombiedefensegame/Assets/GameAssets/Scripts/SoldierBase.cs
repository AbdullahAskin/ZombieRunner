using DG.Tweening;
using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(GunGuide))]
    public abstract class SoldierBase : MonoBehaviour
    {
        [SerializeField] private Gun gun;
        [SerializeField] private float rotateSpeed;
        private SoldierState _currentState;
        private GunGuide _gunGuide;
        private Tween _updateAimTween;
        private Transform _aimPivotTrans;

        private void Awake()
        {
            _gunGuide = GetComponent<GunGuide>();
            ToggleSubscribes(true);
        }

        public void FixedUpdate()
        {
            UpdateAim();
        }

        private void OnDisable()
        {
            ToggleSubscribes(false);
        }

        private void ToggleSubscribes(bool bind)
        {
            if(bind) _aimPivotTrans = gun.aimPivotTrans;
        }

        private void UpdateAim()
        {
            var enemiesInRange = _gunGuide.GetEnemiesInRange();
            var targetAngle = _gunGuide.GetClosestAngle(enemiesInRange);
            var currentEuler = _aimPivotTrans.localEulerAngles;
            if (Mathf.Approximately(currentEuler.y, targetAngle)) gun.Fire();
            _aimPivotTrans.localEulerAngles = Mathf.Lerp(currentEuler.y, targetAngle, .2f) * Vector3.up;
        }
    }

    public enum SoldierState
    {
        Idle,
        Attack
    }
}