using UnityEngine;

namespace TheyAreComing
{
    public class EnemyCollisionManager : MonoBehaviour
    {
        [SerializeField] private ParticleSystem explosionParticle;
        private int _currentHealth;
        private EnemyBase _enemyBase;
        private EnemyStateManager _enemyStateManager;

        private EnemyBase EnemyBase => _enemyBase ? _enemyBase : _enemyBase = GetComponent<EnemyBase>();

        private int CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = Mathf.Clamp(_currentHealth - value, 0, EnemyBase.enemySettings.health);
                if (_currentHealth == 0) EnemyBase.Death();
            }
        }

        private EnemyStateManager EnemyStateManager =>
            _enemyStateManager ? _enemyStateManager : _enemyStateManager = GetComponent<EnemyStateManager>();

        private void Awake()
        {
            _currentHealth = EnemyBase.enemySettings.health;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!EnemyStateManager.IsAlive) return;
            if (!other.TryGetComponent(out ITriggerable<EnemyCollisionManager> triggerable)) return;
            triggerable.TriggerEnter(this);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out ITriggerable<EnemyCollisionManager> triggerable)) return;
            triggerable.TriggerEnter(this);
        }

        public void Damage(int amount)
        {
            CurrentHealth -= amount;
            explosionParticle.Play();
        }
    }
}