using DG.Tweening;
using UnityEngine;

namespace TheyAreComing
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform projectileSpawnTrans;
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private float fireOffset;
        public Transform aimPivotTrans;
        private bool _canFire = true;

        public void Fire()
        {
            if (!_canFire) return;
            DOVirtual.DelayedCall(fireOffset, () => _canFire = true).OnStart(() => _canFire = false);
            var projectile = Instantiate(projectilePrefab, projectileSpawnTrans.position, Quaternion.identity);
            projectile.Init(projectileSpawnTrans.transform.forward);
        }
    }
}