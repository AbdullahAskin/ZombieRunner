using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TheyAreComing
{
    public class EnemyFactory : MonoBehaviour
    {
        public List<ParticleSystem> explosionParticles;

        private void Start()
        {
            CreateParticles();
        }

        private void CreateParticles()
        {
            foreach (var collisionManager in EnemyManager.AllEnemyBases.Select(enemyBase =>
                enemyBase.GetComponent<EnemyCollisionManager>())) collisionManager.CreateParticles(explosionParticles);
        }
    }
}