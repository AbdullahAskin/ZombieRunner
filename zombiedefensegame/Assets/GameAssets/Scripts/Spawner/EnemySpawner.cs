using UnityEngine;

namespace TheyAreComing
{
    public class EnemySpawner : SpawnerBase
    {
        protected override Transform GetSpawnPrefab()
        {
            return spawnablePrefabs[0];
        }
    }
}