using UnityEngine;

namespace TheyAreComing
{
    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private EnemyCollisionManager _enemyCollisionManager;

        private EnemyCollisionManager EnemyCollisionManager => _enemyCollisionManager
            ? _enemyCollisionManager
            : _enemyCollisionManager = GetComponent<EnemyCollisionManager>();

        private void Awake()
        {
            EnemyManager.EnemyBases.Add(this);
        }

        public void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.R)) animator.CrossFadeInFixedTime("Die Backwards", 0.2f);
        }

        private void OnDisable()
        {
            EnemyManager.EnemyBases.Remove(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out ITriggerable<EnemyCollisionManager> triggerable)) return;
            triggerable.TriggerEnter(EnemyCollisionManager);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out ITriggerable<EnemyCollisionManager> triggerable)) return;
            triggerable.TriggerEnter(EnemyCollisionManager);
        }
    }
}