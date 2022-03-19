using DG.Tweening;
using Lean.Pool;
using UnityEngine;

namespace TheyAreComing
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform projectileSpawnTrans;
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private ParticleSystem muzzleParticle;
        [SerializeField] private float fireOffset;
        private bool _canFire = true;

        public void Fire()
        {
            if (!_canFire) return;
            DOVirtual.DelayedCall(fireOffset, () => _canFire = true).OnStart(() => _canFire = false);
            muzzleParticle.Play();
            var projectile = LeanPool.Spawn(projectilePrefab, projectileSpawnTrans.position, Quaternion.identity);
            projectile.Init(projectileSpawnTrans.transform.forward);
        }
    }
}