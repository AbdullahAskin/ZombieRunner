using DG.Tweening;
using Lean.Pool;
using UnityEngine;

namespace TheyAreComing
{
    public class Projectile : MonoBehaviour, ITriggerable<EnemyCollisionManager>
    {
        [SerializeField] private int gunIndex;
        [SerializeField] private float lifeTime;
        [SerializeField] private float speed;
        [SerializeField] private float collideOffset = 0.15f;
        [SerializeField] private Vector2 damageLimit;
        [SerializeField] private ParticleSystem impactParticlePrefab;
        private Tween _lifeTimeTween;
        private Tween _movementTween;
        private Vector3 _startDir;

        private void OnEnable()
        {
            _lifeTimeTween = DOVirtual.DelayedCall(lifeTime, () => LeanPool.Despawn(gameObject));
        }

        private void OnDisable()
        {
            _movementTween?.Kill();
            _lifeTimeTween?.Kill();
        }

        public void TriggerEnter(EnemyCollisionManager collisionManager)
        {
            // _movementTween?.Kill();
            // collisionManager.Damage((int) Random.Range(damageLimit.x, damageLimit.y), gunIndex);
            // Instantiate(explosionParticle);
            // transform.position = hit.point + hit.normal * collideOffset; // Move projectile to point of collision
            // LeanPool.Despawn(gameObject, 2f);
            if ()
                _movementTween?.Kill();
            collisionManager.Damage((int) Random.Range(damageLimit.x, damageLimit.y), gunIndex);
            transform.position = contactPoint.point + contactPoint.normal * collideOffset;
            Instantiate(impactParticlePrefab, transform.position,
                Quaternion.FromToRotation(Vector3.up, contactPoint.normal)); // Spawns impact effect
            Destroy(gameObject, 2f);
        }


        public void Init(Vector3 startDir)
        {
            _startDir = startDir;
            transform.rotation = Quaternion.LookRotation(startDir);
            _movementTween = transform.DOMove(speed * startDir, 1f).SetRelative().SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental);
        }
    }
}