using DG.Tweening;
using Lean.Pool;
using UnityEngine;

namespace TheyAreComing
{
    public class Projectile : MonoBehaviour, ITriggerable<EnemyCollisionManager>
    {
        [SerializeField] private float lifeTime;
        [SerializeField] private float force;
        [SerializeField] private float collideOffset = 0.15f;
        [SerializeField] private Vector2 damageLimit;
        [SerializeField] private ParticleSystem impactParticlePrefab;
        private Tween _lifeTimeTween;

        private void OnEnable()
        {
            _lifeTimeTween = DOVirtual.DelayedCall(lifeTime, () => LeanPool.Despawn(gameObject));
        }

        private void OnDisable()
        {
            _lifeTimeTween?.Kill();
        }

        public void TriggerEnter(EnemyCollisionManager collisionManager)
        {
        }

        public void CollisionEnter(EnemyCollisionManager collisionManager, ContactPoint contactPoint)
        {
            collisionManager.Damage((int) Random.Range(damageLimit.x, damageLimit.y));
            transform.position = contactPoint.point - contactPoint.normal * collideOffset;
            Instantiate(impactParticlePrefab, transform.position,
                Quaternion.FromToRotation(Vector3.up, contactPoint.normal));
            LeanPool.Despawn(gameObject);
        }

        public void Init(Vector3 startDir)
        {
            transform.rotation = Quaternion.LookRotation(startDir);
            GetComponent<Rigidbody>().AddForce(force * startDir);
        }
    }
}