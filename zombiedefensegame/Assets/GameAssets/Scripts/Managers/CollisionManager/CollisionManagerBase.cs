using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(StateManager))]
    public abstract class CollisionManagerBase : MonoBehaviour
    {
        [SerializeField] private ParticleSystem damageParticle;
        private int _currentHealth;
        private StateManager _stateManager;

        protected StateManager StateManager =>
            _stateManager ? _stateManager : _stateManager = GetComponent<StateManager>();

        protected int CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = Mathf.Clamp(value, 0, MAXHealth);
        }

        protected abstract int MAXHealth { get; }

        private void Awake()
        {
            _currentHealth = MAXHealth;
        }

        public virtual void Damage(int amount)
        {
            damageParticle.Play();
            CurrentHealth -= amount;
        }

        protected abstract void Death();
    }
}