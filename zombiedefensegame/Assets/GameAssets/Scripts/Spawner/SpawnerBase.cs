using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TheyAreComing
{
    public abstract class SpawnerBase : MonoBehaviour
    {
        [SerializeField] protected List<Transform> spawnablePrefabs;
        [SerializeField] protected Vector2 spawnPerDistance;
        [SerializeField] protected Vector2 horizontalLimit;
        [SerializeField] protected Vector2 verticalSpawnPos;
        [SerializeField] protected float destroyDistance;
        [SerializeField] private float startDeadZoneDistance;

        protected static readonly List<Transform> CurrentSpawns = new List<Transform>();
        protected int iLastSpawnedPrefab;
        [NonSerialized] public Vector3 LastSpawnPos;
        [NonSerialized] public float TargetDistance;
        protected Vector3 PlayerPos => GameManager.Player.transform.position;

        public void OnEnable()
        {
            LastSpawnPos = (PlayerPos.z + startDeadZoneDistance) * Vector3.forward;
        }

        protected abstract Transform GetSpawnPrefab();

        public void SpawnObject()
        {
            LastSpawnPos = PlayerPos;
            var spawnPrefab = GetSpawnPrefab();
            TargetDistance = Random.Range(spawnPerDistance.x, spawnPerDistance.y);

            var spawnPosX = Random.Range(horizontalLimit.x, horizontalLimit.y) * Vector3.right;
            var spawnPosY = spawnPrefab.position.y * Vector3.up;
            var spawnPosZ = (PlayerPos.z + Random.Range(verticalSpawnPos.x, verticalSpawnPos.y)) * Vector3.forward;

            var spawnedEnemyBase = Instantiate(spawnPrefab, spawnPosX + spawnPosY + spawnPosZ, Quaternion.identity);
            CurrentSpawns.Add(spawnedEnemyBase);
        }

        public void RemoveCheck()
        {
            var destroyedSpawn = CurrentSpawns.Find(x => PlayerPos.z - x.position.z > destroyDistance);
            if (!destroyedSpawn) return;
            CurrentSpawns.Remove(destroyedSpawn);
            Destroy(destroyedSpawn.gameObject);
        }

        public static void RemoveSpawn(Transform spawn)
        {
            CurrentSpawns.Remove(spawn);
        }
    }
}