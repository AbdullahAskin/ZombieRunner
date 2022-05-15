using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using TheyAre;
using UnityEngine;

namespace TheyAreComing
{
    public class ChunkSpawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> chunkPrefabs;
        [SerializeField] private int totalChunkCount;
        [SerializeField] private float spawnDistance;
        private readonly List<Transform> _currentChunks = new List<Transform>();
        private Tween _chunkUpdateTween;
        private int _iLastSpawnedChunk;
        private Vector3 playerPos => GameManager.Player.transform.position;

        private void OnEnable()
        {
            InitChunks();
            _chunkUpdateTween = DOVirtual.DelayedCall(2f, UpdateChunks).SetLoops(-1);
        }

        private void OnDisable()
        {
            _chunkUpdateTween?.Kill();
        }

        private void InitChunks()
        {
            var iTargetChunk = chunkPrefabs.Count;
            for (var index = 0; index < totalChunkCount; index++)
            {
                iTargetChunk = SharedFunctions.GetRandomExcept(chunkPrefabs.Count, iTargetChunk);
                var spawnPos = index * spawnDistance * Vector3.forward;
                var spawnedChunk = LeanPool.Spawn(chunkPrefabs[iTargetChunk], spawnPos, Quaternion.identity);
                _currentChunks.Add(spawnedChunk);
            }
        }

        private void UpdateChunks()
        {
            var destroyableChunk = _currentChunks.Find(x =>
                playerPos.z > x.position.z && Vector3.Distance(x.position, playerPos) >= spawnDistance);
            if (destroyableChunk == null) return;

            //Destroy
            var destroyZ = destroyableChunk.position.z;
            _currentChunks.Remove(destroyableChunk);
            LeanPool.Despawn(destroyableChunk.gameObject);

            //Spawn
            _iLastSpawnedChunk = SharedFunctions.GetRandomExcept(chunkPrefabs.Count, _iLastSpawnedChunk);
            var spawnZ = destroyZ + totalChunkCount * spawnDistance;
            var spawnedChunk = LeanPool.Spawn(chunkPrefabs[_iLastSpawnedChunk], Vector3.forward * spawnZ,
                Quaternion.identity);
            _currentChunks.Add(spawnedChunk);
        }
    }
}