using DG.Tweening;
using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(GunGuide))]
    public abstract class SoldierBase : MonoBehaviour
    {
        [SerializeField] private Transform aimPivotTrans;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private GameObject projectilePrefab;
        private GunGuide _gunGuide;
        private SoldierState _currentState;
        private Tween _updateAimTween; 
        
        private void Awake()
        {
            _gunGuide = GetComponent<GunGuide>();
            ToggleSubscribes(true);
        }

        private void OnDisable() => ToggleSubscribes(false);

        private void ToggleSubscribes(bool bind) { }

        public void FixedUpdate()
        {
            UpdateAim();
        }

        private void UpdateAim()
        {
            var enemiesInRange = _gunGuide.GetEnemiesInRange();
            if (enemiesInRange.Count == 0)
            {
                //IDLE
            }
            var targetAngle = _gunGuide.GetClosestAngle(enemiesInRange);
            var currentEuler = aimPivotTrans.localEulerAngles;
            if (Mathf.Approximately(currentEuler.y, targetAngle))
            {
                GameObject projectile = Instantiate(projectilePrefab, aimPivotTrans.position, Quaternion.identity) as GameObject; //Spawns the selected projectile
                projectile.GetComponent<Rigidbody>().AddForce(aimPivotTrans.transform.right * 1000); //Set the speed of the projectile by applying force to the rigidbody                return;
            }
            aimPivotTrans.localEulerAngles = Mathf.Lerp(currentEuler.y, targetAngle, .2f) * Vector3.up; 
        }
        
        public void OnAttack(EnemyBase enemyBase)
        {
            if (_currentState == SoldierState.Attack) return;
            aimPivotTrans.position = enemyBase.transform.position;
        }

        public void OnIdle()
        {
        }
    }

    public enum SoldierState
    {
        Idle,
        Attack
    }
}