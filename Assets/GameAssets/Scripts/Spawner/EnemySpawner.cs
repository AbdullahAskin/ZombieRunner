using UnityEngine;

namespace TheyAreComing
{
    public class EnemySpawner : SpawnerBase
    {
        protected override Transform GetSpawnPrefab()
        {
            var iSpawnPrefab = Random.Range(0,spawnablePrefabs.Count);
            return spawnablePrefabs[iSpawnPrefab];
        }
    }
}