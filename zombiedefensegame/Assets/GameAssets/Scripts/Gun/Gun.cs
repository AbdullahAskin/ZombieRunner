using Lean.Pool;
using UnityEngine;

namespace TheyAreComing
{
    public class Gun : MonoBehaviour
    {
        public Transform aimTrans;
        [SerializeField] private Transform projectileSpawnTrans;
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private ParticleSystem muzzleParticle;
        public float recoilAmount;
        public float recoilDuration;
        public float fireOffset;

        public void Fire()
        {
            muzzleParticle.Play();
            var projectile = LeanPool.Spawn(projectilePrefab, projectileSpawnTrans.position, Quaternion.identity);
            projectile.Init(projectileSpawnTrans.transform.right);
        }
    }
}