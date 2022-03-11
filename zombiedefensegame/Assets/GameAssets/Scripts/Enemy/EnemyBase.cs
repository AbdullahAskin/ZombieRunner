using System;
using UnityEngine;

namespace TheyAreComing
{
    public class EnemyBase : MonoBehaviour
    {
        private void Awake()
        {
            EnemyManager.enemyBases.Add(this);
        }

        private void OnDisable()
        {
            EnemyManager.enemyBases.Remove(this);
        }
    }
}

