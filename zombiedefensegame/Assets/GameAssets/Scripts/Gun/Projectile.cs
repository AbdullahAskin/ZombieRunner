using DG.Tweening;
using Lean.Pool;
using UnityEngine;

namespace TheyAreComing
{
    public class Projectile : MonoBehaviour, ITriggerable<EnemyCollisionManager>
    {
        [SerializeField] private float lifeTime;
        [SerializeField] private float speed;
        [SerializeField] private int damage;
        private Tween _movementTween;
        private Tween _lifeTimeTween;

        private void OnDisable()
        {
            _movementTween?.Kill();
            _lifeTimeTween?.Kill();
        }

        private void OnEnable()
        {
            _lifeTimeTween = DOVirtual.DelayedCall(lifeTime, () => LeanPool.Despawn(gameObject));
        }

        public void TriggerEnter(EnemyCollisionManager collisionManager)
        {
            _movementTween?.Kill();
            collisionManager.Damage(damage);
            LeanPool.Despawn(gameObject, 2f);
        }

        public void TriggerExit(EnemyCollisionManager collisionManager)
        {
        }

        public void Init(Vector3 startDir)
        {
            transform.rotation = Quaternion.LookRotation(startDir);
            _movementTween = transform.DOMove(speed * startDir, 1f).SetRelative().SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental);
        }
    }
}