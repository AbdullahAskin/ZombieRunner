using UnityEngine;

namespace TheyAreComing
{
    public class EnemyBase : MonoBehaviour,IShootable
    {
        private void Awake()
        {
            EnemyManager.enemyBases.Add(this);
        }

        private void OnDisable()
        {
            EnemyManager.enemyBases.Remove(this);
        }

        public void Damage(float amount)
        {
            
        }
    }
}

