using UnityEngine;

namespace TheyAreComing
{
    public class EnemyBase : MonoBehaviour, IShootable
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float maxHealth;
        private float _currentHealth;

        private void Awake()
        {
            EnemyManager.enemyBases.Add(this);
            _currentHealth = maxHealth;
        }

        public void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.R)) animator.CrossFadeInFixedTime("Die Backwards", 0.2f);
        }

        private void OnDisable()
        {
            EnemyManager.enemyBases.Remove(this);
        }

        public void Damage(float amount)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - amount, 0, maxHealth);
            if (_currentHealth <= 0)
            {
                //KILL
            }
        }
    }
}