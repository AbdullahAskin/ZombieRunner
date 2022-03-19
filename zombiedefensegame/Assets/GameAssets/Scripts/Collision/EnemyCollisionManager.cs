using UnityEngine;

namespace TheyAreComing
{
    public class EnemyCollisionManager : MonoBehaviour
    {
        [SerializeField] private ParticleSystem explosionParticle;
        [SerializeField] private int maxHealth;
        private int _currentHealth;

        private void Awake()
        {
            _currentHealth = maxHealth;
        }

        public void Damage(int amount)
        {
            //Take damage
            explosionParticle.Play();
        }
    }
}