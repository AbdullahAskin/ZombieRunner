using DamageNumbersPro;
using UnityEngine;

namespace TheyAreComing
{
    [RequireComponent(typeof(StateManager))]
    public abstract class CollisionManagerBase : MonoBehaviour
    {
        [SerializeField] private float ySpawnPos;
        [SerializeField] private DamageNumber damageNumber;
        [SerializeField] private DamageNumber healNumber;
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
        protected abstract Vector3 CharacterPos { get; }

        private void Awake()
        {
            _currentHealth = MAXHealth;
        }

        protected void OnHealthChange(int amount)
        {
            CurrentHealth += amount;
            CreateFeedback(amount);
        }

        private void CreateFeedback(int amount)
        {
            var absAmount = Mathf.Abs(amount);
            var additional = new Vector2(0, ySpawnPos);
            if (amount > 0) healNumber.Spawn(CharacterPos + (Vector3)additional * 2, absAmount);
            else damageNumber.Spawn(CharacterPos + (Vector3)additional, absAmount);
        }

        protected abstract void OnDeath();
    }
}