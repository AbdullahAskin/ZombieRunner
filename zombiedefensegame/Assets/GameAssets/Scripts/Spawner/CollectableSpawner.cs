using UnityEngine;

namespace TheyAreComing
{
    public class CollectableSpawner : SpawnerBase
    {
        protected override Transform GetSpawnPrefab()
        {
            return spawnablePrefabs[Random.Range(0, spawnablePrefabs.Count)];
        }
    }
}