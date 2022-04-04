using UnityEngine;

namespace TheyAreComing
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyBase enemyBasePrefab;
        [SerializeField] private float spawnStartDistance;
        [SerializeField] private Vector2 spawnDistanceLimit;
        [SerializeField] private Vector2 spawnPerMeterLimit;
        private float _targetSpawnZ;
        private Vector3 PlayerPos => GameManager.Player.Position;

        private void Start()
        {
            _targetSpawnZ = PlayerPos.z + spawnStartDistance;
        }

        private void Update()
        {
            if (PlayerPos.z < _targetSpawnZ) return;
            Spawn();
            _targetSpawnZ += Random.Range(spawnPerMeterLimit.x, spawnPerMeterLimit.y);
        }

        private void Spawn()
        {
            var spawnZ = PlayerPos.z + Random.Range(spawnDistanceLimit.x, spawnDistanceLimit.y);
            var spawnX = Random.Range(-5, 5);
            var spawnedEnemyBase = Instantiate(enemyBasePrefab, new Vector3(spawnX, 0, spawnZ), Quaternion.identity);
        }
    }
}