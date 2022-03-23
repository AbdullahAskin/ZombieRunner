using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(StateManager))]
    public abstract class CollisionManagerBase : MonoBehaviour
    {
        private int _currentHealth;
        private StateManager _stateManager;

        protected StateManager StateManager =>
            _stateManager ? _stateManager : _stateManager = GetComponent<StateManager>();

        protected int CurrentHealth
        {
            get => _currentHealth;
            set
            {
                _currentHealth = Mathf.Clamp(value, 0, MAXHealth);
                if (_currentHealth == 0) Death();
            }
        }

        protected abstract int MAXHealth { get; }

        private void Awake()
        {
            _currentHealth = MAXHealth;
        }
        
        public abstract void Damage(int amount);

        protected abstract void Death();
    }
}