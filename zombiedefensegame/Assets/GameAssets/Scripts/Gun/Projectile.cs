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
        [SerializeField] private Vector2 damageLimit;
        private Tween _lifeTimeTween;
        private Tween _movementTween;

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
            _movementTween?.Kill();
            collisionManager.Damage((int) Random.Range(damageLimit.x, damageLimit.y), gunIndex);
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