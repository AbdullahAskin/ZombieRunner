using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace TheyAreComing
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private List<SpawnerBase> spawnerBases;
        private Tween _updateSpawnablesTween;
        private Vector3 PlayerPos => GameManager.Player.transform.position;

        private void OnEnable()
        {
            _updateSpawnablesTween = DOVirtual.DelayedCall(.5f, UpdateSpawnables).SetLoops(-1);
        }

        private void OnDisable()
        {
            _updateSpawnablesTween?.Kill();
        }

        private void UpdateSpawnables()
        {
            foreach (var spawnerBase in spawnerBases)
            {
                spawnerBase.RemoveCheck();

                var lastSpawnPos = spawnerBase.LastSpawnPos;
                var distance = Vector3.Distance(lastSpawnPos, PlayerPos);
                var spawnDistance = spawnerBase.TargetDistance;
                if (lastSpawnPos.z > PlayerPos.z || spawnDistance > distance) continue;

                spawnerBase.SpawnObject();
            }
        }
    }
}