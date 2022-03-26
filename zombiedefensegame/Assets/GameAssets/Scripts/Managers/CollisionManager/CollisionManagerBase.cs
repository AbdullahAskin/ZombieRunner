using DamageNumbersPro;
using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(StateManager))]
    public abstract class CollisionManagerBase : MonoBehaviour
    {
        [SerializeField] private float ySpawnPos;
        [SerializeField] private DamageNumber damageNumber;
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

        protected void CalculateHealth(int amount)
        {
            CurrentHealth -= amount;
            CreateFeedback(amount);
        }

        private void CreateFeedback(int amount)
        {
            var additional = new Vector2(0, ySpawnPos);
            damageNumber.Spawn(transform.position + (Vector3) additional, amount, transform);
        }

        protected abstract void Death();
    }
}